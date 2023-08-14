using ElevatorChallenge.Common.Enums;

namespace ElevatorChallenge.Models
{
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

        public void MoveToFloor(int floor)
        {
            Direction = CurrentFloor < floor ? Direction.Up : Direction.Down;
            while (CurrentFloor != floor)
            {
                Console.WriteLine($"Elevator {ElevatorNumber} is moving to floor {floor}");
                if (Direction == Direction.Up)
                    CurrentFloor++;
                else
                    CurrentFloor--;
            }
            Direction = Direction.Stationary;
            OpenDoors();
        }

        public void OpenDoors()
        {
            Console.WriteLine($"Elevator {ElevatorNumber} doors opened.");
        }

        public void CloseDoors()
        {
            Console.WriteLine($"Elevator {ElevatorNumber} doors closed.");
        }

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

        public void UpdateStatus()
        {
            Console.WriteLine($"Elevator {ElevatorNumber} on floor {CurrentFloor}, {Direction} direction, {CurrentLoad}/{Capacity} people.");
        }
    }
}
