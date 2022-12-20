namespace HouseExpenses.Data.Models
{
    public class HouseDao : RootDao
    {
        public List <RoomDao> Rooms { get; set; }
    }
}
