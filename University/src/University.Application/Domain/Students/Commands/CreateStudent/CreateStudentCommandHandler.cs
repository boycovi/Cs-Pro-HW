using MediatR;
using University.Core.Common;
using University.Core.Domain.Students.Common;
using University.Core.Domain.Students.Models;

namespace University.Application.Domain.Students.Commands.CreateStudent;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IStudentsRepository _studentsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStudentCommandHandler(IStudentsRepository studentsRepository, IUnitOfWork unitOfWork)
    {
        _studentsRepository = studentsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
    {
        var student = Student.Create(command.FirstName, command.LastName, command.MiddleName, command.GroupId);
        await _studentsRepository.AddAsync(student);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return student.Id;
    }
}