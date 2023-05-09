using ChatRoomModels;
using MqttBroker.EFs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
