using BfTestAssignment.Assignment1;
using NUnit.Framework;
using Task = BfTestAssignment.Assignment1.Task;

namespace Tests.Assignment1;

public class TasksHandlerTest
{
    private TasksHandler _tasksHandler = null!;
    private readonly Dictionary<string, DateTime> _timeLog = new();

    [SetUp]
    public void SetUp()
    {
        _tasksHandler = new TasksHandler();
        _timeLog.Clear();
    }

    [Test]
    public async System.Threading.Tasks.Task TasksRunSequentially()
    {
        _tasksHandler.AddTask(CreateTask(1, 200));
        _tasksHandler.AddTask(CreateTask(2, 10));
        _tasksHandler.AddTask(CreateTask(3, 100));

        await _tasksHandler.RunAsync();

        Assert.That(_timeLog["Task 1 started"] < _timeLog["Task 1 completed"], Is.True);
        Assert.That(_timeLog["Task 1 completed"] < _timeLog["Task 2 started"], Is.True);
        Assert.That(_timeLog["Task 2 started"] < _timeLog["Task 2 completed"], Is.True);
        Assert.That(_timeLog["Task 2 completed"] < _timeLog["Task 3 started"], Is.True);
        Assert.That(_timeLog["Task 3 started"] < _timeLog["Task 3 completed"], Is.True);
    }

    [Test]
    public async System.Threading.Tasks.Task EmptyQueueDoesNotFall()
    {
        await _tasksHandler.RunAsync();

        Assert.That(_timeLog.Count, Is.EqualTo(0));
    }

    [Test]
    public async System.Threading.Tasks.Task FailedTaskSkipped()
    {
        _tasksHandler.AddTask(CreateTask(1, 100));
        _tasksHandler.AddTask(CreateTask(2, 100, new Exception("EXPECTED")));
        _tasksHandler.AddTask(CreateTask(3, 100));

        await _tasksHandler.RunAsync();

        Assert.That(_timeLog["Task 1 completed"] < _timeLog["Task 2 started"], Is.True);
        Assert.That(_timeLog["Task 1 completed"] < _timeLog["Task 3 completed"], Is.True);
    }

    private Task CreateTask(int taskIndex, int? delay = null, Exception? ex = null)
    {
        return () =>
        {
            Log($"Task {taskIndex} started");
            if (delay != null)
            {
                Thread.Sleep(delay.Value); // эмуляция работы
            }

            if (ex != null)
            {
                throw ex;
            }

            Log($"Task {taskIndex} completed");
        };
    }

    private void Log(string message)
    {
        var time = DateTime.Now;
        _timeLog[message] = time;
        Console.WriteLine($"{time:HH:mm:ss.fffffff}, [Test]: {message}");
    }
}