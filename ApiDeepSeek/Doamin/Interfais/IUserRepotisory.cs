using ApiDeepSeek.Common;
using ApiDeepSeek.models;

namespace ApiDeepSeek.Doamin.Interfais
{
    public interface IUserRepotisory
    {
        Task<Result<User>> GetUserId(string username);
        Task<Result> AddUser(User user);
    }
}
