using AutoMapper;
using HumanManagement.Data.Repository;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAssetController:ControllerBase
    {     
        private readonly IEmployeeAssetRepository _employeeAssetRepository;
        private readonly IMapper _mapper;
        public EmployeeAssetController(IEmployeeAssetRepository employeeAssetRepository, IMapper mapper)
        {
            _employeeAssetRepository = employeeAssetRepository;
            _mapper = mapper;
        }
        [HttpGet("{employeeId}")]
        public IActionResult GetAssetsByEmployee(int employeeId)
        {
            var assets = _mapper.Map<List<AssetDto>>(_employeeAssetRepository.GetAssetsByEmployee(employeeId));
            return Ok(assets);
        }
        [HttpPost]
        public IActionResult CreateEmployeeAsset([FromBody] EmployeeAssetDto eaCreate)
        {
            if (eaCreate == null)
                return BadRequest(ModelState);
            if (!_employeeAssetRepository.CreateEmployeeAsset(_mapper.Map<EmployeeAsset>(eaCreate)))
            {
                return StatusCode(500, "Can't create");
            }
            return Ok("Create successfully");
        }
        [HttpPut]
        public IActionResult UpdateEmployeeAsset([FromBody] EmployeeAssetDto eaUpdate)
        {
            if (eaUpdate == null)
                return BadRequest(ModelState);
            if (!_employeeAssetRepository.CreateEmployeeAsset(_mapper.Map<EmployeeAsset>(eaUpdate)))
            {
                return StatusCode(500, "Can't update");
            }
            return Ok("Update successfully");
        }
        [HttpDelete]
        public IActionResult DeleteEmployeeAsset([FromBody] EmployeeAssetDto eaDelete)
        {
            if (eaDelete == null)
                return BadRequest(ModelState);
            if (!_employeeAssetRepository.DeleteEmployeeAsset(eaDelete.EmployeeId, eaDelete.AssetId))
            {
                ModelState.AddModelError("", "Can't delete");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete successfully");
        }


    }
}
