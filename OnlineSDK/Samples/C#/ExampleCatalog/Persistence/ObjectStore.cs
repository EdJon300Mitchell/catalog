using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Dapper;
using Microsoft.Data.Sqlite;
using Mitchell1.Online.Catalog.Host.TransferObjects;
using Newtonsoft.Json;

namespace ExampleCatalog.Persistence
{
	public static class ObjectStore
	{
		private static readonly DbConnection keepAlive;

		static ObjectStore()
		{
			try
			{
				Console.WriteLine("Creating In-Memory SQL Database...");
				var sql = GetResource("ExampleCatalog.Persistence.DatabaseSchema.txt");

				// Do not close this connection - if it closes, we loose in-memory db that is shared
				keepAlive = GetConnection();

				using (var command = keepAlive.CreateCommand())
				{
					command.CommandType = CommandType.Text;
					command.CommandText = sql;
					command.ExecuteScalar();
				}

				Console.WriteLine("DB Created");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		private static string GetResource(string name)
		{
			var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
			if (resource == null)
				throw new ArgumentNullException(name);

			using (var reader = new StreamReader(resource))
			{
				return reader.ReadToEnd();
			}
		}

		private static DbConnection GetConnection()
		{
			var connectionString = ConfigurationManager.ConnectionStrings["orders"];
			DbConnection connection;
			if (connectionString.ProviderName == "MS.SQlite")
			{
				connection = new SqliteConnection();
			}
			else
			{
				var factory = DbProviderFactories.GetFactory(connectionString.ProviderName);
				connection = factory.CreateConnection();
			}

			Debug.Assert(connection != null);
			connection.ConnectionString = connectionString.ConnectionString;
			connection.Open();

			return connection;
		}

		public static long CreateOrder(string managerPurchaseOrder, int customerId, string vehicleJson)
		{
			using (var connection = GetConnection())
			{
				var result = connection.ExecuteScalar<long>("INSERT INTO orders (ordered_date, manager_po, customer_id, vehicle_json) VALUES (@ordered_date, @manager_po, @customer_id, @vehicle_json); SELECT last_insert_rowid();", new
				{
					ordered_date = DateTime.UtcNow.Ticks,
					manager_po = managerPurchaseOrder,
					customer_id = customerId,
                    vehicle_json = vehicleJson,
				});

				return result;
			}
		}

		public static long CreateTracking(long orderId)
		{
			var rand = new Random();
			using (var connection = GetConnection())
			{
				var result = connection.ExecuteScalar<long>("INSERT INTO order_tracking (order_id, expected_arrival_date) VALUES (@order_id, @expected_arrival_date); SELECT last_insert_rowid();", new
				{
					order_id = orderId,
					expected_arrival_date = DateTime.UtcNow.AddHours(rand.Next(1, 6)).Ticks
				});
				return result;
			}
		}

        public static long CreatePriceCheckSession(PriceCheckRequest priceCheckRequest)
        {
			using (var connection = GetConnection())
            {
                return connection.ExecuteScalar<long>("INSERT INTO price_check_session (json) VALUES (@json); SELECT last_insert_rowid();", new
                {
	                json = JsonConvert.SerializeObject(priceCheckRequest)
                });
            }
        }

        public static long CreateOrderPartsSession(OrderRequest orderRequest)
        {
            using (var connection = GetConnection())
            {
                return connection.ExecuteScalar<long>("INSERT INTO order_parts_session (json) VALUES (@json); SELECT last_insert_rowid();", new
                {
                    json = JsonConvert.SerializeObject(orderRequest)
                });
            }
        }

		public static List<TrackingDetail> GetRelatedOrders(int shop, string managerPurchaseOrder)
		{
			if (string.IsNullOrWhiteSpace(managerPurchaseOrder))
				return new List<TrackingDetail>();

			using (var connection = GetConnection())
			{
				var results = connection.Query<DbTracking>(
					@"SELECT
							order_tracking.id as tracking_id,
							order_tracking.order_id,
							order_tracking.expected_arrival_date,
							orders.manager_po,
							orders.ordered_date,
							orders.vehicle_json
						FROM
							orders
						INNER JOIN order_tracking ON order_tracking.order_id = orders.id
						WHERE orders.customer_id=@customer_id AND orders.manager_po=@po;",
					new
					{
						customer_id = shop,
						po = managerPurchaseOrder
					}).ToList();

				return results.Select(r => new TrackingDetail(r)).ToList();
			}
		}

		public static TrackingDetail GetTrackingStatus(int shop, long orderTrackingNumber)
		{
			using (var connection = GetConnection())
			{
				var result = connection.Query<DbTracking>(
					@"SELECT
							order_tracking.id as tracking_id,
							order_tracking.order_id,
							order_tracking.expected_arrival_date,
							orders.manager_po,
							orders.ordered_date,
							orders.vehicle_json
						FROM
							order_tracking
						INNER JOIN orders ON orders.id = order_tracking.order_id
						WHERE orders.customer_id=@customer_id AND order_tracking.id=@tracking_id;",
					new
					{
						customer_id = shop,
						tracking_id = orderTrackingNumber
					}).FirstOrDefault();

				return result != null ? new TrackingDetail(result) : null;
			}
		}

        public static PriceCheckRequest GetPriceCheckSession(long id)
		{
            using (var connection = GetConnection())
            { 
                return JsonConvert.DeserializeObject<PriceCheckRequest>(connection.Query<string>(
                @"SELECT json
                FROM price_check_session
                WHERE id = @id;",
                new
                {
                    id
                }).SingleOrDefault());
            }
        }

        public static OrderRequest GetOrderPartsSession(long id)
		{
	        using (var connection = GetConnection())
	        {
                return JsonConvert.DeserializeObject<OrderRequest>(connection.Query<string>(
                @"SELECT json
						FROM order_parts_session
						WHERE id = @id;",
			        new
			        {
				        id
			        }).SingleOrDefault());
	        }
        }

        public class DbTracking
		{
			public long tracking_id { get; set; }
			public long order_id { get; set; }
			public long ordered_date { get; set; }
			public long expected_arrival_date { get; set; }
            public string vehicle_json { get; set; }
			public string manager_po { get; set; }
		}
	}

	public class TrackingDetail
	{
		public TrackingDetail(ObjectStore.DbTracking tracking)
		{
			TrackingId = tracking.tracking_id;
			OrderId = tracking.order_id;
			Ordered = new DateTime(tracking.ordered_date, DateTimeKind.Utc);
			Arrives = new DateTime(tracking.expected_arrival_date, DateTimeKind.Utc);
			Status = DateTime.Now >= Arrives ? "Delivered" : "In-Transit";
			VehicleJson = tracking.vehicle_json;
			ManagerPurchaseOrder = tracking.manager_po;
		}

		public long TrackingId { get; }
		public long OrderId { get; }
		public string ManagerPurchaseOrder { get; }
		public DateTime Ordered { get; }
		public DateTime Arrives { get; }
		public string Status { get; }
		public string VehicleJson { get; }
	}
}