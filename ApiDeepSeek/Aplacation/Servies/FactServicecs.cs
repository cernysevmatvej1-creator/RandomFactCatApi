
using ApiDeepSeek.Aplacation.InterfaceServies;
using ApiDeepSeek.Common;
using ApiDeepSeek.Doamin.Interfais;
using ApiDeepSeek.Models;
using ApiDeepSeek.models;
namespace ApiDeepSeek.Aplacation.Servies
{
    public class FactServicecs : IFactService
    {
        private IUserRepotisory _userRepotisory;
        private IFactRepotisiory _factRepotisiory;
        public FactServicecs(IUserRepotisory userRepotisory, IFactRepotisiory factRepotisiory)
        {
            _userRepotisory = userRepotisory;
            _factRepotisiory = factRepotisiory;
        }

        public async Task<Result> FactSave(Answer answer)
        {
            try
            {
                var check = await _userRepotisory.GetUserId(answer.Name);
                if (check.Data == null)
                {
                    string id = Guid.NewGuid().ToString();  
                   await _userRepotisory.AddUser(new User {  UserId = id, UserName =  answer.Name });
                    answer.UserId = id;
                    await _factRepotisiory.SaveFact(answer);
                    return Result.Ok(); 
                    













                }
                answer.UserId = check.Data.UserId;
                await _factRepotisiory.SaveFact(answer);
                return Result.Ok();
            }
            catch(Exception ex) 
            {
                return Result.Fail(ex.Message); 
            }
        }

        public async  Task<Result<List<Answer>>> GetAnswers(string user)
        {
     
            if (user == null)
                return Result<List<Answer>>.Fail("Айди юсера нулевое");
            var listfact =await _factRepotisiory.GetFact(user);
            if(listfact.Data == null)
                return Result<List<Answer>>.Fail("фактов нету");
            return listfact;

        }
    }
}
