using ChatRoomClient.Handlers.Interfaces;
using Microsoft.Extensions.Logging;

namespace ChatRoomClient.Handlers
{
	/// <summary>
	/// Logger 
	/// </summary>
	public class LogHandler : ILogHandler
	{
		public readonly ILogger<LogHandler> _logger;
		public LogHandler( ILogger<LogHandler> logger )
		{
			_logger = logger;
		}

		///<inheritdoc/>
		public void WriteInfo( string message )
		{
			_logger.LogInformation( message );
		}

		///<inheritdoc/>
		public void WriteError( string message )
		{
			_logger.LogError( message );
		}
	}
}
