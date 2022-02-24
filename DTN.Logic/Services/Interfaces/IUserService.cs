using DTN.Models;

namespace DTN.Logic.Services.Interfaces
{
    public interface IUserService
    {

        Task<ResponseModel<bool>> RegisterUser(UserModel user);
        Task<ResponseModel<UserInfoModel>> Authenticate(string username, string password);

    }
}