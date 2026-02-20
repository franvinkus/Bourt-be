using Bourt.DTOs.Request;
using Bourt.DTOs.Response;

namespace Bourt.Services.Implementation
{
    public interface IPlaceServices
    {
        Task<List<PlaceGetResponseModel>> Get(PlaceGetRequestModel request, CancellationToken cancellationToken);
        Task<PlaceGetDetailsResponseModel> GetDetails(PlaceGetDetailsRequestModel request, CancellationToken cancellationToken);
        Task<PlaceInsertResponseModel> Insert(Guid ownerId, PlaceInsertRequestModel request, CancellationToken cacncellationToken);
        Task<PlaceUpdateResponseModel> Update(PlaceUpdateRequestModel request, CancellationToken canncellationToken);
        Task<PlaceDeleteResponseModel> Delete(PlaceDeleteRequestModel request, CancellationToken cancellationToken);
    }
}
