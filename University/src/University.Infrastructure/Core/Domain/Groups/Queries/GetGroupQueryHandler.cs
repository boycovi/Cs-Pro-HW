using MediatR;
using Microsoft.EntityFrameworkCore;
using University.Application.Domain.Groups.Queries.GetGroup;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Groups.Queries;

public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDto[]>
{
    private readonly UniversityDbContext _universityDbContext;

   public GetGroupQueryHandler(UniversityDbContext universityDbContext)
   {
        _universityDbContext = universityDbContext;
   }

    public async Task<GroupDto[]> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _universityDbContext.Groups.AsNoTracking()
            .Include(x => x.Students);

        var skip = (request.PageNumber - 1) * request.PageSize;
        var data = await sqlQuery
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new GroupDto()
            {
                Id = x.Id,
                Name = x.Name,
                StudentsCollection = x.Students.Select(student => new StudentsDto()
                {
                    StudentId = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    MiddleName = student.MiddleName
                }).ToArray()
            })
            .ToArrayAsync(cancellationToken);
        return data;
    }
}