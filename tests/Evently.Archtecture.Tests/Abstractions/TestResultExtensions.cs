

using FluentAssertions;
using NetArchTest.Rules;

namespace Evently.Archtecture.Tests.Abstractions;
internal static class TestResultExtensions
{
    internal static void ShouldBesuccessful(this TestResult testResult)
    {
        testResult.FailingTypes?.Should().BeEmpty();
    }
}
