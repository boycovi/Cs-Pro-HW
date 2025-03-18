using MediatR;
using Microsoft.Extensions.DependencyInjection;
using University.Core.Common;
using University.Core.Domain.Departments.Common;
using University.Core.Domain.Faculties.Common;
using University.Core.Domain.Groups.Common;
using University.Core.Domain.RecordBooks.Common;
using University.Core.Domain.Students.Common;
using University.Core.Domain.Subjects.Common;
using University.Core.Domain.Teachers.Common;
using University.Infrastructure.Core.Common;
using University.Infrastructure.Core.Domain.Departments.Common;
using University.Infrastructure.Core.Domain.Faculties.Common;
using University.Infrastructure.Core.Domain.Groups.Common;
using University.Infrastructure.Core.Domain.RecordBooks.Common;
using University.Infrastructure.Core.Domain.Students.Common;
using University.Infrastructure.Core.Domain.Subjects.Common;
using University.Infrastructure.Core.Domain.Teachers.Common;

namespace University.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureRegistration(this IServiceCollection service)
    {
        service.AddMediatR(typeof(InfrastructureRegistration));

        //UnitOfWork
        service.AddScoped<IUnitOfWork, UnitOfWork>();

        //Repository
        service.AddScoped<ISubjectsRepository, SubjectsRepository>();
        service.AddScoped<IStudentsRepository, StudentsRepository>();
        service.AddScoped<IGroupRepository, GroupRepository>();
        service.AddScoped<ITeachersRepository, TeachersRepository>();
        service.AddScoped<ITeacherSubjectsRepository, TeacherSubjectsRepository>();
        service.AddScoped<IRecordBookRepository, RecordBookRepository>();
        service.AddScoped<IMarkRepository, MarkRepository>();
        service.AddScoped<IDepartmentRepository, DepartmentRepository>();
        service.AddScoped<IFacultyRepository, FacultyRepository>();
        service.AddScoped<IFacultyDepartmentRepository, FacultyDepartmentRepository>();

        //Checkers 
        service.AddScoped<ISubjectNameMustBeUniqueChecker, SubjectNameMustBeUniqueChecker>();
        service.AddScoped<IStudentIdMustBeUniqueChecker, StudentIdMustBeUniqueChecker>();
        service.AddScoped<IStudentFirstNameMustBeInRangeChecker, StudentFirstNameMustBeInRangeChecker>();
        service.AddScoped<ITeacherFirstNameMustBeInRangeChecker, TeacherFirstNameMustBeInRangeChecker>();
        service.AddScoped<IGroupNameMustBeUniqueChecker, GroupNameMustBeUniqueChecker>();
        service.AddScoped<IFacultyNameMustBeUniqueChecker, FacultyNameMustBeUniqueChecker>();
        service.AddScoped<IDepartmentNameMustBeUniqueChecker, DepartmentNameMustBeUniqueChecker>();
    }
}