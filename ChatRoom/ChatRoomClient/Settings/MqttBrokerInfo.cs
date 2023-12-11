using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomClient.Settings
{
    /// <summary>
    /// Mqtt Borker 資訊
    /// </summary>
    public class MqttBrokerInfo
    {
        /// <summary>
        /// 主機位置
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// port號
        /// </summary>
        public int Port { get; set; }
    }
}
