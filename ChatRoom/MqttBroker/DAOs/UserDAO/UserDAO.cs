using ChatRoomModels.DB;
using MqttBroker.EFs;

namespace MqttBroker.DAOs.UserDAO
{
    /// <summary>
    /// 使用者DAO
    /// </summary>
    public class UserDAO : IUserDAO
    {
        private readonly DataContext _dataContext;

        public UserDAO(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        ///<inheritdoc/>
        public Users? Get(string userId)
        {
            return _dataContext.Users
                .Where(u => u.UserId == userId)
                .SingleOrDefault();
        }
    }
}
