namespace Fanex.Tarus.Admin.Entities.MoveBetSchedule
{
    using System;

    public class MoveBetDay
    {
        public MoveBetDay(
            DateTime date,
            int weekDay,
            bool isBackDay1,
            bool isBackDay2,
            bool isBackDay3,
            bool isBackDay4,
            bool isBackDay5,
            bool isBackDay6,
            bool isBackDay7)
        {
            Date = date;
            WeekDay = weekDay;
            IsBackDay1 = isBackDay1;
            IsBackDay2 = isBackDay2;
            IsBackDay3 = isBackDay3;
            IsBackDay4 = isBackDay4;
            IsBackDay5 = isBackDay5;
            IsBackDay6 = isBackDay6;
            IsBackDay7 = isBackDay7;
        }

        public DateTime Date { get; }

        public int WeekDay { get; }

        public bool IsBackDay1 { get; }

        public bool IsBackDay2 { get; }

        public bool IsBackDay3 { get; }

        public bool IsBackDay4 { get; }

        public bool IsBackDay5 { get; }

        public bool IsBackDay6 { get; }

        public bool IsBackDay7 { get; }
    }

    public class MoveBetDayViewModel
    {
        public MoveBetDayViewModel(MoveBetDay moveBetDay)
        {
            Date = moveBetDay.Date.ToShortDateString();
            WeekDay = moveBetDay.WeekDay - 1;
            Options = new bool[]
            {
                moveBetDay.IsBackDay1,
                moveBetDay.IsBackDay2,
                moveBetDay.IsBackDay3,
                moveBetDay.IsBackDay4,
                moveBetDay.IsBackDay5,
                moveBetDay.IsBackDay6,
                moveBetDay.IsBackDay7
            };
        }

        public string Date { get; set; }

        public int WeekDay { get; set; }

        public bool[] Options { get; set; }
    }
}