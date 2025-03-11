using FluentAssertions;
using Serilog;
using ShopAnalytics.Helpers;
using Xunit;

namespace ShopAnalyticsTests.HelpersTests;

public class TaskHelperTests
{
    public TaskHelperTests()
    {
        // Ensure Serilog does not log during tests
        Log.Logger = new LoggerConfiguration().CreateLogger();
    }

    [Fact]
    public async Task ExecuteTasksAsync_TwoTasks_ShouldReturnResults()
    {
        // Arrange
        Task<int> task1 = Task.FromResult(10);
        Task<string> task2 = Task.FromResult("Hello");

        // Act
        var (result1, result2) = await TaskHelper.ExecuteTasksAsync(task1, task2);

        // Assert
        result1.Should().Be(10);
        result2.Should().Be("Hello");
    }

    [Fact]
    public async Task ExecuteTasksAsync_ThreeTasks_ShouldReturnResults()
    {
        // Arrange
        Task<int> task1 = Task.FromResult(5);
        Task<string> task2 = Task.FromResult("World");
        Task<bool> task3 = Task.FromResult(true);

        // Act
        var (result1, result2, result3) = await TaskHelper.ExecuteTasksAsync(task1, task2, task3);

        // Assert
        result1.Should().Be(5);
        result2.Should().Be("World");
        result3.Should().BeTrue();
    }

    [Fact]
    public async Task ExecuteTasksAsync_FourTasks_ShouldReturnResults()
    {
        // Arrange
        Task<int> task1 = Task.FromResult(1);
        Task<string> task2 = Task.FromResult("A");
        Task<bool> task3 = Task.FromResult(false);
        Task<double> task4 = Task.FromResult(2.5);

        // Act
        var (result1, result2, result3, result4) = await TaskHelper.ExecuteTasksAsync(task1, task2, task3, task4);

        // Assert
        result1.Should().Be(1);
        result2.Should().Be("A");
        result3.Should().BeFalse();
        result4.Should().Be(2.5);
    }

    [Fact]
    public async Task ExecuteTasksAsync_ShouldThrowInvalidOperationException_WhenTaskFails()
    {
        // Arrange
        Task<int> task1 = Task.FromResult(10);
        Task<string> task2 = Task.FromException<string>(new Exception("Test failure"));

        // Act
        Func<Task> act = async () => await TaskHelper.ExecuteTasksAsync(task1, task2);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Task execution failed: Test failure");
    }

    [Fact]
    public async Task ExecuteTasksAsync_ShouldThrowOperationCanceledException_WhenTaskIsCanceled()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        Task<int> task1 = Task.FromResult(10);
        Task<string> task2 = Task.Run(() =>
        {
            cts.Cancel();
            cts.Token.ThrowIfCancellationRequested();
            return Task.FromResult("Should not happen");
        }, cts.Token);

        // Act
        Func<Task> act = async () => await TaskHelper.ExecuteTasksAsync(task1, task2);

        // Assert
        await act.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task ExecuteTasksAsync_ThreeTasks_ShouldThrowInvalidOperationException_OnFailure()
    {
        // Arrange
        Task<int> task1 = Task.FromResult(10);
        Task<string> task2 = Task.FromResult("Hello");
        Task<bool> task3 = Task.FromException<bool>(new Exception("Third task failed"));

        // Act
        Func<Task> act = async () => await TaskHelper.ExecuteTasksAsync(task1, task2, task3);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Task execution failed: Third task failed");
    }

    [Fact]
    public async Task ExecuteTasksAsync_FourTasks_ShouldThrowInvalidOperationException_OnFailure()
    {
        // Arrange
        Task<int> task1 = Task.FromResult(10);
        Task<string> task2 = Task.FromResult("Hello");
        Task<bool> task3 = Task.FromResult(true);
        Task<double> task4 = Task.FromException<double>(new Exception("Fourth task failed"));

        // Act
        Func<Task> act = async () => await TaskHelper.ExecuteTasksAsync(task1, task2, task3, task4);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Task execution failed: Fourth task failed");
    }
    

}