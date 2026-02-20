using Bourt.DTOs.Request.Place;
using Bourt.DTOs.Response;
using Bourt.Enums;
using Bourt.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bourt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : Controller
    {
        private readonly IPlaceServices _service;
        public PlaceController(IPlaceServices services)
        {
            _service = services;
        }

        [HttpGet("get-place")]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] PlaceGetRequestModel model, CancellationToken cancellationToken)
        {
            var result = await _service.Get(model, cancellationToken);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("get-place/{id}")]
        [Authorize]
        public async Task<IActionResult> GetDetails([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = new PlaceGetDetailsRequestModel
            {
                PlaceId = id
            };

            var result = await _service.GetDetails(model, cancellationToken);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("insert-place")]
        [Authorize(Roles = nameof(UserRole.Owner))]
        public async Task<IActionResult> Insert([FromBody] PlaceInsertRequestModel model, CancellationToken cancellationToken)
        {
            var ownerIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if(ownerIdString == null)
            {
                return Unauthorized();
            }

            var ownerId = Guid.Parse(ownerIdString);

            var result = await _service.Insert(ownerId, model, cancellationToken);

            if(result.Message.ToLower() == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("update-place/{id}")]
        [Authorize(Roles = nameof(UserRole.Owner))]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] PlaceUpdateRequestModel model, CancellationToken cancellationToken)
        {
            model.Id = id;

            var result = await _service.Update(model, cancellationToken);

            if (result.Message.ToLower() == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("delete-place/{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = new PlaceDeleteRequestModel
            {
                PlaceId = id
            };

            var result = await _service.Delete(model, cancellationToken);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
