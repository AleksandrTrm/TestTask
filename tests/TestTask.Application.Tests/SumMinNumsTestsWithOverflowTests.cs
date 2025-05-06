using FluentAssertions;
using TestTask.Application.Commands;
using TestTask.Application.Commands.SumMinNums;
using TestTask.Application.Commands.SumMinNumsWithOverflow;
using Xunit;

namespace TestTask.Application.Tests;

public class SumMinNumsTestsWithOverflowTests
{
    [Fact]
    public async Task SumMinNums_ShouldReturn_IntegerOverflow_When_NumsIsTooLong()
    {
        //arrange
        int[] nums = [2147483647, 500];
        
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();
        
        //act
        var result = await handler.HandleAsync(command);
        
        //assert
        result.IsFailure.Should().BeTrue();
        result.Error.ErrorCode.Should().Be("integer.overflow");
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_BadRequestError_When_Array_IsLess_Then_2_Elements()
    {
        //arrange
        int[] nums = [25];
        
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();
        
        //act
        
        var result = await handler.HandleAsync(command);
        
        //assert
        
        result.IsFailure.Should().BeTrue();
        result.Error.ErrorCode.Should().Be("invalid.input");
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_50_When_Input_Is_25_and_25()
    {
        //arrange
        int[] nums = [25, 25];
        
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();

        //act
        var result = await handler.HandleAsync(command);
        
        //assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(50);
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_0_When_Input_Is_0_and_0()
    {
        // Arrange
        int[] nums = [0, 0];
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(0);
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_MinValue_When_Input_Is_MinInt_and_0()
    {
        // Arrange
        int[] nums = [int.MinValue, 0];
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(int.MinValue);
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_IntegerOverflow_When_Input_Is_MaxInt_and_MaxInt()
    {
        // Arrange
        int[] nums = [int.MaxValue, int.MaxValue];
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.ErrorCode.Should().Be("integer.overflow");
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_NegativeSum_When_Input_Is_Two_NegativeNumbers()
    {
        // Arrange
        int[] nums = [-10, -20];
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(-30);
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_IntegerOverflow_When_Input_Is_MinInt_and_MinInt()
    {
        // Arrange
        int[] nums = [int.MinValue, int.MinValue];
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.ErrorCode.Should().Be("integer.overflow");
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_10_When_Input_Is_Unordered_Array()
    {
        // Arrange
        int[] nums = [100, 5, 200, 10, 300];
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(15);
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_CorrectSum_When_Input_Has_Duplicates()
    {
        // Arrange
        int[] nums = [5, 5, 10, 10];
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(10); // 5 + 5
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_BadRequestError_When_Array_Is_Empty()
    {
        // Arrange
        int[] nums = [];
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.ErrorCode.Should().Be("invalid.input");
    }
    
    [Fact]
    public async Task SumMinNums_ShouldReturn_CorrectSum_When_Input_Is_Large_Array()
    {
        // Arrange
        int[] nums = Enumerable.Range(1, 100_000_000).ToArray();

        nums[50_000_000] = 0;
        
        var command = new SumMinNumsCommand(nums);
        var handler = new SumMinNumsWithOverflowCommandHandler();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(1); // 1 + 2
    }
}