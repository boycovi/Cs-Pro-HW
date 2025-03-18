using MediatR;
using Microsoft.EntityFrameworkCore;
using University.Application.Domain.Teachers.Queries.GetTeacher;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Teachers.Queries;

public class GetTeacherQueryHandler : IRequestHandler<GetTeachersQuery, TeacherDto[]>
{
    private readonly UniversityDbContext _universityDbContext;

    public GetTeacherQueryHandler(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<TeacherDto[]> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _universityDbContext.Teachers.AsNoTracking()
            .Include(x => x.Subjects);

        var skip = (request.PageNumber - 1) * request.PageSize;
        var teacherDtos = await sqlQuery
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new TeacherDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MiddleName = x.MiddleName,
                SubjectCollection = x.Subjects.Select(subject => new SubjectsDto()
                {
                    SubjectsId = subject.SubjectId,
                    SubjectName = subject.Subject.Name,
                    SubjectCode = subject.Subject.Code
                }).ToList()
            })
            .ToArrayAsync(cancellationToken);

        return teacherDtos;
    }
}