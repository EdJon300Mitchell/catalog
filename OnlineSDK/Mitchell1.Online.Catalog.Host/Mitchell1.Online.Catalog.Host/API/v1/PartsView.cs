namespace Mitchell1.Online.Catalog.Host.API.v1
{
	public class PartsView : CustomWebPageControllerHandleForbidden
	{
		public PartsView(string url) => Url = url;

		protected override string Url { get; }
		protected override string ActionLabel { get; } = "transfer";

		public string Response { get; private set; }

		protected override bool Action(object[] objects)
		{
			Response = objects != null && objects.Length == 2 && objects[1] is string response ? response : null;
			return Response != null;
		}
	}
}