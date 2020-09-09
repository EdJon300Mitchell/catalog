
namespace Mitchell1.Browser.Interfaces
{
	/// <summary>
	/// Represents an IFrame
	/// </summary>
	public class WebFrame
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public WebFrame(long identifier, string name)
		{
			Identifier = identifier;
			Name = name;
		}

		/// <summary>
		/// Identifier of frame
		/// </summary>
		public long Identifier { get; private set; }

		/// <summary>
		/// Name of frame
		/// </summary>
		public string Name { get; private set; }
	}
}
