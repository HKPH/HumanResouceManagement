using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Identity.Data;

public interface IAuthService
{
    AccountDto Authenticate(AccountDto request);
}