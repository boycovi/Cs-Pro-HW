using MediatR;
using Microsoft.EntityFrameworkCore;
using University.Application.Domain.Subjects.Queries.GetSubjects;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Subjects.Queries;

public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, SubjectDto[]>
{
    private readonly UniversityDbContext _universityDbContext;

    public GetSubjectsQueryHandler(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<SubjectDto[]> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _universityDbContext.Subjects.AsNoTracking();
        var skip = (request.PageNumber - 1) * request.PageSize;
        var data = await sqlQuery
            .OrderBy(subject => subject.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(subject => new SubjectDto()
            {
                Id = subject.Id,
                Name = subject.Name,
                Code = subject.Code
            })
            .ToArrayAsync(cancellationToken);
        return data;
    }
}