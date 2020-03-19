using System;
using Xunit;

namespace NRedisTimeSeries.Test
{
    public class TestTimeSeriesTuple
    {
        [Fact]
        public void TestTimeSeriesTupleConstructor()
        {
            TimeSeriesTuple tuple = new TimeSeriesTuple(1, 1.1);
            Assert.Equal(1, tuple.Time);
            Assert.Equal(1.1, tuple.Val);
        }

        [Fact]
        public void TestTimeSeriesTupleEqual()
        {
            TimeSeriesTuple tuple1 = new TimeSeriesTuple(1, 1.1);
            TimeSeriesTuple tuple1_1 = new TimeSeriesTuple(1, 1.1);
            TimeSeriesTuple tuple1_2 = new TimeSeriesTuple(2, 2.2);
            Assert.Equal(tuple1, tuple1_1);
            Assert.NotEqual(tuple1, tuple1_2);
        }

        [Fact]
        public void TestTimeSeriesTupleHashCode()
        {
            TimeSeriesTuple tuple1 = new TimeSeriesTuple(1, 1.1);
            TimeSeriesTuple tuple1_1 = new TimeSeriesTuple(1, 1.1);
            TimeSeriesTuple tuple1_2 = new TimeSeriesTuple(2, 2.2);
            Assert.Equal(tuple1.GetHashCode(), tuple1_1.GetHashCode());
            Assert.NotEqual(tuple1.GetHashCode(), tuple1_2.GetHashCode());
        }
    }
}
