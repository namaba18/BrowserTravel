using Domain.ValueObjects;

namespace UnitTests
{
    public class DateRangeTests
    {
        [Fact]
        public void Test1()
        {
            var r1 = new DateRange(
                new DateTime(2026, 1, 10),
                new DateTime(2026, 1, 15));

            var r2 = new DateRange(
                new DateTime(2026, 1, 14),
                new DateTime(2026, 1, 20));

            Assert.True(r1.Overlaps(r2));
        }
    }
}
