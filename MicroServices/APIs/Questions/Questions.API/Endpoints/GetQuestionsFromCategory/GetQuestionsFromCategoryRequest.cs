namespace Questions.API.Endpoints.GetQuestionsFromCategory;

public class GetQuestionsFromCategoryRequest
{
    public string Category { get; set; }
    public int Amount { get; set; }
}