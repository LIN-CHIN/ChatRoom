using ChatRoomModels.DB;
using MqttBroker.EFs;

namespace MqttBroker.DAOs.MessagesDAO
{
    public class MessageDAO : IMessageDAO
	{
		private readonly DataContext _dataContext;
		public MessageDAO( DataContext dataContext )
		{
			_dataContext = dataContext;
		}

		///<inheritdoc/>
		public Messages Insert( Messages message )
		{
			_dataContext.Messages.Add( message );
			_dataContext.SaveChanges();
			return message;
		}
	}
}
