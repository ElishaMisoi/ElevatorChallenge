using ElevatorChallenge.Common.Enums;

namespace ElevatorChallenge.Models
{
    /// <summary>
    /// This is the Elevator Class.
    /// The Class holds properties of an elevator such as
    /// the elevator number, current floor, direction, capacity and load.
    /// It also includes methods relating to an elevator such as 
    /// moving an elevator, loading people into an elevator, unloading people
    /// out of an elevator, opening and closing of elevator doors.
    /// </summary>
    public class Elevator
    {
        public int CurrentFloor { get; private set; }
        public Direction Direction { get; private set; }
        public int Capacity { get; private set; }
        public int CurrentLoad { get; private set; }
        public int ElevatorNumber { get; private set; }

        public Elevator(int elevatorNumber, int capacity)
        {
            ElevatorNumber = elevatorNumber;
            CurrentFloor = 1;
            Direction = Direction.Stationary;
            Capacity = capacity;
            CurrentLoad = 0;
        }

        /// <summary>
        /// Moves an elevator to a floor 
        /// </summary>
        /// <param name="floor">The floor that the elevator is moving to</param>
        public void MoveToFloor(int floor)
        {
            if (CurrentFloor < floor)
            {
                Direction = Direction.Up;
            }
            else
            {
                Direction = Direction.Down;
            }

            Console.WriteLine($"Elevator {ElevatorNumber} is moving to floor {floor}");

            while (CurrentFloor != floor)
            {
                if (Direction == Direction.Up)
                {
                    CurrentFloor++;
                }
                else
                {
                    CurrentFloor--;
                }
            }

            Direction = Direction.Stationary;

            Console.WriteLine($"Elevator {ElevatorNumber} has arrived at floor {floor}");

            OpenDoors();
        }

        /// <summary>
        /// Opens elevator doors
        /// </summary>
        public void OpenDoors()
        {
            Console.WriteLine($"Elevator {ElevatorNumber} doors opened.");
        }

        /// <summary>
        /// Closes elevator doors
        /// </summary>
        public void CloseDoors()
        {
            Console.WriteLine($"Elevator {ElevatorNumber} doors closed.");
        }

        /// <summary>
        /// Loads people into an elevator
        /// </summary>
        /// <param name="numberOfPeople">The number of people to be loaded into the elevator</param>
        public void LoadPeople(int numberOfPeople)
        {
            if (CurrentLoad + numberOfPeople <= Capacity)
            {
                CurrentLoad += numberOfPeople;

                OpenDoors();

                Console.WriteLine($"{numberOfPeople} people loaded into Elevator {ElevatorNumber}.");

                CloseDoors();
            }
            else
            {
                Console.WriteLine($"Elevator {ElevatorNumber} Cannot load people. Exceeds capacity.");
            }
        }

        /// <summary>
        /// Unloads people out of an elevator
        /// </summary>
        /// <param name="numberOfPeople">The number of people to be unloaded from the elevator</param>
        public void UnloadPeople(int numberOfPeople)
        {
            if (CurrentLoad >= numberOfPeople)
            {
                CurrentLoad -= numberOfPeople;

                OpenDoors();

                Console.WriteLine($"{numberOfPeople} people unloaded from Elevator {ElevatorNumber}.");

                CloseDoors();
            }
            else
            {
                Console.WriteLine($"Elevator {ElevatorNumber} Cannot unload more people than currently inside.");
            }
        }

        /// <summary>
        /// Updates the status and direction of an elevator
        /// </summary>
        public void UpdateStatus()
        {
            Console.WriteLine($"Elevator {ElevatorNumber} on floor {CurrentFloor}, {Direction} direction, {CurrentLoad}/{Capacity} people.");
        }
    }
}
