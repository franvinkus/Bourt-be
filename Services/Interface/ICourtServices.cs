using Bourt.DTOs.Request.Court;
using Bourt.DTOs.Response.Court;

namespace Bourt.Services.Implementation
{
    public interface ICourtServices
    {
        Task<CourtGetDetailsResponseModel> GetDetails(CourtGetDetailsRequestModel request, CancellationToken cancellationToken);
        Task<CourtInsertResponseModel> Insert(CourtInsertRequestModel request, CancellationToken cancellationToken);
        Task<CourtUpdateResponseModel> Update(CourtUpdateRequestModel request, CancellationToken cancellationToken);
        Task<CourtDeleteResponseModel> Delete(CourtDeleteRequestModel request, CancellationToken cancellationToken);
    }
}
