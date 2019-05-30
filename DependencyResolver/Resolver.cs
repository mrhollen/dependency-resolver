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

        private bool TestDependencies(List<Package> dependencies)
        {
            packagesSeen.AddRange(dependencies);

            foreach(var dependency in dependencies)
            {
                if(packagesSeen.ContainsPackageWithConflictingVersion(dependency))
                {
                    // We've seen a package with a different version number
                    return false;
                }
                else
                {
                    // Test the dependencies of the dependency
                    return TestDependencies(dependency.Dependencies);
                }
            }

            // There were no more dependencies so we're good to go
            return true;
        }
    }
}