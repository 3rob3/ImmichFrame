using ImmichFrame.Core.Api;
using ImmichFrame.Core.Interfaces;
using ImmichFrame.Core.Logic;
using Microsoft.AspNetCore.Mvc;

namespace ImmichFrame.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly ILogger<AssetController> _logger;
        private IImmichFrameLogic _logic;

        public AssetController(ILogger<AssetController> logger, ISettings settings)
        {
            _logger = logger;
            _logic = new ImmichFrameLogic(settings);
        }

        [HttpGet(Name = "GetAsset")]
        public async Task<AssetResponseDto> GetAsset()
        {
            return await _logic.GetNextAsset();
        }

        [HttpGet("{id}", Name = "GetImage")]
        public async Task<FileResponse> GetImage(Guid id)
        {
            var x = await _logic.GetImage(id);
            return x;
        }
    }
}
