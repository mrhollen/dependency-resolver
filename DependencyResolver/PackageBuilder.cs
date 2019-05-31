using System.Collections.Generic;
using System.Linq;

namespace DependencyResolver
{
    public class PackageBuilder
    {
        public List<Package> BuildFromFile(DependencyFile dependencyFile)
        { 
            var packages = new List<Package>();
            packages.AddRange(dependencyFile.Packages);

            foreach(var package in packages)
            {
                foreach(var dependency in dependencyFile.Dependencies)
                {
                    AddDependencies(package, dependency);
                }
            }

            return packages;
        }

        // Build up our dependencies recursively
        private void AddDependencies(Package parent, (Package Parent, Package Dependency) dependencyPair)
        {
            if(dependencyPair.Parent == parent)
            {
                parent.Dependencies.Add(dependencyPair.Dependency);
            }
            else
            {
                foreach(var dependency in parent.Dependencies)
                {
                    AddDependencies(dependency, dependencyPair);
                }
            }
        }
    }
}