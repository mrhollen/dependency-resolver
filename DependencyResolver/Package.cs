using System.Collections.Generic;

namespace DependencyResolver
{
    public class Package
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public List<Package> Dependencies { get; set; }
        public Package(string name, string version)
        {
            this.Name = name;
            this.Version = version;

            Dependencies = new List<Package>();
        }
    }
}