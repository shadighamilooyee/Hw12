using HW12.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Interfaces.IService
{
    public interface IAuthentication
    {
        bool Login(string username, string password);
        void Register(string username, string password, RoleEnum role);
    }
}
