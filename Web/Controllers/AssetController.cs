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
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;

        public AssetController(IAssetRepository assetRepository, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var assets = await _assetRepository.GetAssetsAsync();
            var assetDtos = _mapper.Map<List<AssetDto>>(assets);
            return Ok(assetDtos);
        }

        [HttpGet("{assetId}")]
        public async Task<IActionResult> GetAsset(int assetId)
        {
            var asset = await _assetRepository.GetAssetByIdAsync(assetId);
            if (asset == null)
            {
                return NotFound();
            }

            var assetDto = _mapper.Map<AssetDto>(asset);
            return Ok(assetDto);
        }

        [HttpGet("active/{active}")]
        public async Task<IActionResult> GetAssetsByActive(bool active)
        {
            var assets = await _assetRepository.GetAssetsByActiveAsync(active);
            var assetDtos = _mapper.Map<List<AssetDto>>(assets);
            return Ok(assetDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] AssetDto assetDto)
        {
            if (assetDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asset = _mapper.Map<Asset>(assetDto);
            var createdAsset = await _assetRepository.CreateAssetAsync(asset);

            if (createdAsset == null)
            {
                return StatusCode(500, "Can't create");
            }

            var createdAssetDto = _mapper.Map<AssetDto>(createdAsset);

            return CreatedAtAction(
                actionName: nameof(GetAsset),
                routeValues: new { assetId = createdAsset.Id },
                value: createdAssetDto
            );
        }

        [HttpPut("{assetId}")]
        public async Task<IActionResult> UpdateAsset(int assetId, [FromBody] AssetDto assetDto)
        {
            if (assetDto == null)
            {
                return BadRequest(ModelState);
            }

            if (assetId != assetDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asset = _mapper.Map<Asset>(assetDto);
            var updatedAsset = await _assetRepository.UpdateAssetAsync(asset);

            if (updatedAsset == null)
            {
                return StatusCode(500, "Không thể cập nhật tài sản.");
            }

            return Ok("Cập nhật tài sản thành công.");
        }

        [HttpDelete("{assetId}")]
        public async Task<IActionResult> DeleteAsset(int assetId)
        {
            var deletedAsset = await _assetRepository.DeleteAssetAsync(assetId);

            if (deletedAsset == null)
            {
                return StatusCode(500, "Không thể xóa tài sản.");
            }

            return Ok("Xóa tài sản thành công.");
        }
    }
}
