using ElevatorChallenge.Common.Enums;

namespace ElevatorChallenge.Models
{
    public class Elevator
    {
        public int CurrentFloor { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public int MaxCapacity { get; private set; }
        public int CurrentCapacity { get; private set; }
        public int ElevatorId { get; private set; }

        public Elevator(int elevatorId, int maxCapacity)
        {
            ElevatorId = elevatorId;
            CurrentFloor = 1;
            CurrentDirection = Direction.None;
            MaxCapacity = maxCapacity;
            CurrentCapacity = 0;
        }

        public void MoveToFloor(int floor)
        {
            if (floor > CurrentFloor)
                CurrentDirection = Direction.Up;
            else if (floor < CurrentFloor)
                CurrentDirection = Direction.Down;

            Console.WriteLine($"Elevator {ElevatorId} is moving {CurrentDirection} to floor {floor}");

            //int durationInSeconds = 10;
            //Timer timer = new Timer(PrintArrow, null, 0, 1000);

            //// Wait for the specified duration
            //Thread.Sleep(durationInSeconds * 1000);

            //// Dispose the timer when done
            //timer.Dispose();

            //Console.WriteLine("Timer stopped.");

            while (CurrentFloor != floor)
            {
                if (CurrentDirection == Direction.Up)
                    CurrentFloor++;
                else
                    CurrentFloor--;

                Console.WriteLine($"Elevator {ElevatorId} is now at floor {CurrentFloor}");
            }

            CurrentDirection = Direction.None;
            Console.WriteLine($"Elevator {ElevatorId} has arrived at floor {CurrentFloor}");
        }

        static void PrintArrow(object state)
        {
            Console.Write(">>");
        }

        public bool IsAvailable()
        {
            return CurrentDirection == Direction.None && CurrentCapacity < MaxCapacity;
        }

        public void LoadPassenger(int count)
        {
            if (CurrentCapacity + count <= MaxCapacity)
            {
                CurrentCapacity += count;
                Console.WriteLine($"{count} passengers loaded. Current capacity: {CurrentCapacity}");
            }
            else
            {
                Console.WriteLine($"Elevator {ElevatorId} is at full capacity. Cannot load more passengers.");
            }
        }

        public void UnloadPassengers(int passengersToUnload = 0)
        {
            if (passengersToUnload == 0)
                return;

            if (passengersToUnload > CurrentCapacity)
                passengersToUnload = CurrentCapacity;

            if (CurrentCapacity > 0 && passengersToUnload > 0 && passengersToUnload <= CurrentCapacity)
            {
                Console.WriteLine($"Elevator {ElevatorId} has unloaded {passengersToUnload}.");
                CurrentCapacity = CurrentCapacity - passengersToUnload;
            }
        }
    }
}
