using Microsoft.EntityFrameworkCore;
using NordFish.Database.Entities;
using NordFish.Database.Enumes;
using NordFish.EntityFramework.Repository;
using NordFish.Services.UserServices.Models;
using NordFishServices.AuthenticationServices;
using NordFishServices.UserServices.Models;

namespace NordFishServices.UserServices
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<UserEntity> _genericRepository;
        private readonly IAuthenticationServices _authenticationServices;

        public UserService(IGenericRepository<UserEntity> genericRepository,
            IAuthenticationServices authenticationServices)
        {
            _genericRepository = genericRepository;
            _authenticationServices = authenticationServices;
        }

        public async Task<bool> Create(SignUpViewModel vm)
        {
            UserEntity userEntity = await _genericRepository.Table
                .FirstOrDefaultAsync(user => user.Email == vm.Email && user.Password == vm.Password);

            if(userEntity != null)
            {
                return false;
            }

            UserEntity dbRecord = new UserEntity()
            {
                CreatedOn = DateTime.Now,
                Email = vm.Email,
                Login = vm.Login,
                Password = vm.Password,
                Type = UserTypes.Client,
            };

            _genericRepository.Create(dbRecord);
            return true;
        }

        public async Task<UserEntity> GetByIdAsync(long id)
        {
            return await _genericRepository.Table
                .Include(user => user.Products)
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<bool> SignIn(SignInViewModel vm)
        {
            UserEntity userEntity = await _genericRepository.Table
                .FirstOrDefaultAsync(user => user.Email == vm.Email && user.Password == vm.Password);
              
            if(userEntity == null)
            {
                return false;
            }
            
            _authenticationServices.SignInAsync(userEntity);
            return true;
        }

        public async Task<bool> SignIn(string email, string password)
        {
            var user =  await _genericRepository.Table
                .FirstOrDefaultAsync(user => user.Email == email && user.Password == password);

            if (user == null)
            {
                return false;
            }

            _authenticationServices.SignInAsync(user);
            return true;
        }

        public void SignOut()
        {
            _authenticationServices.SignOutAsync();
        }

        public void Update(UserEntity userEntity)
        {
            _genericRepository.Update(userEntity);
        }

        public async Task<bool> SignUp(SignUpViewModel vm)
        {
            UserEntity userEntity = await _genericRepository.Table
                .FirstOrDefaultAsync(user => user.Email == vm.Email);

            if (userEntity != null)
            {
                return false;
            }

            UserEntity dbRecord = new UserEntity()
            {
                CreatedOn = DateTime.Now,
                Email = vm.Email,
                Login = vm.Login,
                Password = vm.Password,
                Type = UserTypes.Client,
            };

            _genericRepository.Create(dbRecord);
            _authenticationServices.SignInAsync(dbRecord);
            return true;
        }

        public void Delete(UserEntity userEntity)
        {
            userEntity.DeletedOn = DateTime.Now;
            Update(userEntity);
        }

        public async Task<UserEntity> GetByEmailAndPassword(string email, string passsword)
        {
            return await _genericRepository.Table
                .Include(user => user.Products)
                .FirstOrDefaultAsync(user => user.Email == email && user.Password == passsword);
        }

        public async Task<List<UserEntity>> GetByEmail(string email)
        {
            return await _genericRepository.Table
                .Include(user => user.Products)
                .Where(user => user.Email == email)
                .ToListAsync();
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await _genericRepository.Table.ToListAsync();
        }
    }
}