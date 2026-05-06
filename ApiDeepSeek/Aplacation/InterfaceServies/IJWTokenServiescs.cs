using ApiDeepSeek.models;

namespace ApiDeepSeek.Aplacation.InterfaceServies
{
    public interface IJWTokenServiescs
    {
        Task<string> SignAnonimal(User user);
    }
}
