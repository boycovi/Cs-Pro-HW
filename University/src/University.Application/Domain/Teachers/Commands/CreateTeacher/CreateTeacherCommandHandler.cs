using MediatR;
using University.Core.Common;
using University.Core.Domain.Teachers.Common;
using University.Core.Domain.Teachers.Models;

namespace University.Application.Domain.Teachers.Commands.CreateTeacher;

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Guid>
{
    private readonly ITeachersRepository _teachersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTeacherCommandHandler(ITeachersRepository teachersRepository, IUnitOfWork unitOfWork)
    {
        _teachersRepository = teachersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateTeacherCommand command, CancellationToken cancellationToken)
    {
        var teacher = Teacher.Create(command.FirstName, command.LastName, command.MiddleName);
        await _teachersRepository.AddAsync(teacher);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return teacher.Id;
    }
}