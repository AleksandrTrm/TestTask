using CSharpFunctionalExtensions;
using TestTask.Application.Models;

namespace TestTask.Application.Commands.SumMinNumsLinq;

public class SumMinNumsLinqCommandHandler
{
    //O(log n) скорость намнго ниже за счет сортировки массива 
    public Task<Result<long, Error>> HandleAsync(SumMinNumsCommand command)
    {
        var nums = command.Nums;
        
        if (nums.Length <= 1)
        {
            var error = new Error("Array length must be greater than 1", "invalid.input", 400);
            
            return Task.FromResult<Result<long, Error>>(error);
        }

        var minNum = nums.OrderBy(n => n).Take(2).ToList();

        var result = (long)minNum[0] + minNum[1];

        return Task.FromResult<Result<long, Error>>(result);
    } 
}