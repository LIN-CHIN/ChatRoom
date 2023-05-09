using MqttBroker.Handlers.Interfaces;
using MqttBroker.Services.Interfaces;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttBroker
{
    public class Application
	{
		private readonly IWriteMessageHandler _consoleWithLogHandler;
		private static ManualResetEvent _manualResetEvent = new ManualResetEvent( false );
		IMqttServer _mqttServer;
		IMqttServerService _mqttServerService;

		public Application( IMqttServerService mqttServerService, IWriteMessageHandler consoleWithLogHandler )
		{
			_mqttServerService = mqttServerService;
			_consoleWithLogHandler = consoleWithLogHandler;
		}

		public async Task Start()
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( "Run application Start() " );
			await StartMqttServer();
			_consoleWithLogHandler.WriteConsoleWithInfoLog( "MQTT server已啟動" );
			_manualResetEvent.WaitOne();

			_consoleWithLogHandler.WriteConsoleWithInfoLog( "The MQTT Server has been shut down. " );
			_mqttServer.StopAsync().Wait();
		}

		/// <summary>
		/// 開啟Mqtt連線
		/// </summary>
		/// <returns></returns>
		public async Task StartMqttServer()
		{
			_mqttServer = await _mqttServerService.StartMqttServer();
		}
	}
}
