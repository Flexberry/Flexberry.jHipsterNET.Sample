
using SampleProject.Domain;
using SampleProject.Dto;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class EmployeeGetQuery : IRequest<Employee>
    {
        public long Id { get; set; }
    }
}
