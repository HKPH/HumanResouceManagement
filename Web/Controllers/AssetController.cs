using AutoMapper;
using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using HumanManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController:ControllerBase
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        public AssetController(IAssetRepository assetRepository, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAssets()
        {
            var assets= _mapper.Map<List<AssetDto>>(_assetRepository.GetAssets());
            return Ok(assets);
        }
        [HttpGet("{assetId}")]
        public IActionResult GetAsset(int assetId)
        {
            var asset=_mapper.Map<AssetDto>(_assetRepository.GetAssetById(assetId));
            return Ok(asset);
        }
        [HttpGet("active/{active}")]
        public IActionResult GetAssetsByActive(bool active)
        {
            var assets= _mapper.Map<List<AssetDto>>(_assetRepository.GetAssetsByActive(active));
            return Ok(assets);
        }
        [HttpPost]
        public IActionResult CreateAsset([FromBody]AssetDto asset)
        {
            if (asset == null)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_assetRepository.CreateAsset(_mapper.Map<Asset>(asset)))
            {
                ModelState.AddModelError("", "Can't create asset");
                return StatusCode(500,ModelState);
            }
            return Ok("Create asset succesfully");
        }
        [HttpPut]
        public IActionResult UpdateAsset([FromBody] AssetDto asset)
        {
            if (asset == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_assetRepository.UpdateAsset(_mapper.Map<Asset>(asset)))
            {
                ModelState.AddModelError("", "Can't update asset");
                return StatusCode(500, ModelState);
            }
            return Ok("Update asset succesfully");
        }
        [HttpDelete("{assetId}")]
        public IActionResult DeleteAsset(int assetId)
        {
            if(!_assetRepository.DeleteAsset(assetId))
            {
                ModelState.AddModelError("", "Can't delete asset");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete asset succesfully");
        }

    }
}
