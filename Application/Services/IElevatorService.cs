namespace ElevatorChallenge.Application.Services
{
    public interface IElevatorService
    {
        void Initialize(int numberOfElevators, int numberOfFloors, int elevatorMaxCapacity);
        Task CallElevator(int floor, int passengers);
        void DisplayElevatorStatus();
    }
}