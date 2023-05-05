using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttBroker.Services.Interfaces
{
    public interface IMqttServerService
    {
        /// <summary>
        /// 開啟Mqtt連線
        /// </summary>
        /// <returns></returns>
        Task<IMqttServer> StartMqttServer();

    }
}
