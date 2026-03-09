using System.Security.Claims;
using Bourt.DTOs.Request.Booking;
using Bourt.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Bourt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingServices _services;

        public BookingController(IBookingServices services)
        {
            _services = services;
        }

        [HttpGet("get-all")]
        [Authorize (Roles = nameof(Enums.UserRole.Admin))]
        public async Task<IActionResult> GetAll([FromQuery] BookingGetAllRequestModel model, CancellationToken cancellationToken)
        {
            var result = await _services.GetAll(model, cancellationToken);

            return Ok(result);
        }

        [HttpGet("get-customer-booking")]
        [Authorize(Roles = nameof(Enums.UserRole.Customer))]
        public async Task<IActionResult> GetCustomerBooking([FromQuery] BookingGetCustomerRequestModel model, CancellationToken cancellationToken)
        {
            var CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            model.CustomerId = Guid.Parse(CustomerId);

            var result = await _services.GetCustomerBooking(model, cancellationToken);

            return Ok(result);
        }

        [HttpGet("get-owner-booking/{placeId}")]
        [Authorize(Roles = nameof(Enums.UserRole.Owner))]
        public async Task<IActionResult> GetOwnerBooking([FromRoute] Guid placeId, [FromQuery] BookingGetOwnerRequestModel model, CancellationToken cancellationToken)
        {
            var OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            model.OwnerId = Guid.Parse(OwnerId);
            model.PlaceId = placeId;

            var result = await _services.GetOwnerBooking(model, cancellationToken);

            return Ok(result);
        }

        [HttpPost("insert-booking/{Id}")]
        [Authorize (Roles = nameof(Enums.UserRole.Customer))]
        public async Task<IActionResult> Insert([FromRoute] Guid id, [FromBody] BookingInsertRequestModel model, CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "User token is invalid or expired." });
            }

            model.UserId = Guid.Parse(userId);
            model.CourtId = id;

            var result = await _services.Insert(model, cancellationToken);

            if (result.Message.ToLower() == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPatch("to-pending/{id}")]
        [Authorize(Roles = nameof(Enums.UserRole.Owner))]
        public async Task<IActionResult> ToVerifying([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = new BookingPatchStatusRequestModel
            {
                BookingId = id,
            };

            var result = await _services.ToVerifying(model, cancellationToken);

            if (result.Message.ToLower() == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPatch("to-confirmed/{id}")]
        [Authorize(Roles = nameof(Enums.UserRole.Owner))]
        public async Task<IActionResult> ToConfirmed([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = new BookingPatchStatusRequestModel
            {
                BookingId = id,
            };

            var result = await _services.ToConfirmed(model, cancellationToken);

            if (result.Message.ToLower() == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPatch("to-completed/{id}")]
        [Authorize(Roles = nameof(Enums.UserRole.Owner))]
        public async Task<IActionResult> ToCompleted([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = new BookingPatchStatusRequestModel
            {
                BookingId = id,
            };

            var result = await _services.ToCompleted(model, cancellationToken);

            if (result.Message.ToLower() == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPatch("to-cancelled/{id}")]
        [Authorize(Roles = nameof(Enums.UserRole.Owner))]
        public async Task<IActionResult> ToCancelled([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = new BookingPatchStatusRequestModel
            {
                BookingId = id,
            };

            var result = await _services.ToCancelled(model, cancellationToken);

            if (result.Message.ToLower() == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPatch("request-cancel/{id}")]
        [Authorize(Roles = nameof(Enums.UserRole.Owner))]
        public async Task<IActionResult> RequestCancel([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var model = new BookingPatchStatusRequestModel
            {
                BookingId = id,
            };

            var result = await _services.RequestCancel(model, cancellationToken);

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
