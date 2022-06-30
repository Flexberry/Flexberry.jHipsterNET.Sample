
using SampleProject.Domain;
using SampleProject.Dto;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class DepartmentGetQuery : IRequest<Department>
    {
        public long Id { get; set; }
    }
}
