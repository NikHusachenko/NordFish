using NordFish.Database.Entities;
using NordFish.Services.UserServices.Models;
using NordFishServices.UserServices.Models;

namespace NordFishServices.UserServices
{
    public interface IUserService
    {
        Task<bool> Create(SignUpViewModel vm);
        void Delete(UserEntity userEntity);
        void Update(UserEntity userEntity);

        Task<UserEntity> GetByIdAsync(long id);
        Task<UserEntity> GetByEmailAndPassword(string email, string password);
        Task<List<UserEntity>> GetByEmail(string email);
        Task<List<UserEntity>> GetAllAsync();

        Task<bool> SignIn(SignInViewModel vm);
        Task<bool> SignIn(string email, string password);
        Task<bool> SignUp(SignUpViewModel vm);

        void SignOut();
    }
}
