using ChatRoomModels.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttBroker.DAOs.UserDAO
{
    /// <summary>
    /// 使用者DAO Interface
    /// </summary>
    public interface IUserDAO
    {
        /// <summary>
        /// 根據帳號取得User
        /// </summary>
        /// <param name="userId">使用者帳號</param>
        /// <returns></returns>
        public Users? Get(string userId);
    }
}
