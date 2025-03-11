using Serilog;

namespace ShopAnalytics.Helpers;

public static class TaskHelper
{
        public static async Task<(T1, T2)> ExecuteTasksAsync<T1, T2>(
        Task<T1> task1, 
        Task<T2> task2, 
        string errorMessage = "Task execution failed")
    {
        try
        {
            await Task.WhenAll(task1, task2).ConfigureAwait(false);
            return (await task1, await task2);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (AggregateException ex)
        {
            HandleAggregateException(ex, errorMessage);
            throw; 
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"{errorMessage}: {ex.Message}", ex);
        }
    }
    
    public static async Task<(T1, T2, T3)> ExecuteTasksAsync<T1, T2, T3>(
        Task<T1> task1, 
        Task<T2> task2, 
        Task<T3> task3, 
        string errorMessage = "Task execution failed")
    {
        try
        {
            await Task.WhenAll(task1, task2, task3).ConfigureAwait(false);
            return (await task1, await task2, await task3);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (AggregateException ex)
        {
            HandleAggregateException(ex, errorMessage);
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"{errorMessage}: {ex.Message}", ex);
        }
    }
    
    public static async Task<(T1, T2, T3, T4)> ExecuteTasksAsync<T1, T2, T3, T4>(
        Task<T1> task1, 
        Task<T2> task2, 
        Task<T3> task3, 
        Task<T4> task4, 
        string errorMessage = "Task execution failed")
    {
        try
        {
            await Task.WhenAll(task1, task2, task3, task4).ConfigureAwait(false);
            return (await task1, await task2, await task3, await task4);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (AggregateException ex)
        {
            HandleAggregateException(ex, errorMessage);
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"{errorMessage}: {ex.Message}", ex);
        }
    }

    private static void HandleAggregateException(AggregateException ex, string errorMessage)
    {
        ex.Handle(innerEx =>
        {
            Log.Error(innerEx, errorMessage);
            
            return true;
        });
        
    }
}