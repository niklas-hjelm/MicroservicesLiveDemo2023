using Domain.Common.DTOs;

namespace Questions.API.Endpoints.GetAll;

public class GetAllResponse
{
    public IEnumerable<QuestionDto> Questions { get; set; }
}