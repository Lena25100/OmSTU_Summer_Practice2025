using task02;

namespace task02tests
{
    public class StudentServiceTests
    {
        private List<Student> _testStudents;
        private StudentService _service;

        public StudentServiceTests()
        {
            _testStudents = new List<Student>
        {
            new() { Name = "Данил", Faculty = "ПИ", Grades = new List<int> { 3, 4, 3 } },
            new() { Name = "Алёна", Faculty = "ФИТ", Grades = new List<int> { 5, 5, 4 } },
            new() { Name = "Саша", Faculty = "ПСД", Grades = new List<int> { 3, 2, 3 } },
            new() { Name = "Сабина", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
            new() { Name = "Ярослав", Faculty = "ФИТ", Grades = new List<int> { 5, 5, 5 } }
        };
            _service = new StudentService(_testStudents);
        }

        [Fact]
        public void GetStudentsByFaculty_ReturnsCorrectStudents()
        {
            var result = _service.GetStudentsByFaculty("ФИТ").ToList();
            Assert.Equal(3, result.Count);
            Assert.True(result.All(s => s.Faculty == "ФИТ"));
        }

        [Fact]
        public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
        {
            var result = _service.GetFacultyWithHighestAverageGrade();
            Assert.Equal("ФИТ", result);
        }

        [Fact]
        public void GetStudentsWithMinAverageGrade_ReturnsCorrectStudents()
        {
            var result = _service.GetStudentsWithMinAverageGrade(4.4).ToList();
            Assert.Equal(3, result.Count);
            Assert.All(result, student => Assert.True(student.Grades.Average() > 4.4));
        }


        [Fact]
        public void GetStudentsOrderedByName_ReturnsCorrectStudents()
        {
            var result = _service.GetStudentsOrderedByName().Select(s => s.Name).ToList();
            Assert.Equal(5, result.Count);
            Assert.Equal(new[] { "Алёна", "Данил", "Сабина", "Саша", "Ярослав"}, result);
        }

        [Fact]
        public void GroupStudentsByFaculty_ReturnsCorrectFaculty()
        {
            var result = _service.GroupStudentsByFaculty();
            Assert.Equal(3, result.Count);
            Assert.Equal(3, result["ФИТ"].Count());
            Assert.Equal(1, result["ПСД"].Count());
            Assert.Equal(1, result["ПИ"].Count());
        }
    }
}