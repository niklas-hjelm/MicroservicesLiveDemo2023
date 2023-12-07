using FastEndpoints;
using MongoDB.Bson;
using Quiz.DataAccess;

namespace Quiz.API.Endpoints.UpdateQuiz;

public class Handler(IQuizRepository quizRepository) : Endpoint<Request,Response>
{
    public override void Configure()
    {
        Put("quiz/{quizId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var quiz = await quizRepository.GetByIdAsync(ObjectId.Parse(req.QuizId));

        quiz.Title = req.Quiz.Title;
        quiz.Description = req.Quiz.Description;
        quiz.Categories = req.Quiz.Categories;
        quiz.Questions = req.Quiz.Questions;

        await quizRepository.UpdateAsync(quiz);
        await SendAsync(new Response(), 200, ct);
    }
}