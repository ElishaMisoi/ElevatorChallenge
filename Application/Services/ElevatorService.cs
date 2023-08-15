using ElevatorChallenge.Common.Enums;
using ElevatorChallenge.Common.Exceptions;
using ElevatorChallenge.Models;

namespace ElevatorChallenge.Application.Services
{
    /// <summary>
    /// This is the Elevator Service.
    /// The service provides a way to interact with the elevators, 
    /// including calling them to a specific floor and setting 
    /// the number of people waiting on each floor.
    /// It supports multiple floors and multiple elevators.
    /// </summary>
    public class ElevatorService : IElevatorService
    {
        private List<Elevator>? _elevators;
        private List<Floor>? _floors;
        private bool _isInitialized;

        public ElevatorService()
        {
            _isInitialized = false;
        }

        /// <summary>
        /// Initializes Elevater Service.
        /// </summary>
        /// <param name="numberOfElevators">The number of elevators to be used</param>
        /// <param name="numberOfFloors">The number of floors to be used</param>
        /// <param name="maxCapacity">The maximum capacity of an elevator</param>
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

        /// <summary>
        /// Requests the nearest elevator to a provided floor
        /// </summary>
        /// <param name="floorNumber">The floor number where the request is coming from</param>
        /// <returns>Returns the nearest Elevator if available</returns>
        public Elevator? RequestNearestElevator(int floorNumber)
        {
            if (!_isInitialized)
                throw new InitializationException("Elevator Service not initialized");

            Floor? existingFloor = _floors!.FirstOrDefault(s => s.FloorNumber == floorNumber);

            if (existingFloor == null)
            {
                Console.WriteLine($"Floor {floorNumber} does not exist");
                return null;
            }

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

        /// <summary>
        /// Moves an Elevator from the floor where the request is coming from
        /// to a destination floor
        /// </summary>
        /// <param name="floorNumber">The floor number where the request is coming from</param>
        /// <param name="destinationFloor">The destination floor where the elavtor is destined</param>
        public void MoveElevatorToFloor(int floorNumber, int destinationFloor)
        {
            Floor? existingFloor = _floors!.FirstOrDefault(s => s.FloorNumber == floorNumber);

            if (existingFloor == null)
            {
                Console.WriteLine($"Floor {floorNumber} does not exist");
                return;
            }

            existingFloor = _floors!.FirstOrDefault(s => s.FloorNumber == destinationFloor);

            if (existingFloor == null)
            {
                Console.WriteLine($"Floor {destinationFloor} does not exist");
                return;
            }

            Elevator? nearestElevator = RequestNearestElevator(floorNumber);

            if (nearestElevator != null)
            {
                nearestElevator.MoveToFloor(destinationFloor);
            }
        }

        /// <summary>
        /// Adds a floor to the list of floors
        /// </summary>
        /// <param name="floor">The floor to be added</param>
        public void AddFloor(Floor floor)
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            _floors!.Add(floor);
        }

        /// <summary>
        /// Removes a floor from the list of floors
        /// </summary>
        /// <param name="floor">The floor to be removed</param>
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

        /// <summary>
        /// Adds people to a given floor
        /// </summary>
        /// <param name="floor">The floor where people should be added</param>
        /// <param name="numberOfPeople">The number of people to be added to the floor</param>
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

        /// <summary>
        /// Removes people from a given floor
        /// </summary>
        /// <param name="floor">The floor where people should be removed</param>
        /// <param name="numberOfPeople">The number of people to be removed from the floor</param>
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

        /// <summary>
        /// Adds an elevator to the list of elevators
        /// </summary>
        /// <param name="elevator">The elevator to be added</param>
        public void AddElevator(Elevator elevator)
        {
            if (!_isInitialized)
                ThrowNotInitialized();

            _elevators!.Add(elevator);
        }

        /// <summary>
        /// Removes an elevator from the list of elevators
        /// </summary>
        /// <param name="elevator">The elevator to be removed</param>
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

        /// <summary>
        /// Loads people into an elevator 
        /// </summary>
        /// <param name="elevator">The elevator to load people</param>
        /// <param name="numberOfPeople">The number of people to be loaded into the elevator</param>
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

        /// <summary>
        /// Unloads people out of an elevator
        /// </summary>
        /// <param name="elevator">The elevator to unload people</param>
        /// <param name="numberOfPeople">The number of people to be unloaded out of the elevator</param>
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

        /// <summary>
        /// Gets the current status and direction of all the elevator
        /// </summary>
        public void GetElevatorStatusesAndDirections()
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
