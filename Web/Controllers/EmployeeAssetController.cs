using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAssetController : ControllerBase
    {
        private readonly IEmployeeAssetRepository _employeeAssetRepository;
        private readonly IMapper _mapper;

        public EmployeeAssetController(IEmployeeAssetRepository employeeAssetRepository, IMapper mapper)
        {
            _employeeAssetRepository = employeeAssetRepository;
            _mapper = mapper;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetAssetsByEmployee(int employeeId)
        {
            var assets = await _employeeAssetRepository.GetAssetsByEmployeeAsync(employeeId);
            var assetDtos = _mapper.Map<List<AssetDto>>(assets);
            return Ok(assetDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsset([FromBody] EmployeeAssetDto eaCreate)
        {
            if (eaCreate == null)
                return BadRequest("Invalid data.");

            var employeeAsset = _mapper.Map<EmployeeAsset>(eaCreate);
            var createdEmployeeAsset = await _employeeAssetRepository.CreateEmployeeAssetAsync(employeeAsset);

            if (createdEmployeeAsset == null)
                return StatusCode(500, "Can't create");

            return StatusCode(201, "Create successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeAsset([FromBody] EmployeeAssetDto eaUpdate)
        {
            if (eaUpdate == null)
                return BadRequest("Invalid data.");

            var employeeAsset = _mapper.Map<EmployeeAsset>(eaUpdate);
            var updatedEmployeeAsset = await _employeeAssetRepository.UpdateEmployeeAssetAsync(employeeAsset);

            if (updatedEmployeeAsset == null)
                return StatusCode(500, "Can't update");

            return Ok("Update successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeAsset([FromBody] EmployeeAssetDto eaDelete)
        {
            if (eaDelete == null)
                return BadRequest("Invalid data.");

            var deletedEmployeeAsset = await _employeeAssetRepository.DeleteEmployeeAssetAsync(eaDelete.EmployeeId, eaDelete.AssetId);

            if (deletedEmployeeAsset == null)
                return StatusCode(500, "Can't delete");

            return Ok("Delete successfully");
        }
    }
}
