
using ApiDeepSeek.models;
using ApiDeepSeek.Common;
using ApiDeepSeek.Models;
namespace ApiDeepSeek.Aplacation.InterfaceServies
{
    public interface IFactService
    {
        Task<Result> FactSave(Answer answer);
        Task<Result<List<Answer>>> GetAnswers(string username);
    }
}
