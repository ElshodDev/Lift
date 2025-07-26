namespace Lift.API.Models
{
    public class ElevatorStatusDto
    {
        public int CurrentFloor { get; set; }
        public string Direction { get; set; }
        public bool IsBusy { get; set; } = false;
    }
}
