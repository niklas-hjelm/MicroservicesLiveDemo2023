using Domain.Common.DTOs;

namespace Questions.API.Endpoints.PostNew;

public class PostNewRequest
{
    public QuestionDto? NewQuestion { get; set; }
}