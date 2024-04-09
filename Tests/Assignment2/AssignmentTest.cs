using BfTestAssignment.Assignment2;
using NUnit.Framework;

namespace Tests.Assignment2;

public class AssignmentTest
{
    [Test]
    public void MainCaseTest()
    {
        var row = Enumerable.Range(1, 10).Select(x => (double)x);

        Assert.That(Assignment.GetRowSums(row),
            Is.EquivalentTo(new List<double> {1, 3, 6, 10, 15, 21, 28, 36, 45, 55}));
    }

    [Test]
    public void EmptyTest()
    {
        var row = Enumerable.Empty<double>();

        Assert.That(Assignment.GetRowSums(row),
            Is.EquivalentTo(Enumerable.Empty<double>()));
    }

    [Test]
    public void SingleElementTest()
    {
        var row = new []{1d}.AsEnumerable();

        Assert.That(Assignment.GetRowSums(row),
            Is.EquivalentTo(new List<double> {1}));
    }
}