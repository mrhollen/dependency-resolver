using System;
using System.Collections;
using System.Collections.Generic;

namespace DependencyResolver
{
    public class PackageCollection
    {
        private readonly IDictionary<string, HashSet<string>> packages = new Dictionary<string, HashSet<string>>();

        public void Add(Package package)
        {
            if(this.packages.ContainsKey(package.Name))
            {
                this.packages[package.Name].Add(package.Version);
            }
            else 
            {
                this.packages.Add(package.Name, new HashSet<string>(){package.Version});
            }
        }

        public void AddRange(IEnumerable<Package> collection)
        {
            foreach(var item in collection)
            {
                this.Add(item);
            }
        }

        public bool ContainsPackage(Package package)
        {
            return this.packages.ContainsKey(package.Name) && this.packages[package.Name].Contains(package.Version);
        }

        public bool ContainsPackageWithConflictingVersion(Package package)
        {
            if(this.packages.TryGetValue(package.Name, out var versions))
            {
                foreach(var version in versions)
                {
                    if(version != package.Version)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}