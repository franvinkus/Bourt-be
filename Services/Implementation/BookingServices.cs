using System.Linq;
using Bourt.Data;
using Bourt.DTOs.Request.Booking;
using Bourt.DTOs.Response.Booking;
using Bourt.Models;
using Bourt.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Bourt.Services.Implementation
{
    public class BookingServices: IBookingServices
    {
        private readonly AppDbContext _db;
        public BookingServices(AppDbContext db)
        {
            _db = db;
        }

        //For Admin
        public async Task<BookinGetAllPagedResponseModel> GetAll(BookingGetAllRequestModel request, CancellationToken cancellationToken)
        {
            var query = _db.Bookings.AsQueryable();

            if (!string.IsNullOrEmpty(request.InputString))
            {
                query = query.Where(x => x.User.Username.ToLower().Contains(request.InputString.ToLower()) || x.Court.Place.Name.ToLower().Contains(request.InputString.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.OrderDate))
            {
                if(request.OrderDate == "ASC")
                {
                    query = query.OrderBy(x => x.Date);
                }
                else if(request.OrderDate == "DESC")
                {
                    query = query.OrderByDescending(x => x.Date);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedAt);
            }

            var countData = await query.CountAsync(cancellationToken);

            var bookings = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new BookingGetAllResponseModel
                {
                    BookingId = x.Id,
                    CourtId = x.CourtId,
                    CourtName = x.Court.Name,
                    CourtNumber = x.Court.Number,
                    PlaceId = x.Court.PlaceId,
                    PlaceName = x.Court.Place.Name,
                    UserId = x.UserId,
                    Username = x.User.Username,
                    Date = x.Date,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                })
                .ToListAsync(cancellationToken);

            return new BookinGetAllPagedResponseModel
            {
                TotalData = countData,
                TotalPages = (int)Math.Ceiling(countData / (double)request.PageSize),
                CurrentPage = request.PageNumber,
                datas = bookings,
            };
        }

        //For Customer
        public async Task<BookingGetCustomerPageModel> GetCustomerBooking(BookingGetCustomerRequestModel request, CancellationToken cancellationToken)
        {
            var query = _db.Bookings.AsQueryable();

            query = query.Where(x => x.UserId == request.CustomerId);

            if (!string.IsNullOrWhiteSpace(request.StringInput))
            {
                var searchKeyword = request.StringInput.ToLower();
                query = query.Where(x => x.Court.Name.ToLower().Contains(request.StringInput) || x.Court.Place.Name.ToLower().Contains(request.StringInput));
            }

            if (!string.IsNullOrEmpty(request.OrderDate))
            {
                if (request.OrderDate == "ASC")
                {
                    query = query.OrderBy(x => x.Date);
                }
                else if (request.OrderDate == "DESC")
                {
                    query = query.OrderByDescending(x => x.Date);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedAt);
            }

            var totalDatas = await query.CountAsync(cancellationToken);

            var customerBooking = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new BookingGetCustomerResponseModel
                    {
                        BookingId = x.Id,
                        CourtNumber = x.Court.Number,
                        PlaceName = x.Court.Place.Name,
                        CourtName = x.Court.Name,
                        Date = x.Date,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                    }).ToListAsync(cancellationToken);

            return new BookingGetCustomerPageModel
            {
                TotalData = totalDatas,
                TotalPages = (int)Math.Ceiling(totalDatas / (double)request.PageSize),
                CurrentPage = request.PageNumber,
                Datas = customerBooking
            };
        }

        public async Task<BookingGetOwnerPageModel> GetOwnerBooking(BookingGetOwnerRequestModel request, CancellationToken cancellationToken)
        {
            var query = _db.Bookings.AsQueryable();

            var placeId = await _db.Places
                .Where(x => x.OwnerId == request.OwnerId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            query = query.Where(x => x.Court.PlaceId == placeId && x.Court.Place.OwnerId == request.OwnerId);

            if (!string.IsNullOrEmpty(request.StringInput))
            {
                query = query.Where(x => x.Court.Name == request.StringInput || x.User.Username == request.StringInput);
            }

            if (!string.IsNullOrEmpty(request.OrderDate))
            {
                if (request.OrderDate == "ASC")
                {
                    query = query.OrderBy(x => x.Date);
                }
                else if (request.OrderDate == "DESC")
                {
                    query = query.OrderByDescending(x => x.Date);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.Date);
            }

            var totalDatas = await query.CountAsync(cancellationToken);

            var ownerBooking = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new BookingGetOwnerResponseModel
                {
                    BookingId = x.Id,
                    CourtName = x.Court.Name,
                    CourtNumber = x.Court.Number,
                    PlaceName = x.Court.Place.Name,
                    Username = x.User.Username,
                    Date = x.Date,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                }).ToListAsync(cancellationToken);

            return new BookingGetOwnerPageModel
            {
                TotalData = totalDatas,
                TotalPages = (int)Math.Ceiling(totalDatas / (double)request.PageSize),
                CurrentPage = request.PageNumber,
                Datas = ownerBooking
            };
        }

        public async Task<BookingInsertResponseModel> Insert(BookingInsertRequestModel request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Date))
            {
                return new BookingInsertResponseModel
                {
                    Message = "Date must be inserted"
                };
            }

            var date = DateOnly.Parse(request.Date);

            if (string.IsNullOrWhiteSpace(request.StartTime))
            {
                return new BookingInsertResponseModel
                {
                    Message = "StartTime must be inserted"
                };
            }

            if (string.IsNullOrWhiteSpace(request.EndTime))
            {
                return new BookingInsertResponseModel
                {
                    Message = "EndTime must be inserted"
                };
            }

            var startTimeParse = TimeOnly.Parse(request.StartTime);
            var endTimeParse = TimeOnly.Parse(request.EndTime);

            var checkTimeSet = await _db.Bookings
                .AnyAsync(x =>
                    x.StartTime < endTimeParse &&
                    x.EndTime > startTimeParse &&
                    x.Date == date && 
                    x.Court.Id == request.CourtId, cancellationToken);

            if (checkTimeSet)
            {
                return new BookingInsertResponseModel
                {
                    Message = "The Court has been booked at this time / Overlapped with another time"
                };
            }

            var calculateTimeLeft = endTimeParse - startTimeParse;
            var totalTime = calculateTimeLeft.Hours;

            var court = await _db.Courts
                .FirstOrDefaultAsync(x => x.Id == request.CourtId, cancellationToken);

            if (court == null)
            {
                return new BookingInsertResponseModel { Message = "Court not found" };
            }

            var totalPrice = court.PricePerHour * totalTime;

            var newBooking = new Booking
            {
                Id = Guid.NewGuid(),
                CourtId = request.CourtId,
                UserId = request.UserId,
                Date = date,
                StartTime = startTimeParse,
                EndTime = endTimeParse,
                TotalPrice = totalPrice,
                CreatedAt = DateTime.UtcNow,
                Status = Enums.BookingStatus.Pending,
            };

            _db.Bookings.Add(newBooking);
            await _db.SaveChangesAsync(cancellationToken);

            return new BookingInsertResponseModel
            {
                Id = newBooking.Id,
                CourtId = newBooking.CourtId,
                Date = newBooking.Date,
                StartTime = startTimeParse,
                EndTime = endTimeParse,
                Message = "Success"
            };
        }

        public async Task<List<BookingGetAvailableCourtHoursResponse>> GetAvailableCourtHours(Guid courtId, string pickedDate, CancellationToken cancellationToken)
        {
            var date = DateOnly.Parse(pickedDate);
            var availableTime = new List<BookingGetAvailableCourtHoursResponse>();

            var court = await _db.Courts
                .Include(x => x.Place)
                .FirstOrDefaultAsync(x => x.Id == courtId, cancellationToken);

            if (court == null) return null;

            var openHour = court.Place.OpenHour;
            var closeHour = court.Place.CloseHour;

            var bookedCourts = await _db.Bookings.Where(x => x.CourtId == courtId && x.Date == date && x.Status != Enums.BookingStatus.Cancelled)
                .ToListAsync(cancellationToken);

            var currentTime = openHour;

            while(currentTime < closeHour)
            {
                var nextTime = currentTime.AddHours(1);

                bool isBooked = bookedCourts.Any(x => x.StartTime < nextTime && x.EndTime > currentTime);

                availableTime.Add(new BookingGetAvailableCourtHoursResponse
                {
                    Time = currentTime.ToString("HH:mm"),
                    IsBooked = isBooked
                });

                currentTime = nextTime;
            }

            return availableTime;
        }

        public async Task<BookingPatchStatusResponseModel> ToVerifying(BookingPatchStatusRequestModel request, CancellationToken cancellationToken)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == request.BookingId, cancellationToken);

            if(booking == null)
            {
                return new BookingPatchStatusResponseModel
                {
                    Message = "Booking is not exist"
                };
            }

            booking.Status = Enums.BookingStatus.Verifying;

            await _db.SaveChangesAsync(cancellationToken);

            return new BookingPatchStatusResponseModel
            {
                Message = "Success"
            };
        }

        public async Task<BookingPatchStatusResponseModel> ToConfirmed(BookingPatchStatusRequestModel request, CancellationToken cancellationToken)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == request.BookingId, cancellationToken);

            if (booking == null)
            {
                return new BookingPatchStatusResponseModel
                {
                    Message = "Booking is not exist"
                };
            }

            booking.Status = Enums.BookingStatus.Confirmed;

            await _db.SaveChangesAsync(cancellationToken);

            return new BookingPatchStatusResponseModel
            {
                Message = "Success"
            };
        }

        public async Task<BookingPatchStatusResponseModel> ToCompleted(BookingPatchStatusRequestModel request, CancellationToken cancellationToken)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == request.BookingId, cancellationToken);

            if (booking == null)
            {
                return new BookingPatchStatusResponseModel
                {
                    Message = "Booking is not exist"
                };
            }

            booking.Status = Enums.BookingStatus.Completed;

            await _db.SaveChangesAsync(cancellationToken);

            return new BookingPatchStatusResponseModel
            {
                Message = "Success"
            };
        }

        public async Task<BookingPatchStatusResponseModel> ToCancelled(BookingPatchStatusRequestModel request, CancellationToken cancellationToken)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == request.BookingId, cancellationToken);

            if (booking == null)
            {
                return new BookingPatchStatusResponseModel
                {
                    Message = "Booking is not exist"
                };
            }

            booking.Status = Enums.BookingStatus.Cancelled;

            await _db.SaveChangesAsync(cancellationToken);

            return new BookingPatchStatusResponseModel
            {
                Message = "Success"
            };
        }

        public async Task<BookingPatchStatusResponseModel> RequestCancel(BookingPatchStatusRequestModel request, CancellationToken cancellationToken)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == request.BookingId, cancellationToken);

            if (booking == null)
            {
                return new BookingPatchStatusResponseModel
                {
                    Message = "Booking is not exist"
                };
            }

            if(booking.Status == Enums.BookingStatus.Verifying || booking.Status == Enums.BookingStatus.Confirmed)
            {
                return new BookingPatchStatusResponseModel { Message = "Tidak bisa request Cancel setelah pembayaran" };
            }

            if (booking.Status == Enums.BookingStatus.Pending)
            {
                booking.Status = Enums.BookingStatus.RequestCancel;
            }

            await _db.SaveChangesAsync(cancellationToken);

            return new BookingPatchStatusResponseModel
            {
                Message = "Success"
            };
        }
    }
}
