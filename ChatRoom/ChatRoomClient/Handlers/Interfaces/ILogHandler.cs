namespace ChatRoomClient.Handlers.Interfaces
{
	/// <summary>
	/// Logger Interface
	/// </summary>
	public interface ILogHandler
	{
		/// <summary>
		/// 寫入Info等級的Log
		/// </summary>
		/// <param name="message"></param>
		public void WriteInfo( string message );

		/// <summary>
		/// 寫入Error等級的Log
		/// </summary>
		/// <param name="message"></param>
		public void WriteError( string message );
	}
}
