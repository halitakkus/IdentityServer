using IdentityServer.AuthServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.AuthServer.Services
{
    public interface ICustomUserRepository
    {
        /// <summary>
        /// Email ve şifre doğrulama işlemi.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> Validate(string email, string password);

        /// <summary>
        /// İd değerine göre kullanıcı bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CustomUser> FindById(int id);
        /// <summary>
        /// Email parametresine göre kullanıcı bul.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<CustomUser> FindByEmail(string email);
    }
}
