namespace Docs.Architecture.Tests;

[TestClass]
public class ArchitectureProjectDependencyTests
{
  // Writing ArchUnit style tests for .Net and C# to enforce architecture rules
  // https://www.ben-morris.com/writing-archunit-style-tests-for-net-and-c-for-self-testing-architectures/

  [TestMethod]
  public void DocsDomain_Should_Not_HaveDependencyOnOtherProjects()
  {
    // Arrange
    var assembly = Domain.AssemblyReference.Assembly;
    var projects = new[]
    {
        Namespaces.Application,
        Namespaces.Infrastructure,
        Namespaces.Presentation,
        Namespaces.WebApp
      };

    // Act
    var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAny(projects).GetResult();

    // Assert
    Assert.IsTrue(testResult.IsSuccessful);
  }

  [TestMethod]
  public void DocsApplication_Should_Not_HaveDependencyOnOtherProjects()
  {
    // Arrange
    var assembly = Application.AssemblyReference.Assembly;
    var projects = new[]
    {
        Namespaces.Infrastructure,
        Namespaces.Presentation,
        Namespaces.WebApp
      };

    // Act
    var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAny(projects).GetResult();

    // Assert
    Assert.IsTrue(testResult.IsSuccessful);
  }

  [TestMethod]
  public void DocsInfrastructure_Should_Not_HaveDependencyOnOtherProjects()
  {
    // Arrange
    var assembly = Infrastructure.AssemblyReference.Assembly;
    var projects = new[]
    {
        Namespaces.Presentation,
        Namespaces.WebApp
      };

    // Act
    var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAny(projects).GetResult();

    // Assert
    Assert.IsTrue(testResult.IsSuccessful);
  }

  [TestMethod]
  public void DocsPresentation_Should_Not_HaveDependencyOnOtherProjects()
  {
    // Arrange
    var assembly = Presentation.AssemblyReference.Assembly;
    var projects = new[]
    {
        Namespaces.Infrastructure,
        Namespaces.WebApp
      };

    // Act
    var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAny(projects).GetResult();

    // Assert
    Assert.IsTrue(testResult.IsSuccessful);
  }

}
