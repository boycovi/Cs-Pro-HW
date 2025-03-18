using University.Core.Domain.Departments.Models;

namespace University.Core.Domain.Faculties.Models;

public class FacultyDepartment
{
    private FacultyDepartment()
    {

    }

    private FacultyDepartment(Guid facultyId, Guid departmentId)
    {
        FacultyId = facultyId;
        DepartmenttId = departmentId;
    }

    public Guid FacultyId { get; set; }

    public Guid DepartmenttId { get; set; }

    public Department Department { get; set; }

    public static FacultyDepartment Create(Guid facultyId, Guid departmentId)
    {
        return new FacultyDepartment(facultyId, departmentId);
    }
}