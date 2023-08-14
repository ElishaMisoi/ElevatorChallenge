namespace ElevatorChallenge.Models
{
    public class Floor
    {
        public int FloorNumber { get; private set; }
        public int PeopleWaiting { get; private set; }

        public Floor(int floorNumber)
        {
            FloorNumber = floorNumber;
            PeopleWaiting = 0;
        }

        public void AddPeople(int numberOfPeople)
        {
            PeopleWaiting += numberOfPeople;
            Console.WriteLine($"{numberOfPeople} people added to Floor {FloorNumber}.");
        }

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
