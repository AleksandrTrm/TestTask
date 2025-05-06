using CSharpFunctionalExtensions;
using TestTask.Application.Models;

namespace TestTask.Application.Commands.SumMinNums;

public class SumMinNumsCommandHandler
{
    
    //O(n), скорость перебора массива максимальная
    public Task<Result<long, Error>> HandleAsync(SumMinNumsCommand command)
    {
        var nums = command.Nums;
        
        if (nums.Length <= 1)
        {
            var error = new Error("Array length must be greater than 1", "invalid.input", 400);
            
            return Task.FromResult<Result<long, Error>>(error);
        }
        
        var firstMinNumber = long.MaxValue;
        var secondMinNumber = long.MaxValue;
        
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] < firstMinNumber)
            {
                secondMinNumber = firstMinNumber;
                firstMinNumber = nums[i];

                continue;
            }

            if (nums[i] < secondMinNumber)
            {
                secondMinNumber = nums[i];
            }
        }
        
        return Task.FromResult<Result<long, Error>>(firstMinNumber + secondMinNumber);
    }
}