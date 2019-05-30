using System.Collections.Generic;

namespace DependencyResolver
{
    public class DependencyFile
    {
        public List<Package> Packages { get; set; }
        public List<(Package Parent, Package Dependency)> Dependencies { get; set; }

        public DependencyFile()
        {
            Packages = new List<Package>();
            Dependencies = new List<(Package Parent, Package Dependency)>();
        }
    }
}