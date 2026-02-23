using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Bourt.Data;
using Bourt.DTOs.Request.Court;
using Bourt.DTOs.Response.Court;
using Bourt.Models;
using Microsoft.EntityFrameworkCore;

namespace Bourt.Services.Implementation
{
    public class CourtServices: ICourtServices
    {
        public readonly AppDbContext _db;

        public CourtServices(AppDbContext db)
        {
            _db = db;
        }

        public async Task<CourtGetDetailsResponseModel> GetDetails(CourtGetDetailsRequestModel request, CancellationToken cancellationToken)
        {
            var courts = await _db.Courts
                .Where(p => p.Id == request.CourtId)
                .Select(x => new CourtGetDetailsResponseModel
                {
                    CourtName = x.Name,
                    CourtNumber = x.Number,
                    CourtPricePerHour = x.PricePerHour,
                })
                .FirstOrDefaultAsync(cancellationToken);

            return courts;
        }

        public async Task<CourtInsertResponseModel> Insert(CourtInsertRequestModel request, CancellationToken cancellationToken)
        {
            var checkCourt = await _db.Courts.Where(x => x.PlaceId == request.PlaceId)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);

            if (checkCourt != null)
            {
                return new CourtInsertResponseModel
                {
                    Message = "There's a Court with the same name"
                };
            }
            else
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return new CourtInsertResponseModel
                    {
                        Message = "Name of the court must be filled"
                    };
                }

                var courtNumbers = await _db.Courts.Select(x => x.Number).ToListAsync(cancellationToken);

                var checkNumbers = await _db.Courts
                    .AnyAsync(x => x.Number == request.Number || courtNumbers.Contains(request.Number), cancellationToken);

                if (checkNumbers)
                {
                    return new CourtInsertResponseModel
                    {
                        Message = "Court's number must be uniqeu"
                    };
                }

                if (request.Number <= 0)
                {
                    return new CourtInsertResponseModel
                    {
                        Message = "Court's number must be filled"
                    };
                }

                if (request.PricePerHour <= 0)
                {
                    return new CourtInsertResponseModel
                    {
                        Message = "Court's price must be filled"
                    };
                }

                var newCourt = new Court
                {
                    PlaceId = request.PlaceId,
                    Name = request.Name,
                    Number = request.Number,
                    PricePerHour = request.PricePerHour,
                    CreatedAt = DateTime.UtcNow,
                };

                _db.Courts.Add(newCourt);
                await _db.SaveChangesAsync(cancellationToken);

                return new CourtInsertResponseModel
                {
                    Message = "Success"
                };
            }
        }

        public async Task<CourtUpdateResponseModel> Update(CourtUpdateRequestModel request, CancellationToken cancellationToken)
        {
            var checkCourtExist = await _db.Courts.FirstOrDefaultAsync(x => x.Id == request.CourtId, cancellationToken);

            if (checkCourtExist == null)
            {
                return new CourtUpdateResponseModel
                {
                    Message = "Court not found"
                };
            }
            else
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return new CourtUpdateResponseModel
                    {
                        Message = "Name of the court must be filled"
                    };
                }

                var courtNumbers = await _db.Courts.Select(x => x.Number).ToListAsync(cancellationToken);

                var checkNumbers = await _db.Courts
                    .AnyAsync(x => x.Number == request.Number || courtNumbers.Contains(request.Number) && x.Id != request.CourtId, cancellationToken);

                if (checkNumbers)
                {
                    return new CourtUpdateResponseModel
                    {
                        Message = "Court's number must be uniqeu"
                    };
                }

                if (request.Number <= 0)
                {
                    return new CourtUpdateResponseModel
                    {
                        Message = "Court's number must be filled"
                    };
                }

                if (request.PricePerHour <= 0)
                {
                    return new CourtUpdateResponseModel
                    {
                        Message = "Court's price must be filled"
                    };
                }

                checkCourtExist.Name = request.Name;
                checkCourtExist.Number = request.Number;
                checkCourtExist.PricePerHour = request.PricePerHour;
                checkCourtExist.UpdatedAt = DateTime.UtcNow;

                await _db.SaveChangesAsync(cancellationToken);

                return new CourtUpdateResponseModel
                {
                    Message = "Success"
                };
            }
        }

        public async Task<CourtDeleteResponseModel> Delete(CourtDeleteRequestModel request,  CancellationToken cancellationToken)
        {
            var checkId = await _db.Courts.FirstOrDefaultAsync(x => x.Id == request.CourtId, cancellationToken);

            if(checkId == null)
            {
                return new CourtDeleteResponseModel
                {
                    Message = "Id not found"
                };
            }
            else
            {
                _db.Remove(checkId);
                await _db.SaveChangesAsync(cancellationToken);

                return new CourtDeleteResponseModel
                {
                    Message = "Success"
                };
            }
        }
    }
}
