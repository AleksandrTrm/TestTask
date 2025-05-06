using CSharpFunctionalExtensions;
using TestTask.Application.Models;

namespace TestTask.Application.Commands.SumMinNumsWithOverflow;

public class SumMinNumsWithOverflowCommandHandler
{
    public Task<Result<int, Error>> HandleAsync(SumMinNumsCommand command)
    {
        var nums = command.Nums;
        
        if (nums.Length <= 1)
        {
            var error = new Error("Array length must be greater than 1", "invalid.input", 400);
            
            return Task.FromResult<Result<int, Error>>(error);
        }
        
        var firstMinNumber = int.MaxValue;
        var secondMinNumber = int.MaxValue;
        
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

        int result = 0;
        try
        {
            result = checked(firstMinNumber + secondMinNumber);
        }
        catch
        {
            var error = new Error("Integer overflow", "integer.overflow", 400);
            
            return Task.FromResult<Result<int, Error>>(error);
        }
        
        
        return Task.FromResult<Result<int, Error>>(result);
    }
}