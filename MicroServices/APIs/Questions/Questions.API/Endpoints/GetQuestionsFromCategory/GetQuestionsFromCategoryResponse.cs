using Domain.Common.DTOs;

namespace Questions.API.Endpoints.GetQuestionsFromCategory;

public class GetQuestionsFromCategoryResponse
{
    public IEnumerable<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}