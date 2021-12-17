using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mitchell1.Online.Catalog.Host.Test
{
	[TestClass]
	public class MultiListTests
	{
		private MultiList<Base, Derived> list;

		[TestInitialize]
		public void TestInitialize()
		{
			list = new MultiList<Base, Derived>
			{
				new Base {BaseProperty = "IsBase"},
				new Derived {BaseProperty = "IsDerived", DerivedProperty = "extra"}
			};
		}

		[TestMethod]
		public void ConstructorTest()
		{
			list = new MultiList<Base, Derived>(new[]
			{
				new Derived {BaseProperty = "0", DerivedProperty = "extra0"},
				new Derived {BaseProperty = "1", DerivedProperty = "extra1"},
			});
			var derivedList = (IList<Derived>) list;

			Assert.AreEqual(2, list.Count);
			Assert.AreEqual("0", derivedList[0].BaseProperty);
			Assert.AreEqual("extra0", derivedList[0].DerivedProperty);
			Assert.AreEqual("1", derivedList[1].BaseProperty);
			Assert.AreEqual("extra1", derivedList[1].DerivedProperty);
		}

		[TestMethod]
		public void HappyPathTest()
		{
			Assert.AreEqual("IsBase", list[0].BaseProperty);
			Assert.AreEqual("IsDerived", list[1].BaseProperty);
			Assert.AreEqual("IsDerived", ((Derived)list[1]).BaseProperty);
			Assert.AreEqual("extra", ((Derived)list[1]).DerivedProperty);

			var derivedList = (IList<Derived>) list;
			Assert.AreEqual("IsDerived", derivedList[1].BaseProperty);
			Assert.AreEqual("extra", derivedList[1].DerivedProperty);

			var derivedReadOnlyList = (IReadOnlyList<Derived>) list;
			Assert.AreEqual("IsDerived", derivedReadOnlyList[1].BaseProperty);
			Assert.AreEqual("extra", derivedReadOnlyList[1].DerivedProperty);

			foreach (var item in list)
			{
				Assert.IsNotNull(item.BaseProperty);
			}
		}

		[TestMethod]
		public void InvalidCastException_for_IList_indexer_Test()
		{
			var derivedList = (IList<Derived>) list;
			Assert.ThrowsException<InvalidCastException>(() => derivedList[0].BaseProperty);
		}

		[TestMethod]
		public void InvalidCastException_for_IReadOnlyList_indexer_Test()
		{
			var derivedReadOnlyList = (IReadOnlyList<Derived>) list;
			Assert.ThrowsException<InvalidCastException>(() => derivedReadOnlyList[0].BaseProperty);
		}

		[TestMethod]
		public void InvalidCastException_for_IList_Test()
		{
			Assert.ThrowsException<InvalidCastException>(() =>
			{
				var derivedList = (IList<Derived>) list;
				foreach (var item in derivedList)
				{
					Assert.IsNotNull(item.BaseProperty);
				}
			});
		}

		[TestMethod]
		public void InvalidCastException_for_IReadOnlyList_Test()
		{
			Assert.ThrowsException<InvalidCastException>(() =>
			{
				var derivedReadOnlyList = (IReadOnlyList<Derived>) list;
				foreach (var item in derivedReadOnlyList)
				{
					Assert.IsNotNull(item.BaseProperty);
				}
			});
		}

		[TestMethod]
		public void IsReadOnly_Test()
		{
			Assert.IsFalse(((IList<Base>)list).IsReadOnly);
			Assert.IsFalse(((IList<Derived>)list).IsReadOnly);
		}

		[TestMethod]
		public void CopyTo_Test()
		{
			Base[] baseArray = new Base[3];

			list.CopyTo(baseArray, 1);

			Assert.IsNull(baseArray[0]);
			Assert.AreEqual("IsBase", baseArray[1].BaseProperty);
			Assert.AreEqual("IsDerived", baseArray[2].BaseProperty);

			Assert.ThrowsException<ArgumentException>(() => list.CopyTo(baseArray, 2));
		}

		[TestMethod]
		public void InvalidCastException_for_Derived_CopyTo_Test()
		{
			Derived[] derivedArray = new Derived[3];

			Assert.ThrowsException<InvalidCastException>(() => list.CopyTo(derivedArray, 1));
		}

		[TestMethod]
		public void Derived_CopyTo_Test()
		{
			Derived[] derivedArray = new Derived[3];
			list[0] = new Derived {BaseProperty = "new Derived", DerivedProperty = "extra0"};

			list.CopyTo(derivedArray, 1);

			Assert.IsNull(derivedArray[0]);
			Assert.AreEqual("new Derived", derivedArray[1].BaseProperty);
			Assert.AreEqual("extra0", derivedArray[1].DerivedProperty);
			Assert.AreEqual("IsDerived", derivedArray[2].BaseProperty);
			Assert.AreEqual("extra", derivedArray[2].DerivedProperty);

			Assert.ThrowsException<ArgumentException>(() => list.CopyTo(derivedArray, 2));
		}

		private class Derived : Base
		{
			public string DerivedProperty { get; set; }
		}

		private class Base
		{
			public string BaseProperty { get; set; }
		}
	}
}