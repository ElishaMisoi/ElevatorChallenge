using ElevatorChallenge.Application.Services;

namespace ElevatorChallenge.Application
{
    public class ElevatorApp
    {
        private readonly IElevatorService _elevatorService;

        public ElevatorApp(IElevatorService elevatorService)
        {
            _elevatorService = elevatorService;
        }

        public async Task Run(int elevatorMaxCapacity = 10)
        {
            while (true)
            {
                Console.WriteLine($"Welcome to the Elevator System. Maximum capacity of passengers per elevator is {elevatorMaxCapacity}\n\n");

                Console.WriteLine("Enter the number of floors between 1 and 170"); // as of today, tallest building has 168 floors

                string? floors = Console.ReadLine();

                bool isFloorsConversionSuccessful = int.TryParse(floors, out int numberOfFloors);
                if (!isFloorsConversionSuccessful || !(numberOfFloors >= 1 && numberOfFloors <= 170))
                {
                    ShowInputError();
                    break;
                }

                Console.WriteLine("Enter the number of elevators between 1 and 57"); // number of elevators of the tallest building

                string? elevators = Console.ReadLine();

                bool isElevatorsConversionSuccessful = int.TryParse(elevators, out int numberOfElevators);
                if (!isElevatorsConversionSuccessful || !(numberOfElevators >= 1 && numberOfElevators <= 57))
                {
                    ShowInputError();
                    break;
                }

                Console.WriteLine("Enter number of passengers between 1 and 40,000"); // approximate capacity of the tallest building

                string? passengers = Console.ReadLine();

                bool isPassengersConversionSuccessful = int.TryParse(passengers, out int numberOfPassengers);
                if (!isPassengersConversionSuccessful || !(numberOfPassengers >= 1 && numberOfPassengers <= 40000))
                {
                    ShowInputError();
                    break;
                }

                _elevatorService.Initialize(numberOfElevators, numberOfFloors, elevatorMaxCapacity);
                await _elevatorService.CallElevator(numberOfFloors, numberOfPassengers);
                _elevatorService.DisplayElevatorStatus();
            }
        }

        private void ShowInputError()
        {
            Console.WriteLine("Invalid input. Please try again.\n\n");
            Run();
        }
    }
}
