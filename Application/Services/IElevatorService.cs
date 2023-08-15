using ElevatorChallenge.Models;

namespace ElevatorChallenge.Application.Services
{
    public interface IElevatorService
    {
        /// <summary>
        /// Initializes Elevater Service.
        /// </summary>
        /// <param name="numberOfElevators">The number of elevators to be used</param>
        /// <param name="numberOfFloors">The number of floors to be used</param>
        /// <param name="maxCapacity">The maximum capacity of an elevator</param>
        void Initialize(int numberOfElevators, int numberOfFloors, int maxCapacity = 10);

        /// <summary>
        /// Requests the nearest elevator to a provided floor
        /// </summary>
        /// <param name="floorNumber">The floor number where the request is coming from</param>
        /// <returns>Returns the nearest Elevator if available</returns>
        Elevator? RequestNearestElevator(int floorNumber);

        /// <summary>
        /// Moves an Elevator from the floor where the request is coming from
        /// to a destination floor
        /// </summary>
        /// <param name="floorNumber">The floor number where the request is coming from</param>
        /// <param name="destinationFloor">The destination floor where the elavtor is destined</param>
        void MoveElevatorToFloor(int floorNumber, int destinationFloor);

        /// <summary>
        /// Adds a floor to the list of floors
        /// </summary>
        /// <param name="floor">The floor to be added</param>
        void AddFloor(Floor floor);

        /// <summary>
        /// Removes a floor from the list of floors
        /// </summary>
        /// <param name="floor">The floor to be removed</param>
        void RemoveFloor(Floor floor);

        /// <summary>
        /// Adds people to a given floor
        /// </summary>
        /// <param name="floor">The floor where people should be added</param>
        /// <param name="numberOfPeople">The number of people to be added to the floor</param>
        void AddPeopleToFloor(int floor, int peopleToAdd);

        /// <summary>
        /// Removes people from a given floor
        /// </summary>
        /// <param name="floor">The floor where people should be removed</param>
        /// <param name="numberOfPeople">The number of people to be removed from the floor</param>
        void RemovePeopleFromFloor(int floor, int numberOfPeople);

        /// <summary>
        /// Adds an elevator to the list of elevators
        /// </summary>
        /// <param name="elevator">The elevator to be added</param>
        void AddElevator(Elevator elevator);

        /// <summary>
        /// Removes an elevator from the list of elevators
        /// </summary>
        /// <param name="elevator">The elevator to be removed</param>
        void RemoveElevator(Elevator elevator);

        /// <summary>
        /// Loads people into an elevator 
        /// </summary>
        /// <param name="elevator">The elevator to load people</param>
        /// <param name="numberOfPeople">The number of people to be loaded into the elevator</param>
        void LoadPeopleIntoElevator(int elevator, int numberOfPeople);

        /// <summary>
        /// Unloads people out of an elevator
        /// </summary>
        /// <param name="elevator">The elevator to unload people</param>
        /// <param name="numberOfPeople">The number of people to be unloaded out of the elevator</param>
        void UnLoadPeopleOutOfElevator(int elevator, int numberOfPeople);

        /// <summary>
        /// Gets the current status and direction of all the elevator
        /// </summary>
        void GetElevatorStatusesAndDirections();
    }
}