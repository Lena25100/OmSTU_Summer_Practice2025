using task13;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonSerializerAndDeserializer
{
    public static class JsonSerializerAndDeserializer
    {
        static void Main()
        {
            var Options = new JsonSerializerOptions
            {
                Converters = { new CustomDateConverter() },
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true,
            };

            Student student = new Student
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

            string serialisation_result = JsonSerializer.Serialize(student, Options);
            Console.WriteLine("Сериализация JSON:");
            Console.WriteLine(serialisation_result);

            string path = @"E:\student.json";
            File.WriteAllText(path, serialisation_result);
            Console.WriteLine($"\nJSON сохранён в файл: {path}");

            Student? deserialized_result = JsonSerializer.Deserialize<Student>(serialisation_result, Options);

            if (deserialized_result == null) Console.WriteLine("Ошибка: объект отсутствует!");
            else
            {
                Console.WriteLine("Студент: ");
                if (deserialized_result.FirstName == null) Console.WriteLine("Имя отсутствует!");
                else Console.WriteLine($"Имя: {deserialized_result.FirstName}");

                if (deserialized_result.LastName == null) Console.WriteLine("Фамилия отсутствует!");
                else Console.WriteLine($"Фамилия: {deserialized_result.LastName}");

                if (deserialized_result.BirthDate == null) Console.WriteLine("Дата рождения отсутствует!");
                else Console.WriteLine($"Дата рождения: {deserialized_result.BirthDate:dd.MM.yyyy}");

                if (deserialized_result.Grades == null || deserialized_result.Grades.Count == 0) Console.WriteLine("Оценки отсутсвуют!");
                else
                {
                    Console.WriteLine("Оценки по предметам:");
                    foreach (var subject in deserialized_result.Grades)
                    {
                        Console.WriteLine($"{subject.Name}: {subject.Grade}");
                    }
                }
            }
        }
    }
}
