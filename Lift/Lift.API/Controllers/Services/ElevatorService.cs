using Lift.API.Data;
using Lift.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Lift.API.Controllers.Services
{
    public class ElevatorService
    {
        private readonly ElevatorDbContext _context;

        public ElevatorService(ElevatorDbContext context)
        {
            _context = context;
        }

        public async Task<ElevatorStatusDto> GetCurrentStatusAsync()
        {
            var status = await _context.ElevatorStatuses.FirstOrDefaultAsync();

            if (status == null)
            {
                status = new ElevatorStatus
                {
                   CurrentFloor = 1,
                   Direction = ElevatorDirection.Idle,
                   IsBusy = false
                };
                _context.ElevatorStatuses.Add(status);
                await _context.SaveChangesAsync();
            }

            return new ElevatorStatusDto
            {
                CurrentFloor = status.CurrentFloor,
                Direction = status.Direction.ToString(),
                IsBusy = status.IsBusy
            };
        }

        public async Task<string> AddRequestAsync(int requestFloor)
        {
            if (requestFloor < 1 || requestFloor > 10)
                return "Xato: 1-qavatdan 10-qavatgacha bo'lishi kerak.";

            var request = new ElevatorRequest
            {
                RequestedFloor = requestFloor,
                IsCompleted = false,
                RequestTime = DateTime.UtcNow
            };

            _context.ElevatorRequests.Add(request);
            await _context.SaveChangesAsync();

            return "So‘rov muvaffaqiyatli qo‘shildi.";
        }

        public async Task ProcessNextRequestAsync()
        {
            var status = await _context.ElevatorStatuses.FirstOrDefaultAsync();
            if (status == null) return;
            if (status.IsBusy) return;

            var pendingRequests = await _context.ElevatorRequests
                .Where(r => !r.IsCompleted)
                .OrderBy(r => r.RequestTime) //FIFO 
               // .OrderBy(r => Math.Abs(r.RequestedFloor - status.CurrentFloor))  // PRIORITET: yaqin qavat
                //.ThenBy(r => r.RequestTime)
                .ToListAsync();

            if (!pendingRequests.Any()) return;

            status.IsBusy = true;
            await _context.SaveChangesAsync();

            foreach (var nextRequest in pendingRequests)
            {
                int targetFloor = nextRequest.RequestedFloor;

                while (status.CurrentFloor != targetFloor)
                {
                    if (status.CurrentFloor < targetFloor)
                    {
                        status.CurrentFloor++;
                        status.Direction = ElevatorDirection.Up;
                    }
                    else
                    {
                        status.CurrentFloor--;
                        status.Direction = ElevatorDirection.Down;
                    }

                    _context.ElevatorStatuses.Update(status);
                    await _context.SaveChangesAsync();

                    await Task.Delay(3000);
                }

                nextRequest.IsCompleted = true;
                status.Direction = ElevatorDirection.Idle;

                _context.ElevatorRequests.Update(nextRequest);
                await _context.SaveChangesAsync();
            }

            status.IsBusy = false;
            await _context.SaveChangesAsync();
        }

        public async Task<List<ElevatorRequest>> GetAllRequestsAsync() =>
            await _context.ElevatorRequests.OrderBy(r => r.RequestTime).ToListAsync();
    }
}
