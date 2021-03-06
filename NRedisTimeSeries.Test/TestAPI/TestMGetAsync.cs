﻿using NRedisTimeSeries.DataTypes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NRedisTimeSeries.Test.TestAPI
{
    public class TestMGetAsync : AbstractTimeSeriesTest
    {
        public TestMGetAsync(RedisFixture redisFixture) : base(redisFixture) { }

        [Fact]
        public async Task TestMGetQuery()
        {
            var keys = CreateKeyNames(2);
            var db = redisFixture.Redis.GetDatabase();

            var label1 = new TimeSeriesLabel(keys[0], "value");
            var label2 = new TimeSeriesLabel(keys[1], "value2");
            var labels1 = new List<TimeSeriesLabel> { label1, label2 };
            var labels2 = new List<TimeSeriesLabel> { label1 };

            var ts1 = await db.TimeSeriesAddAsync(keys[0], "*", 1.1, labels: labels1);
            var tuple1 = new TimeSeriesTuple(ts1, 1.1);
            var ts2 = await db.TimeSeriesAddAsync(keys[1], "*", 2.2, labels: labels2);
            var tuple2 = new TimeSeriesTuple(ts2, 2.2);

            var results = await db.TimeSeriesMGetAsync(new List<string> { $"{keys[0]}=value" });
            Assert.Equal(2, results.Count);
            Assert.Equal(keys[0], results[0].key);
            Assert.Equal(tuple1, results[0].value);
            Assert.Equal(new List<TimeSeriesLabel>(), results[0].labels);
            Assert.Equal(keys[1], results[1].key);
            Assert.Equal(tuple2, results[1].value);
            Assert.Equal(new List<TimeSeriesLabel>(), results[1].labels);
        }

        [Fact]
        public async Task TestMGetQueryWithLabels()
        {
            var keys = CreateKeyNames(2);
            var db = redisFixture.Redis.GetDatabase();

            var label1 = new TimeSeriesLabel(keys[0], "value");
            var label2 = new TimeSeriesLabel(keys[1], "value2");
            var labels1 = new List<TimeSeriesLabel> { label1, label2 };
            var labels2 = new List<TimeSeriesLabel> { label1 };

            var ts1 = await db.TimeSeriesAddAsync(keys[0], "*", 1.1, labels: labels1);
            var tuple1 = new TimeSeriesTuple(ts1, 1.1);
            var ts2 = await db.TimeSeriesAddAsync(keys[1], "*", 2.2, labels: labels2);
            var tuple2 = new TimeSeriesTuple(ts2, 2.2);

            var results = await db.TimeSeriesMGetAsync(new List<string> { $"{keys[0]}=value" }, withLabels: true);
            Assert.Equal(2, results.Count);
            Assert.Equal(keys[0], results[0].key);
            Assert.Equal(tuple1, results[0].value);
            Assert.Equal(labels1, results[0].labels);
            Assert.Equal(keys[1], results[1].key);
            Assert.Equal(tuple2, results[1].value);
            Assert.Equal(labels2, results[1].labels);
        }
    }
}
