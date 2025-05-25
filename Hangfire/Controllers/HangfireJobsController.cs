using Microsoft.AspNetCore.Mvc;
using Hangfire;
using System;
using System.Threading; // Added for CancellationToken and Thread

namespace Hangfire.Controllers;

[ApiController]
[Route("[controller]")]
public class HangfireJobsController : ControllerBase
{
    /// <summary>
    /// Enqueues a fire-and-forget background job that executes immediately.
    /// The job can be cancelled via a CancellationToken.
    /// </summary>
    /// <returns>An IActionResult indicating the job was fired and its Job ID.</returns>
    [HttpGet]
    [Route("[action]")]
    public IActionResult FireAndForget()
    {
        // Hangfire will automatically inject a CancellationToken
        // into LongRunningTask if it's a parameter.
        var jobId = BackgroundJob.Enqueue(() => LongRunningTask("Fire And Forget (Cancellable with CancellationToken)", CancellationToken.None));
        return Ok($"Job Fired (Cancellable with CancellationToken). Job ID: {jobId}");
    }

    /// <summary>
    /// Schedules a background job to execute after a specified delay.
    /// The job can be cancelled via a CancellationToken.
    /// </summary>
    /// <returns>An IActionResult indicating the job was scheduled and its Job ID.</returns>
    [HttpGet]
    [Route("[action]")]
    public IActionResult FireAndForgetWithDelay()
    {
        var jobId = BackgroundJob.Schedule(() => LongRunningTask("Fire And Forget With Delay (Cancellable with CancellationToken)", CancellationToken.None), TimeSpan.FromSeconds(10));
        return Ok($"Job Fired With Delay (Cancellable with CancellationToken). Job ID: {jobId}");
    }

    /// <summary>
    /// Adds or updates a recurring background job that executes on a CRON schedule (minutely).
    /// The job can be cancelled via a CancellationToken.
    /// Recurring jobs are identified by a unique ID ("MyRecurringJob" in this case) rather than a dynamic one.
    /// </summary>
    /// <returns>An IActionResult indicating the recurring job was started or updated.</returns>
    [HttpGet]
    [Route("[action]")]
    public IActionResult StartRecurringJob()
    {
        RecurringJob.AddOrUpdate("MyRecurringJob", () => LongRunningTask("Recurring Job Fired (Cancellable with CancellationToken)", CancellationToken.None), Cron.Minutely);
        return Ok("Recurring Job Started/Updated (Cancellable with CancellationToken)");
    }

    /// <summary>
    /// Attempts to cancel (delete) a Hangfire job by its ID.
    /// If the job is running, its CancellationToken will be signalled.
    /// </summary>
    /// <param name="jobId">The ID of the Hangfire job to cancel.</param>
    /// <returns>An IActionResult indicating whether the deletion was successful.</returns>
    [HttpGet]
    [Route("[action]/{jobId}")] // Make jobId part of the route
    public IActionResult CancelJob(string jobId)
    {
        if (string.IsNullOrEmpty(jobId))
        {
            return BadRequest("Job ID cannot be null or empty.");
        }

        try
        {
            bool deleted = BackgroundJob.Delete(jobId);
            if (deleted)
            {
                return Ok($"Job {jobId} deletion requested. If it was running, it should be cancelled.");
            }
            else
            {
                return Ok($"Request to delete job {jobId} processed, but it might have already completed or did not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error trying to delete job {jobId}: {ex.Message}");
            return StatusCode(500, $"An error occurred while trying to delete job {jobId}.");
        }
    }


    /// <summary>
    /// A simulated long-running task that can be cancelled using CancellationToken.
    /// This method is intended to be called by Hangfire as a background job.
    /// </summary>
    /// <param name="message">Message to display during task execution.</param>
    /// <param name="cancellationToken">Standard .NET cancellation token provided by Hangfire.
    /// This token will be signalled if Hangfire requests the job to be cancelled (e.g., by deleting the job).
    /// </param>
    public void LongRunningTask(string message, CancellationToken cancellationToken) // Removed "= default"
    {
        Console.WriteLine($"Long Running Task Started: {message}. Job will run for approx 5 seconds if not cancelled.");
        try
        {
            for (int i = 0; i < 5; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Thread.Sleep(5000);
                Console.WriteLine($"Working... {i + 1}/5 for: {message}. To cancel, use the CancelJob endpoint with the Job ID.");
            }
            Console.WriteLine($"Message from task: {message}");
            Console.WriteLine($"Long Running Task Ended Gracefully: {message}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"Long Running Task Cancelled: {message}");
            Console.WriteLine($"Cleanup for cancelled task ({message}) would happen here.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in Long Running Task ({message}): {ex.Message}");
        }
    }
}
