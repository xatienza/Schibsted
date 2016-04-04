using System;
namespace Schibsted.Service.Security
{
    public interface ISecurityService
    {
        Schibsted.Domain.Model.Security.Users.User Authenticate(string username, string password);
    }
}
