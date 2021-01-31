using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace MES.WinformClient
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public static class UserInfo
    {
        public static UserModel  User { get; set; }

        public static string Token => User?.Token;

        public static Metadata Headers => new Metadata { { "Authorization", $"Bearer {User?.Token }" }};
    }
}
