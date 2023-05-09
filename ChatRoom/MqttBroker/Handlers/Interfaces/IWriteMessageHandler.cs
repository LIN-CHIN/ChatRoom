using MqttBroker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttBroker.Handlers.Interfaces
{
    /// <summary>
    /// 主要負責寫Console 和 Log的訊息
    /// </summary>
    public interface IWriteMessageHandler
    {
        /// <summary>
        /// 寫訊息至Console和Log
        /// </summary>
        /// <param name="message">要寫的訊息</param>
        public void WriteConsoleWithInfoLog(string message);

        /// <summary>
        /// 寫Log
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="logLevelEnum">Log嚴重等級</param>
        public void WriteLog(string message, LogLevelEnum logLevelEnum);
    }
}
