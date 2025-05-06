using FluentAssertions;
using TestTask.Application.Commands;
using TestTask.Application.Commands.SumMinNumsLinq;
using TestTask.Application.Commands.SumMinNumsWithOverflow;
using Xunit;

namespace TestTask.Application.Tests;

public class SumMinNumsLinqTest
{
    [Fact]
    public async Task SumMinNums_ShouldReturn_CorrectSum_When_Input_Is_Large_Array()
    {
        // Arrange
        int[] nums = Enumerable.Range(1, 100_000_000).ToArray();

        nums[50_000_000] = 0;
        
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsLinqCommandHandler();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(1); // 1 + 2
    }
}