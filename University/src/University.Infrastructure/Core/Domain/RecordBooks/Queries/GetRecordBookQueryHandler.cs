using MediatR;
using Microsoft.EntityFrameworkCore;
using University.Application.Domain.RecordBooks.Queries.GetRecordBook;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.RecordBooks.Queries;

public class GetRecordBookQueryHandler : IRequestHandler<GetRecordBookQuery, RecordBookDto[]>
{
    private readonly UniversityDbContext _universityDbContext;

    public GetRecordBookQueryHandler(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }


    public RecordBookDto[] GetRecordBooks(int pageSize, int pageNumber)
    {
        var sqlQuery = _universityDbContext.RecordBooks.AsNoTracking()
            .Include(x=> x.Subjects)
            .Include(x=> x.Student)
            .ToArray();

        var skip = (pageNumber - 1) * pageSize;
        var recordBookDttos = sqlQuery
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(pageSize)
            .Select(x => new RecordBookDto()
            {
                Id = x.Id,
                StudentId = x.StudentId,
                Marks = x.Subjects.Select(subject=> new MarkDto()
                {
                    Grade = subject.Grade,
                    SubjectId = subject.SubjectId
                }).ToList()
            }).ToArray();

        return recordBookDttos;
    }

    public async Task<RecordBookDto[]> Handle(GetRecordBookQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _universityDbContext.RecordBooks.AsNoTracking()
            .Include(x => x.Subjects)
            .Include(x => x.Student);

        var skip = (request.PageNumber - 1) * request.PageSize;
        var recordBookDttos = await sqlQuery
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new RecordBookDto()
            {
                Id = x.Id,
                StudentId = x.StudentId,
                FirstName = x.Student.FirstName,
                LastName = x.Student.LastName,
                MiddleName = x.Student.MiddleName,
                Marks = x.Subjects.Select(subject => new MarkDto()
                {
                    SubjectId = subject.SubjectId,
                    SubjectName = subject.Subject.Name,
                    Grade = subject.Grade
                }).ToList()
            }).ToArrayAsync(cancellationToken);

        return recordBookDttos;
    }
}