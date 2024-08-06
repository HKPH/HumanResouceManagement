using HumanManagement.Models.Dto;
using HumanManagement.Services;
using HumanManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IContractReportService _contractReport;
        private readonly IRewardAndDisciplineReportService _rewardAndDisciplineReportService;
        private readonly IEmployeeContractReportService _employeeContractReportService;
        private readonly IAssetReportService _assetReportService;

        public ReportController(IContractReportService contractReport,
            IRewardAndDisciplineReportService rewardAndDisciplineReportService, 
            IEmployeeContractReportService employeeContractReportService,
            IAssetReportService assetReportService)
        {
            _contractReport = contractReport;
            _rewardAndDisciplineReportService = rewardAndDisciplineReportService;
            _employeeContractReportService = employeeContractReportService;
            _assetReportService = assetReportService;
        }
        [HttpGet("active-contracts")]
        public async Task<IActionResult> GetContractTypesWithActiveEmployeeContracts()
        {
            var result = await _contractReport.GetContractTypesReportAsync();
            return Ok(result);
        }

        [HttpGet("monthly-report")]
        public async Task<IActionResult> GetMonthlyReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Ngày bắt đầu không thể lớn hơn ngày kết thúc.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = await _rewardAndDisciplineReportService.GetReportByMonth(startDate, endDate);
            return Ok(report);
        }

        [HttpGet("employee-contract-report")]
        public async Task<IActionResult> GetEmployeeContractReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Ngày bắt đầu không thể lớn hơn ngày kết thúc.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var report = await _employeeContractReportService.GetEmployeeContractReportAsync(startDate, endDate);
            return Ok(report);
        }


        [HttpGet("asset-report")]
        public async Task<IActionResult> GetAssetReport()
        {
            var assetReport = await _assetReportService.GetAssetReportAsync();
            return Ok(assetReport);
        }
    }
}
