namespace Lift.API.Models
{
    public class ElevatorStatus
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; } = 1;
        public ElevatorDirection Direction { get; set; } = ElevatorDirection.Idle;
        public bool IsBusy { get; set; } = false;
    }
}
