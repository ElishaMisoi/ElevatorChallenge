﻿using ElevatorChallenge.Application.Services;

namespace ElevatorChallenge.Application
{
    public class ElevatorApp
    {
        private readonly IElevatorService _elevatorService;

        public ElevatorApp(IElevatorService elevatorService)
        {
            _elevatorService = elevatorService;
        }

        public void Run(int elevatorMaxCapacity = 10)
        {
            bool initialized = InitializeBaseInputs(elevatorMaxCapacity);

            if (initialized)
            {
                DisplayChoices();
            }
            else
            {
                Run(elevatorMaxCapacity);
            }
        }

        private bool InitializeBaseInputs(int elevatorMaxCapacity = 10)
        {
            while (true)
            {
                Console.WriteLine($"Welcome to the Elevator System. Maximum capacity of passengers per elevator is {elevatorMaxCapacity}\n\n");

                Console.WriteLine("Enter the number of floors (At least 2 floors)");

                string? floors = Console.ReadLine();

                bool isFloorsConversionSuccessful = int.TryParse(floors, out int numberOfFloors);
                if (!isFloorsConversionSuccessful || numberOfFloors < 2)
                {
                    ShowInitializationInputError();
                    break;
                }

                Console.WriteLine("Enter the number of elevators (At least 1)");

                string? elevators = Console.ReadLine();

                bool isElevatorsConversionSuccessful = int.TryParse(elevators, out int numberOfElevators);
                if (!isElevatorsConversionSuccessful || numberOfElevators < 1)
                {
                    ShowInitializationInputError();
                    break;
                }

                _elevatorService.Initialize(numberOfElevators, numberOfFloors, elevatorMaxCapacity);
                break;
            }

            return true;
        }

        private void DisplayChoices()
        {
            while (true)
            {
                Console.WriteLine("Options:");
                Console.WriteLine("1. Request Nearest Elevator");
                Console.WriteLine("2. Add People to Floor");
                Console.WriteLine("3. Remove People from Floor");
                Console.WriteLine("4. Get Elevators Statuses");
                Console.WriteLine("5. Load People into Elevator");
                Console.WriteLine("6. Unload People from Elevator");
                Console.WriteLine("7. Move Elevator to Floor");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");

                string? input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    bool isParseSuccessful = int.TryParse(input, out int choice);

                    if (!isParseSuccessful || choice < 1)
                    {
                        ShowInputError();
                        break;
                    }

                    switch (choice)
                    {
                        case 1:
                            RequestNearestElevator();
                            break;
                        case 2:
                            AddPeopleToFloor();
                            break;
                        case 3:
                            RemovePeopleFromFloor();
                            break;
                        case 4:
                            GetElevatorStatuses();
                            break;

                        case 5:
                            LoadPeopleIntoElevator();
                            break;
                        case 6:
                            UnLoadPeopleOutOfElevator();
                            break;
                        case 7:
                            MoveElevatorToFloor();
                            break;
                        case 8:
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            ShowInputError();
                            break;
                    }

                    Console.WriteLine();
                }
            }
        }

        private void ShowInitializationInputError()
        {
            Console.WriteLine("Invalid input. Please try again.\n\n");
            InitializeBaseInputs();
        }

        private void ShowInputError()
        {
            Console.WriteLine("Invalid input. Please try again.\n\n");
            DisplayChoices();
        }

        private void RequestNearestElevator()
        {
            Console.Write("Enter your current floor number: ");

            string? input = Console.ReadLine();

            bool isParseSuccessful = int.TryParse(input, out int floorNumber);

            if (!isParseSuccessful || floorNumber < 1)
            {
                ShowInputError();
                return;
            }

            _elevatorService.RequestNearestElevator(floorNumber);
        }

        private void AddPeopleToFloor()
        {
            Console.Write("Enter floor number: ");

            string? input = Console.ReadLine();

            bool isParseSuccessful = int.TryParse(input, out int floorNumber);

            if (!isParseSuccessful || floorNumber < 1)
            {
                ShowInputError();
                return;
            }

            Console.Write("Enter number of people to add: ");

            input = Console.ReadLine();

            isParseSuccessful = int.TryParse(input, out int numberOfPeople);

            if (!isParseSuccessful || floorNumber < 1)
            {
                ShowInputError();
                return;
            }

            _elevatorService.AddPeopleToFloor(floorNumber, numberOfPeople);
        }

        private void RemovePeopleFromFloor()
        {
            Console.Write("Enter floor number: ");

            string? input = Console.ReadLine();

            bool isParseSuccessful = int.TryParse(input, out int floorNumber);

            if (!isParseSuccessful || floorNumber < 1)
            {
                ShowInputError();
                return;
            }

            Console.Write("Enter number of people to remove: ");

            input = Console.ReadLine();

            isParseSuccessful = int.TryParse(input, out int numberOfPeople);

            if (!isParseSuccessful || floorNumber < 1)
            {
                ShowInputError();
                return;
            }

            _elevatorService.RemovePeopleFromFloor(floorNumber, numberOfPeople);
        }

        private void GetElevatorStatuses()
        {
            _elevatorService.GetElevatorStatuses();
        }

        private void LoadPeopleIntoElevator()
        {
            Console.Write("Enter elevator number: ");

            string? input = Console.ReadLine();

            bool isParseSuccessful = int.TryParse(input, out int elevatorNumber);

            if (!isParseSuccessful || elevatorNumber < 1)
            {
                ShowInputError();
                return;
            }

            Console.Write("Enter number of people to load: ");

            input = Console.ReadLine();

            isParseSuccessful = int.TryParse(input, out int numberOfPeople);

            if (!isParseSuccessful || elevatorNumber < 1)
            {
                ShowInputError();
                return;
            }

            _elevatorService.LoadPeopleIntoElevator(elevatorNumber, numberOfPeople);
        }

        private void UnLoadPeopleOutOfElevator()
        {
            Console.Write("Enter elevator number: ");

            string? input = Console.ReadLine();

            bool isParseSuccessful = int.TryParse(input, out int elevatorNumber);

            if (!isParseSuccessful || elevatorNumber < 1)
            {
                ShowInputError();
                return;
            }

            Console.Write("Enter number of people to unload: ");

            input = Console.ReadLine();

            isParseSuccessful = int.TryParse(input, out int numberOfPeople);

            if (!isParseSuccessful || elevatorNumber < 1)
            {
                ShowInputError();
                return;
            }

            _elevatorService.UnLoadPeopleOutOfElevator(elevatorNumber, numberOfPeople);
        }

        private void MoveElevatorToFloor()
        {
            Console.Write("Enter your current floor number: ");

            string? input = Console.ReadLine();

            bool isParseSuccessful = int.TryParse(input, out int floorNumber);

            if (!isParseSuccessful || floorNumber < 1)
            {
                ShowInputError();
                return;
            }

            Console.Write("Enter your destination floor number: ");

            input = Console.ReadLine();

            isParseSuccessful = int.TryParse(input, out int destinationFloor);

            if (!isParseSuccessful || floorNumber < 1)
            {
                ShowInputError();
                return;
            }

            _elevatorService.MoveElevatorToFloor(floorNumber, destinationFloor);
        }
    }
}
