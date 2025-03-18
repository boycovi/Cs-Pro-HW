using MediatR;
using Microsoft.EntityFrameworkCore;
using University.Application.Domain.Students.Queries.GetStudent;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Students.Queries;

public class GetStudentQueryHandler : IRequestHandler<GetStudentsQuery, StudentDto[]>
{
    private readonly UniversityDbContext _universityDbContext;

    public GetStudentQueryHandler(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<StudentDto[]> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _universityDbContext.Students.AsNoTracking()
            .Include(x => x.Group);

        var skip = (request.PageNumber - 1) * request.PageSize;
        var data = await sqlQuery
            .OrderBy(student => student.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(student => new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                GroupId = student.GroupId,
                GroupName = student.Group.Name
            })
            .ToArrayAsync(cancellationToken);
        return data;
    }
}