using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Identity.Data;

namespace HumanManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AuthService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public AccountDto Authenticate(AccountDto request)
        {
            var user = _accountRepository.CheckAccountByUsernameAndPassword(request);
            if (user == null)
            {
                return null;
            }

            return _mapper.Map<AccountDto>(user);
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            // Xác thực mật khẩu
            return inputPassword == storedPassword; // Chỉ là ví dụ, nên sử dụng hashing trong thực tế
        }
    }

}
