namespace Fanex.Tarus.Admin.Entities.MoveBetSchedule
{
    public class MoveBetDay
    {
        public string Date { get; set; }

        public int WeekDay { get; set; }

        public bool[] Options { get; set; }
    }
}