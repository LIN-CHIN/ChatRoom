using ChatRoomModels.DB;

namespace MqttBroker.DAOs.MessagesDAO
{
    public interface IMessageDAO
	{
		/// <summary>
		/// 新增訊息
		/// </summary>
		/// <param name="message">訊息實體</param>
		/// <returns></returns>
		public Messages Insert(Messages message);
	}
}
