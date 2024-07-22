using System;
using System.Collections.Generic;

namespace HumanManagement.Models;

public partial class ContractAllowanceDto
{
    public DateTime? CreateDate { get; set; }=DateTime.Now;

    public int AllowanceId { get; set; }

    public int? CreaterId { get; set; }

    public bool? Active { get; set; } = true;

    public int ContractTypeId { get; set; }

}
