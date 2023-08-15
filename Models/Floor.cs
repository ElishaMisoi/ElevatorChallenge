namespace ElevatorChallenge.Models
{
    /// <summary>
    /// This is the Floor Class.
    /// The Class holds properties of a floor including
    /// the floor number and the number of people on that floor.
    /// It also included methods to add people to a floor 
    /// and remove people from a floor
    /// </summary>
    public class Floor
    {
        public int FloorNumber { get; private set; }
        public int PeopleWaiting { get; private set; }

        public Floor(int floorNumber)
        {
            FloorNumber = floorNumber;
            PeopleWaiting = 0;
        }

        /// <summary>
        /// Adds people to a floor
        /// </summary>
        /// <param name="numberOfPeople">The number of people to be added to the floor</param>
        public void AddPeople(int numberOfPeople)
        {
            PeopleWaiting += numberOfPeople;
            Console.WriteLine($"{numberOfPeople} people added to Floor {FloorNumber}.");
        }

        /// <summary>
        /// Removes people from a floor
        /// </summary>
        /// <param name="numberOfPeople">The number of people to be removed from the floor</param>
        public void RemovePeople(int numberOfPeople)
        {
            if (PeopleWaiting >= numberOfPeople)
            {
                PeopleWaiting -= numberOfPeople;
                Console.WriteLine($"{numberOfPeople} people removed from Floor {FloorNumber}.");
            }
            else
            {
                Console.WriteLine("Cannot remove more people than currently waiting.");
            }
        }
    }
}
