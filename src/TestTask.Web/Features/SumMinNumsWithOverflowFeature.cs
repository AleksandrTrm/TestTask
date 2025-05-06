using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Commands;
using TestTask.Application.Commands.SumMinNums;
using TestTask.Application.Commands.SumMinNumsWithOverflow;
using TestTask.Application.Models;
using TestTask.Web.Endpoints;

namespace TestTask.Web.Features;

public class SumMinNumsWithOverflowFeature : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("math-first", HandleAsync);
    }
    
    private async Task<IResult> HandleAsync(
        int[] nums, 
        [FromServices] SumMinNumsWithOverflowCommandHandler handler)
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