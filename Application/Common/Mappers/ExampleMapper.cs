using Application.Examples.Queries.GetExamples;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Common.Mappers;

[Mapper]
public partial class ExampleMapper
{
    public partial ExampleDto CreateDto(Example example);
    public partial List<ExampleDto> CreateDto(List<Example> example);
}