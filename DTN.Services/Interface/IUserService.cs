using DTN.Models;
using DTN.Services.Utilities;


namespace DTN.Services.Interface
{
    public interface IUserService
    {

        Task<ResponseModel<bool>> RegisterUser(UserModel user);
        Task<ResponseModel<UserInfoModel>> Authenticate(string username, string password);

    }
}