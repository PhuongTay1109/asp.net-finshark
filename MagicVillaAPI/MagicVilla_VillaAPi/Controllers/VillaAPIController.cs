
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    // Lớp này kế thừa từ ControllerBase, là lớp cơ sở cho các controller trong ASP.NET Core.
    // ControllerBase cung cấp các phương thức hỗ trợ cho việc xây dựng các API.
    [ApiController]
    [Route("api/v1/VillaAPI")] // nên định nghĩa cụ thể như vậy thay vì dùng [controller] 
    public class VillaAPIController : ControllerBase
    {
        // dependency injection cái logger
        // đưa VillaAPIController vào trong ILogger<VillaAPIController> là để xác định rõ ràng rằng muốn sử dụng
        // 1 logger được cấu hình cho class VillaAPIController cụ thể, giúp quản lý và phân loại log hiệu quả trong ứng dụng.
        // quy ước đặt _ trước biến private
        // readonly - tương tự final trong java
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IVillaService _villaService;

        public VillaAPIController(ILogger<VillaAPIController> logger, IVillaService villaService)
        {
            _logger = logger;
            _villaService = villaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");
            return Ok(_villaService.GetAllVillas());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogInformation("Get Villa Error with Id " + id);
                return BadRequest();
            }
            var villa = _villaService.GetVillaById(id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            if (_villaService.VillaExists(villaDTO.Name))
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            _villaService.CreateVilla(villaDTO);
            return Ok(villaDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            _villaService.DeleteVilla(id);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }

            _villaService.UpdateVilla(villaDTO);
            return NoContent();
        }
    }
}
