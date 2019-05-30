using System.Linq;
using Xunit;

namespace DependencyResolver.Tests
{
    public class PackageBuilderTests
    {
        [Fact]
        public void CanBuildSimplePackages()
        {
            // Setup
            var packageBuilder = new PackageBuilder();
            var dependencyFile = new DependencyFile();

            dependencyFile.Packages.Add(new Package("p1", "v1"));

            // Test
            var results = packageBuilder.BuildFromFile(dependencyFile);

            // Assert
            Assert.True(results.Any(r => r.Name == "p1" && r.Version == "v1"));
        }

        [Fact]
        public void CanBuildComplexPackages()
        {
            // Setup
            var packageBuilder = new PackageBuilder();
            var dependencyFile = new DependencyFile();

            dependencyFile.Packages.Add(new Package("p1", "v1"));

            dependencyFile.Dependencies.Add((new Package("p1", "v1"), new Package("p2", "v1")));
            dependencyFile.Dependencies.Add((new Package("p2", "v1"), new Package("p3", "v1")));

            // Test
            var results = packageBuilder.BuildFromFile(dependencyFile);

            // Assert
            Assert.True(results.Any(r => r.Name == "p1" && r.Version == "v1" 
                && r.Dependencies.Any(d => d.Name == "p2" && d.Version == "v1"
                    && d.Dependencies.Any(dd => dd.Name == "p3" && dd.Version == "v1"))));
        }
    }
}