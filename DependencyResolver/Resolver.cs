using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyResolver
{
    public class Resolver
    {
        private PackageCollection packagesSeen = new PackageCollection();

        public bool CanInstallAllPackages(List<Package> packages)
        {
            packagesSeen.AddRange(packages);

            foreach(var package in packages)
            {
                if(!TestDependencies(package.Dependencies))
                {
                    return false;
                }
            }
            
            return true;
        }

        // Depth first search for conflicting dependencies
        private bool TestDependencies(List<Package> dependencies)
        {
            var noConflict = true;
            packagesSeen.AddRange(dependencies);

            foreach(var dependency in dependencies)
            {
                if(packagesSeen.ContainsPackageWithConflictingVersion(dependency))
                {
                    // We've seen a package with a different version number
                    return false;
                }
                else if(dependency.Dependencies.Count > 0)
                {
                    // Test the dependencies of the dependency
                    noConflict = TestDependencies(dependency.Dependencies);
                }
            }

            // Return the results we've found
            return noConflict;
        }
    }
}