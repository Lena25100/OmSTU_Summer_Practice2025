using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13
{
    public class Subject
    {
        public string? Name { get; set; }
        public int Grade { get; set; }
    }

    public class Student
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<Subject>? Grades { get; set; }
    }

    public class CustomDateConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateTime.ParseExact(reader.GetString()!, "dd.MM.yyyy", null);

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("dd.MM.yyyy"));

    }
}
