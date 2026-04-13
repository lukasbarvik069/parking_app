namespace parking_app.Models.ValueObjects
{
    public class Date
    {
        public readonly DateTime Start;
        public readonly DateTime End;

        public TimeSpan Duration => End - Start;

        private Date(DateTime start, DateTime end)
        {
            if (start >= end)
            {
                return;
            }

            this.Start = start;
            this.End = end;
        }

        public static Date? Create(DateTime start, DateTime end)
        {
            if (start >= end)
            {
                return null;
            }

            return new Date(start, end);
        }
    }
}
