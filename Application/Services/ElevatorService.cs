using ElevatorChallenge.Common.Enums;
using ElevatorChallenge.Common.Exceptions;
using ElevatorChallenge.Models;

namespace ElevatorChallenge.Application.Services
{
    public class ElevatorService : IElevatorService
    {
        private List<Elevator>? _elevators;
        private List<Floor>? _floors;
        private bool _isInitialized;

        public ElevatorService()
        {
            _isInitialized = false;
        }

        public void Initialize(int numberOfElevators, int numberOfFloors, int maxCapacity = 10)
        {
            if (numberOfElevators < 1)
                throw new Exception("At least one elevator is required");

            if (numberOfFloors < 2)
                throw new Exception("At least two floors are required");

            _elevators = new List<Elevator>();

            for (int i = 0; i < numberOfElevators; i++)
            {
                _elevators.Add(new Elevator(elevatorNumber: (i + 1), capacity: maxCapacity));
            }

            _floors = new List<Floor>();

            for (int i = 0; i < numberOfFloors; i++)
            {
                _floors.Add(new Floor(floorNumber: (i + 1)));
            }

            _isInitialized = true;
        }

        public Elevator? RequestNearestElevator(int floorNumber)
        {
            if (!_isInitialized)
                throw new InitializationException("Elevator Service not initialized");

            // Find the nearest available elevator
            Elevator? nearestElevator = null;
            int minDistance = int.MaxValue;

            if (_elevators != null && _elevators.Any())
            {
                foreach (var elevator in _elevators)
                {
                    if (elevator.Direction == Direction.Stationary)
                    {
                        int distance = Math.Abs(elevator.CurrentFloor - floorNumber);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            nearestElevator = elevator;
                        }
                    }
                }

                if (nearestElevator != null)
                {
                    nearestElevator.MoveToFloor(floorNumber);
                    Console.WriteLine($"Elevator {nearestElevator.ElevatorNumber} has arrived at floor {floorNumber}");
                    return nearestElevator;
                }
                else
                {
                    Console.WriteLine("No available elevators at the moment.");
                    return null;
                }
            }

            return null;
        }

        public void MoveElevatorToFloor(int floorNumber, int destinationFloor)
        {
            Elevator? nearestElevator = RequestNearestElevator(floorNumber);

            if (nearestElevator != null)
            {
                nearestElevator.MoveToFloor(destinationFloor);
                Console.WriteLine($"Elevator {nearestElevator.ElevatorNumber} has arrived at floor {floorNumber}");
            }
        }

        public void AddFloor(Floor floor)
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            _floors!.Add(floor);
        }

        public void RemoveFloor(Floor floor)
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            bool removed = _floors!.Remove(floor);

            if (!removed)
            {
                Console.WriteLine("The floor does not exist");
            }
        }

        public void AddPeopleToFloor(int floor, int numberOfPeople)
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            Floor? existingFloor = _floors!.FirstOrDefault(s => s.FloorNumber == floor);

            if (existingFloor == null)
            {
                Console.WriteLine("The floor does not exist");
                return;
            }

            existingFloor.AddPeople(numberOfPeople);
        }

        public void RemovePeopleFromFloor(int floor, int numberOfPeople)
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            Floor? existingFloor = _floors!.FirstOrDefault(s => s.FloorNumber == floor);

            if (existingFloor == null)
            {
                Console.WriteLine("The floor does not exist");
                return;
            }

            existingFloor.RemovePeople(numberOfPeople);
        }

        public void AddElevator(Elevator elevator)
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            _elevators!.Add(elevator);
        }

        public void RemoveElevator(Elevator elevator)
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            bool removed = _elevators!.Remove(elevator);

            if (!removed)
            {
                Console.WriteLine("The elevator does not exist");
            }
        }

        public void LoadPeopleIntoElevator(int elevator, int numberOfPeople)
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            Elevator? existingElevator = _elevators!.FirstOrDefault(s => s.ElevatorNumber == elevator);

            if (existingElevator == null)
            {
                Console.WriteLine("The elevator does not exist");
                return;
            }

            existingElevator.LoadPeople(numberOfPeople);
        }

        public void UnLoadPeopleOutOfElevator(int elevator, int numberOfPeople)
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            Elevator? existingElevator = _elevators!.FirstOrDefault(s => s.ElevatorNumber == elevator);

            if (existingElevator == null)
            {
                Console.WriteLine("The elevator does not exist");
                return;
            }

            existingElevator.UnloadPeople(numberOfPeople);
        }

        public void GetElevatorStatuses()
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            foreach (var elevator in _elevators!)
            {
                elevator.UpdateStatus();
            }
        }

        private void ThrowNotInitialized()
        {
            throw new InitializationException("Elevator Service not initialized");
        }
    }
}
