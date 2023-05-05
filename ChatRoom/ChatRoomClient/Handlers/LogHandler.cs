using ChatRoomClient.Handlers.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomClient.Handlers
{
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
