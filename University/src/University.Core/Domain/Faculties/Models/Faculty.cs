using University.Core.Common;
using University.Core.Domain.Faculties.Common;
using University.Core.Domain.Faculties.Validators;

namespace University.Core.Domain.Faculties.Models
{
    public class Faculty : Entity
    {
        private Faculty()
        {

        }

        public Faculty(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public IReadOnlyCollection<FacultyDepartment> Departments { get; private set; }

        public static async Task<Faculty> CreateAsync(
            string name,
            IFacultyNameMustBeUniqueChecker facultyNameMustBeUniqueChecker,
            CancellationToken cancellationToken = default)
        {
            var faculty = new Faculty(new Guid(), name);
            await ValidateAsync(new CreateFacultyDataValidator(null, facultyNameMustBeUniqueChecker), faculty, cancellationToken);
            return faculty;
        }
    }
}
