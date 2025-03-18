using MediatR;
using University.Core.Common;
using University.Core.Domain.Teachers.Common;
using University.Core.Domain.Teachers.Models;

namespace University.Application.Domain.Teachers.Commands.CreateTeacherSubject;

public class CreateTeacherSubjectCommandHandler : IRequestHandler<CreateTeacherSubjectCommand, Guid>
{
    private readonly ITeacherSubjectsRepository _teacherSubjectsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTeacherSubjectCommandHandler(ITeacherSubjectsRepository teacherSubjectsRepository, IUnitOfWork unitOfWork)
    {
        _teacherSubjectsRepository = teacherSubjectsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateTeacherSubjectCommand command, CancellationToken cancellationToken)
    {
        var teacher = TeacherSubject.Create(command.TeacherId, command.SubjectId);
        await _teacherSubjectsRepository.AddAsync(teacher);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return teacher.TeacherId;
    }
}