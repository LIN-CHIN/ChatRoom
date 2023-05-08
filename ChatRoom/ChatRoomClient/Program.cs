using ChatRoom;
using ChatRoomClient.Handlers.Interfaces;
using ChatRoomClient.Handlers;
using ChatRoomClient.MqttService;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ChatRoomClient.MqttService.Interfaces;

namespace ChatRoomClient
{
    internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var logger = LogManager.GetCurrentClassLogger();

			try {
				var serviceProvider = new ServiceCollection()
							 .AddLogging( x =>
							 {
								 // configure Logging with NLog
								 x.ClearProviders();
								 x.SetMinimumLevel( Microsoft.Extensions.Logging.LogLevel.Trace );
								 x.AddNLog( "./NLog.xml" );
							 } )
							 .AddSingleton<ILogHandler, LogHandler>()
							 .AddScoped<FrmStart>()
							 .AddScoped<IMqttClientService, MqttClientService>()
							 .BuildServiceProvider();

				ApplicationConfiguration.Initialize();
				Application.Run( serviceProvider.GetService<FrmStart>() );
			}
			catch( Exception ex ) {
				logger.Error( ex, "Stopped program because of exception" );
				throw ex;
			}
			finally {
				//�ݽT�w�b�����ɡA��nlog����
				LogManager.Shutdown();
			}

		}
	}
}