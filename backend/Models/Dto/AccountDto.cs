﻿namespace HumanManagement.Models.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        public string? Email { get; set; }

        public int? EmployeeId { get; set; }

        public bool? Active { get; set; }
    }
}
