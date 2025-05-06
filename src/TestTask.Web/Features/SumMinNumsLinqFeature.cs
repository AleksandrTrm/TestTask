using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Commands;
using TestTask.Application.Commands.SumMinNumsLinq;
using TestTask.Application.Models;
using TestTask.Web.Endpoints;

namespace TestTask.Web.Features;

public class SumMinNumsLinqFeature : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("math-linq", HandleAsync);
    }

    private async Task<IResult> HandleAsync(int[] nums, [FromServices] SumMinNumsLinqCommandHandler handler)
    {
        var command = new SumMinNumsCommand(nums);
        var sumMinNumsResult = await handler.HandleAsync(command);
        if (sumMinNumsResult.IsFailure)
        {
            var envelope = new Envelope(null, [sumMinNumsResult.Error]);
            
            return Results.BadRequest(envelope);
        }

        return Results.Ok(sumMinNumsResult.Value);
    }
}