using Bourt.DTOs.Request.Booking;
using Bourt.DTOs.Response.Booking;

namespace Bourt.Services.Interface
{
    public interface IBookingServices
    {
        Task<BookinGetAllPagedResponseModel> GetAll(BookingGetAllRequestModel request, CancellationToken cancellation);
        Task<BookingGetCustomerPageModel> GetCustomerBooking(BookingGetCustomerRequestModel request, CancellationToken cancellation);
        Task<BookingGetOwnerPageModel> GetOwnerBooking(BookingGetOwnerRequestModel request, CancellationToken cancellation);
        Task<BookingInsertResponseModel> Insert(BookingInsertRequestModel request, CancellationToken cancellationToken);
        Task<List<BookingGetAvailableCourtHoursResponse>> GetAvailableCourtHours(Guid id, string date, CancellationToken cancellationToken);
        Task<BookingPatchStatusResponseModel> ToVerifying(BookingPatchStatusRequestModel request, CancellationToken cancelToken);
        Task<BookingPatchStatusResponseModel> ToConfirmed(BookingPatchStatusRequestModel request, CancellationToken cancelToken);
        Task<BookingPatchStatusResponseModel> ToCompleted(BookingPatchStatusRequestModel request, CancellationToken cancelToken);
        Task<BookingPatchStatusResponseModel> ToCancelled(BookingPatchStatusRequestModel request, CancellationToken cancelToken);
        Task<BookingPatchStatusResponseModel> RequestCancel(BookingPatchStatusRequestModel request, CancellationToken cancelToken);
    }
}
