using Bourt.DTOs.Request.Court;
using Bourt.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bourt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtController : Controller
    {
        private readonly ICourtServices _service;
        public CourtController(ICourtServices services)
        {
            _service = services;
        }

        [HttpGet("get-court-details/{Id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = new CourtGetDetailsRequestModel
            {
                CourtId = id
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

        [HttpPost("insert-court")]
        [Authorize(Roles = nameof(Enums.UserRole.Owner))]
        public async Task<IActionResult> Insert([FromBody] CourtInsertRequestModel model, CancellationToken cancellationToken)
        {
            var result = await _service.Insert(model, cancellationToken);

            if(result.Message.ToLower() == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("update-court")]
        [Authorize(Roles = nameof(Enums.UserRole.Owner))]
        public async Task<IActionResult> Update([FromBody] CourtUpdateRequestModel model, CancellationToken cancellationToken)
        {
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

        [HttpDelete("delete-court/{id}")]
        [Authorize(Roles = nameof(Enums.UserRole.Owner))]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = new CourtDeleteRequestModel
            {
                CourtId = id
            };
            
            var result = await _service.Delete(model, cancellationToken);

            if (result.Message.ToLower() == "success")
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
