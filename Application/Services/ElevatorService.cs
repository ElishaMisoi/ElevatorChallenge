using ElevatorChallenge.Common.Exceptions;
using ElevatorChallenge.Models;

namespace ElevatorChallenge.Application.Services
{
    public class ElevatorService : IElevatorService
    {
        private List<Elevator>? _elevators;
        private bool _isInitialized;
        private int _totalPassengersTransported;

        public ElevatorService()
        {
            _isInitialized = false;
            _totalPassengersTransported = 0;
        }

        public void Initialize(int numberOfElevators, int numberOfFloors, int elevatorMaxCapacity)
        {
            _elevators = new List<Elevator>();

            for (int i = 0; i < numberOfElevators; i++)
            {
                _elevators.Add(new Elevator(i, elevatorMaxCapacity));
            }

            _isInitialized = true;
        }

        public async Task CallElevator(int floor, int totalPassengers)
        {
            if (!_isInitialized)
                throw new InitializationException("Elevator Service not initialized");

            //if (_elevators != null && _elevators.Any())
            //{
            //    List<Task> elevatorTasks = new List<Task>();

            //    foreach (var elevator in _elevators)
            //    {
            //        if (elevator.IsAvailable())
            //        {
            //            Task elevatorTask = Task.Run(() =>
            //            {
            //                elevator.MoveToFloor(floor);
            //                elevator.LoadPassenger(passengers);
            //            });
            //            elevatorTasks.Add(elevatorTask);
            //        }
            //    }

            //    await Task.WhenAll(elevatorTasks);
            //}

            if (_elevators != null && _elevators.Any())
            {
                int passengersPerElevator = totalPassengers / _elevators.Count; // Divide passengers evenly
                int remainingPassengers = totalPassengers % _elevators.Count; // Distribute remaining passengers

                List<Task> elevatorTasks = new List<Task>();
                object lockObject = new object(); // Create a lock object

                foreach (var elevator in _elevators)
                {
                    if (elevator.IsAvailable())
                    {
                        int passengersToLoad = passengersPerElevator;

                        if (remainingPassengers > 0)
                        {
                            passengersToLoad++;
                            remainingPassengers--;
                        }

                        Task elevatorTask = Task.Run(() =>
                        {
                            elevator.MoveToFloor(floor);
                            elevator.LoadPassenger(passengersToLoad);

                            // Lock to update _totalPassengersTransported
                            lock (lockObject)
                            {
                                _totalPassengersTransported += passengersToLoad; // Update total passengers transported
                                Console.WriteLine($"Total passengers transported: {_totalPassengersTransported}");
                            }
                        });

                        elevatorTasks.Add(elevatorTask);
                    }
                }

                await Task.WhenAll(elevatorTasks);
            }
        }

        private Elevator? FindNearestAvailableElevator(int floor)
        {
            if (_elevators != null && _elevators.Any())
            {
                lock (_elevators) // Lock access to the elevators list
                {
                    Elevator? nearestElevator = null;
                    int minDistance = int.MaxValue;

                    foreach (Elevator elevator in _elevators)
                    {
                        if (elevator.IsAvailable())
                        {
                            int distance = Math.Abs(elevator.CurrentFloor - floor);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                nearestElevator = elevator;
                            }
                        }
                    }

                    return nearestElevator;
                }
            }

            return null;
        }

        public void DisplayElevatorStatus()
        {
            Console.WriteLine("Elevator Status:");

            if (_elevators != null && _elevators.Any())
            {
                foreach (var elevator in _elevators)
                {
                    Console.WriteLine($"Elevator - Current Floor: {elevator.CurrentFloor}, Direction: {elevator.CurrentDirection}, Capacity: {elevator.CurrentCapacity}/{elevator.MaxCapacity}");
                }
            }
        }
    }
}
