using Domain.ValueObjects;

namespace UnitTests.Domain.ValueObjects
{
    public class DateRangeTests
    {
        [Fact]
        public void Overlaps_WhenRangesOverlap_ReturnsTrue()
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
