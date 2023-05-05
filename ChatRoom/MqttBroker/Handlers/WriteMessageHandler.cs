using Microsoft.Extensions.Logging;
using MqttBroker.Enums;
using MqttBroker.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttBroker.Handlers
{
    /// <summary>
    /// 負責Console和Log訊息的同步
    /// </summary>
    public class WriteMessageHandler : IWriteMessageHandler
	{
		private readonly ILogger<WriteMessageHandler> _logger;
		public WriteMessageHandler( ILogger<WriteMessageHandler> logger )
		{
			_logger = logger;
		}

		///<inheritdoc/>
		public void WriteConsoleWithLog( string message )
		{
			_logger.LogInformation( message );
			Console.WriteLine( message );
		}

		///<inheritdoc/>
		public void WriteLog( string message, LogLevelEnum logLevelEnum )
		{
			if( logLevelEnum == LogLevelEnum.Info ) {
				_logger.LogInformation( message );
			}
			else if( logLevelEnum == LogLevelEnum.Error ) {
				_logger.LogError( message );
			}
		}
	}
}
