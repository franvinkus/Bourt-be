using Bourt.Data;
using Bourt.DTOs.Request;
using Bourt.DTOs.Response;
using Bourt.Models;
using Bourt.Services.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Bourt.Services.Interface
{
    public class PlaceServices: IPlaceServices
    {
        private readonly AppDbContext _db;

        public PlaceServices(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<PlaceGetResponseModel>> Get(PlaceGetRequestModel request, CancellationToken cancellationToken)
        {
            var places = await _db.Places
                .Include(x => x.OwnerName)
                .Select(x => new PlaceGetResponseModel
                {
                    PlaceId = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    City = x.City,
                    Address = x.Address,
                    OpenHour = x.OpenHour,
                    CloseHour = x.CloseHour,
                    OwnerName = x.OwnerName.Username
                })
                .ToListAsync(cancellationToken);

            return places;
        }

        public async Task<PlaceGetDetailsResponseModel> GetDetails(PlaceGetDetailsRequestModel request, CancellationToken cancellationToken)
        {
            var placeDetails = await _db.Places
                .Where(x => x.Id == request.PlaceId)
                .Select(x => new PlaceGetDetailsResponseModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    City = x.City,
                    Address = x.Address,
                    OpenHour = x.OpenHour,
                    CloseHour = x.CloseHour,
                    OwnerName = x.OwnerName.Username,
                    Courts = x.Courts
                    .Select(c => new Courts
                    {
                        CourtId = c.Id,
                        CourtName = c.Name,
                        CourtNumber = c.Number,
                        PricePerHour = c.PricePerHour,
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            return placeDetails;
        }

        public async Task<PlaceInsertResponseModel> Insert(Guid ownerId, PlaceInsertRequestModel request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.City))
            {
                return new PlaceInsertResponseModel
                {
                    Message = "City of the place located must be inserted"
                };
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new PlaceInsertResponseModel
                {
                    Message = "Name of the place must be inserted"
                };
            }

            var checkDuplicate = await _db.Places.FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower() && x.City.ToLower() == request.City.ToLower(), cancellationToken);

            if(checkDuplicate != null)
            {
                return new PlaceInsertResponseModel
                {
                    Message = $"There's a place named {request.Name} in {request.City}"
                };
            }
            else
            {

                if (string.IsNullOrWhiteSpace(request.Address))
                {
                    return new PlaceInsertResponseModel
                    {
                        Message = "Address of the place must be inserted"
                    };
                }

                if (string.IsNullOrWhiteSpace(request.OpenHour))
                {
                    return new PlaceInsertResponseModel
                    {
                        Message = "Open hour must be inserted"
                    };
                }

                if(string.IsNullOrWhiteSpace(request.CloseHour))
                {
                    return new PlaceInsertResponseModel
                    {
                        Message = "Close hour must be inserted"
                    };
                }

                var places = new Place
                {
                    Id = Guid.NewGuid(),
                    OwnerId = ownerId,
                    Name = request.Name,
                    Description = request.Description,
                    City = request.City,
                    Address = request.Address,
                    OpenHour = request.OpenHour,
                    CloseHour = request.CloseHour,
                    CreatedAt = DateTime.UtcNow,
                };

                _db.Places.Add(places);
                await _db.SaveChangesAsync(cancellationToken);

                return new PlaceInsertResponseModel
                {
                    Message = "Success"
                };
            }
        }

        public async Task<PlaceUpdateResponseModel> Update(PlaceUpdateRequestModel request,  CancellationToken cancellationToken)
        {
            var check = await _db.Places.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (check == null)
            {
                return new PlaceUpdateResponseModel
                {
                    Message = "Place not found"
                };
            }
            
            var checkDuplicate = await _db.Places.FirstOrDefaultAsync(x =>
                    x.Name.ToLower() == request.Name.ToLower() &&
                    x.City.ToLower() == request.City.ToLower() && 
                    x.Id != request.Id, cancellationToken);

            if (checkDuplicate != null)
            {
                return new PlaceUpdateResponseModel
                {
                    Message = "Name already taken by another place"
                };
            }
            else
            {
                check.Name = request.Name;
                check.Description = request.Description;
                check.City = request.City;
                check.Address = request.Address;
                check.OpenHour = request.OpenHour;
                check.CloseHour = request.CloseHour;
                check.UpdateAt = DateTime.UtcNow;

                await _db.SaveChangesAsync(cancellationToken);

                return new PlaceUpdateResponseModel
                {
                    Message = "Success"
                };
            }
        }

        public async Task<PlaceDeleteResponseModel> Delete(PlaceDeleteRequestModel request, CancellationToken cancellationToken)
        {
            var place = await _db.Places
                .Include(x => x.Courts)
                .FirstOrDefaultAsync(x => x.Id == request.PlaceId, cancellationToken);

            if(place == null)
            {
                return new PlaceDeleteResponseModel
                {
                    Message = "Id not found"
                };
            }

            if(place.Courts.Any() && place.Courts != null)
            {
                return new PlaceDeleteResponseModel
                {
                    Message = "Can't delete place when there are still active courts (delete Courts First)"
                };
            }
            else
            {
                _db.Places.Remove(place);

                await _db.SaveChangesAsync(cancellationToken);

                return new PlaceDeleteResponseModel
                {
                    Message = "Success"
                };
            }

        }
    }
}
