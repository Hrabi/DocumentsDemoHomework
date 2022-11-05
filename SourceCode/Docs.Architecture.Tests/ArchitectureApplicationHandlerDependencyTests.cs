namespace Docs.Architecture.Tests;

[TestClass]
public class ArchitectureApplicationHandlerDependencyTests
{
  // Writing ArchUnit style tests for .Net and C# to enforce architecture rules
  // https://www.ben-morris.com/writing-archunit-style-tests-for-net-and-c-for-self-testing-architectures/

  [TestMethod]
  public void Application_Handler_Should_HaveDependencyOnDomain()
  {
    // Arrange
    var assembly = Application.AssemblyReference.Assembly;

    // Act
    var testResult = Types.InAssembly(assembly).That().HaveNameEndingWith("Handler").Should().HaveDependencyOn(Namespaces.Domain).GetResult();

    // Assert
    Assert.IsTrue(testResult.IsSuccessful);
  }
}

