namespace Domain.ValueObjects
{
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public DateRange(DateTime start, DateTime end)
        {
            if (start >= end)
                throw new ArgumentException("Start date must be before end date");

            StartDate = start;
            EndDate = end;
        }

        public bool Overlaps(DateRange other)
        {
            return StartDate < other.EndDate && EndDate > other.StartDate;
        }

        public bool Contains(DateTime date)
        {
            return date >= StartDate && date <= EndDate;
        }
    }
}
