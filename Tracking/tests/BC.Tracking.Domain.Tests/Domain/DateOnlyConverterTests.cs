using System;
using Xunit;
using System.Text.Json;

namespace Connexity.BC.Tracking.Domain.Tests.Domain
{
    public class DateOnlyConverterTests
    {

        internal class DateOnlyHolder
        {
            public DateOnly MyDateOnly { get; set; }
        }

        internal DateOnlyHolder expectedDateonly = new() { MyDateOnly = new DateOnly(2022, 02, 22) };

        [Fact]
        public void DateOnlyHolder_ShouldThrowErrorValidFrFrSlash()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"22/02/2022\"}";
           
            Assert.Throws<FormatException>(() => GetDeserializedDateOnly(jsonDateonly));
        }

        [Fact]
        public void DateOnlyHolder_ShouldThrowErrorValidFrFrDash()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"22-02-2022\"}";

            Assert.Throws<FormatException>(() => GetDeserializedDateOnly(jsonDateonly));
        }

        [Fact]
        public void DateOnlyHolder_ShouldDeserializeValidEnGbSlash()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"02/22/2022\"}";

            var deserializedDateonly = GetDeserializedDateOnly(jsonDateonly);
            Assert.True(expectedDateonly.MyDateOnly.Equals(deserializedDateonly.MyDateOnly), $"expected {expectedDateonly.MyDateOnly} != deserialized {deserializedDateonly.MyDateOnly}");
        }

        [Fact]
        public void DateOnlyHolder_ShouldDeserializeValidEnGbDash()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"02-22-2022\"}";

            var deserializedDateonly = GetDeserializedDateOnly(jsonDateonly);
            Assert.True(expectedDateonly.MyDateOnly.Equals(deserializedDateonly.MyDateOnly), $"expected {expectedDateonly.MyDateOnly} != deserialized {deserializedDateonly.MyDateOnly}");
        }

        [Fact]
        public void DateOnlyHolder_ShouldDeserializeValidEnUsSlash()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"2022/02/22\"}";

            var deserializedDateonly = GetDeserializedDateOnly(jsonDateonly);
            Assert.True(expectedDateonly.MyDateOnly.Equals(deserializedDateonly.MyDateOnly), $"expected {expectedDateonly.MyDateOnly} != deserialized {deserializedDateonly.MyDateOnly}");
        }

        [Fact]
        public void DateOnlyHolder_ShouldDeserializeValidEnUsDash()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"2022-02-22\"}";

            var deserializedDateonly = GetDeserializedDateOnly(jsonDateonly);
            Assert.True(expectedDateonly.MyDateOnly.Equals(deserializedDateonly.MyDateOnly), $"expected {expectedDateonly.MyDateOnly} != deserialized {deserializedDateonly.MyDateOnly}");
        }

        [Fact]
        public void DateOnlyHolder_ShouldThrowErrorForInvalidEnGbSlash()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"2022/22/02\"}";

            Assert.Throws<FormatException>(() => GetDeserializedDateOnly(jsonDateonly));
        }

        [Fact]
        public void DateOnlyHolder_ShouldThrowErrorForInvalidEnGbDash()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"2022-22-02\"}";

            Assert.Throws<FormatException>(() => GetDeserializedDateOnly(jsonDateonly));
        }

        [Fact]
        public void DateOnlyHolder_ShouldThrowErrorValidFrFrSlashTruncatedYear()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"22/02/22\"}";
            
            Assert.Throws<FormatException>(() => GetDeserializedDateOnly(jsonDateonly));
        }

        [Fact]
        public void DateOnlyHolder_ShouldThrowErrorValidFrFrDashTruncatedYear()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"22-02-22\"}";
            
            Assert.Throws<FormatException>(() => GetDeserializedDateOnly(jsonDateonly));
        }

        [Fact]
        public void DateOnlyHolder_ShouldDeserializeValidEnGbSlashTruncatedYear()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"02/22/22\"}";

            var deserializedDateonly = GetDeserializedDateOnly(jsonDateonly);
            Assert.True(expectedDateonly.MyDateOnly.Equals(deserializedDateonly.MyDateOnly), $"expected {expectedDateonly.MyDateOnly} != deserialized {deserializedDateonly.MyDateOnly}");
        }

        [Fact]
        public void DateOnlyHolder_ShouldDeserializeValidEnGbDashTruncatedYear()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"02-22-22\"}";

            var deserializedDateonly = GetDeserializedDateOnly(jsonDateonly);
            Assert.True(expectedDateonly.MyDateOnly.Equals(deserializedDateonly.MyDateOnly), $"expected {expectedDateonly.MyDateOnly} != deserialized {deserializedDateonly.MyDateOnly}");
        }

        [Fact]
        public void DateOnlyHolder_ShouldThrowErrorForInvalidValueNumber()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"0\"}";

            Assert.Throws<FormatException>(() => GetDeserializedDateOnly(jsonDateonly));
        }

        [Fact]
        public void DateOnlyHolder_ShouldThrowErrorForInvalidValueText()
        {
            var jsonDateonly = "{\"MyDateOnly\":\"Test\"}";

            Assert.Throws<FormatException>(() => GetDeserializedDateOnly(jsonDateonly));
        }

        internal static DateOnlyHolder GetDeserializedDateOnly(string jsonDateOnly)
        {
            return JsonSerializer.Deserialize<DateOnlyHolder>(jsonDateOnly, new JsonSerializerOptions());
        }
    }
}
