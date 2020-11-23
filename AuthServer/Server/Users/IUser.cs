using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassModel.Connection.Reponsitory;
using ClassModel.Model.Fiber;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace AuthServer.Server.Users
{
    public interface IUser: IReponsitory<User>
    {
        dynamic uHashPass(string pass);
        dynamic genToken(User user);
        dynamic login(User users);
        dynamic updateUserNonePassword(User users);
        Boolean checkUsername(User us);
    }
}
