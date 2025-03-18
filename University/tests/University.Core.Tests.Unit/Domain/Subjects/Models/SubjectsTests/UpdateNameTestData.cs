using System.Collections;

namespace University.Core.Tests.Unit.Domain.Subjects.Models.SubjectsTests;

class UpdateNameTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "1Test", "2Test" };
        yield return new object[] { "2Test", "3Test" };
        yield return new object[] { "3Test", "10Test" };
        yield return new object[] { "4Test", "9Test" };
        yield return new object[] { "5Test", "8Test" };
        yield return new object[] { "6Test", "7Test" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}