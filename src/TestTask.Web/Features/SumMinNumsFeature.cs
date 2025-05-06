using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Commands;
using TestTask.Application.Commands.SumMinNums;
using TestTask.Application.Models;
using TestTask.Web.Endpoints;

namespace TestTask.Web.Features;

public class SumMinNumsFeature : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("math-second", HandleAsync);
    }
    
    private async Task<IResult> HandleAsync(int[] nums, [FromServices] SumMinNumsCommandHandler handler)
    {
        var command = new SumMinNumsCommand(nums);
        
        var sumMinNumsResult = await handler.HandleAsync(command);
        if (sumMinNumsResult.IsFailure)
        {
            return Results.BadRequest(new Envelope(null, [sumMinNumsResult.Error]));
        }
        
        var envelope = new Envelope(sumMinNumsResult.Value, null);
        
        return Results.Ok(envelope);
    }
}