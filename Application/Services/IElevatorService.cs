using ElevatorChallenge.Models;

namespace ElevatorChallenge.Application.Services
{
    public interface IElevatorService
    {
        void Initialize(int numberOfElevators, int numberOfFloors, int maxCapacity = 10);
        Elevator? RequestNearestElevator(int floorNumber);
        void MoveElevatorToFloor(int floorNumber, int destinationFloor);
        void AddFloor(Floor floor);
        void RemoveFloor(Floor floor);
        void AddPeopleToFloor(int floor, int peopleToAdd);
        void RemovePeopleFromFloor(int floor, int numberOfPeople);
        void AddElevator(Elevator elevator);
        void RemoveElevator(Elevator elevator);
        void LoadPeopleIntoElevator(int elevator, int numberOfPeople);
        void UnLoadPeopleOutOfElevator(int elevator, int numberOfPeople);
        void GetElevatorStatuses();
    }
}