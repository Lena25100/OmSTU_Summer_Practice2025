
using System.Text.Json;
using System.Text.Json.Serialization;
using task13;

namespace task13tests
{
    public class JsonSerializerAndDeserializerTests
    {
        public JsonSerializerOptions Options { get; }

        public JsonSerializerAndDeserializerTests()
        {
            Options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new CustomDateConverter() },
                WriteIndented = true,
            };
        }

        [Fact]
        public void Serialization_ShouldWorkCorrectly()
        {
            Student test_student = new Student
            {
                FirstName = "Kate",
                LastName = "Perry",
                BirthDate = new DateTime(2004, 9, 14),
                Grades = new List<Subject>
                {
                    new Subject { Name = "English", Grade = 5 },
                    new Subject { Name = "Physics", Grade = 2 },
                    new Subject { Name = "PE", Grade = 4 },
                    new Subject { Name = "Chemistry", Grade = 3 }
                }
            };

            string result = JsonSerializer.Serialize(test_student, Options);

            Assert.Contains("14.09.2004", result);
            Assert.Contains("Kate", result);
            Assert.Contains("Perry", result);
            Assert.Contains("English", result);
            Assert.Contains("5", result);
            Assert.Contains("Physics", result);
            Assert.Contains("2", result);
            Assert.Contains("PE", result);
            Assert.Contains("4", result);
            Assert.Contains("Chemistry", result);
            Assert.Contains("3", result);
        }

        [Fact]
        public void Deserialization_ShouldWorkCorrectly()
        {
            Student test_student = new Student
            {
                FirstName = null,
                LastName = "Patisson",
                BirthDate = new DateTime(2006, 11, 8),
                Grades = null
            };

            string result = JsonSerializer.Serialize(test_student, Options);

            Student? deserialization_result = JsonSerializer.Deserialize<Student>(result, Options);

            Assert.NotNull(deserialization_result);
            Assert.Null(deserialization_result.FirstName);
            Assert.Equal("Patisson", deserialization_result.LastName);
            Assert.Null(deserialization_result.Grades);
            Assert.Equal(new DateTime(2006, 11, 8), deserialization_result.BirthDate);
        }

    }
}
