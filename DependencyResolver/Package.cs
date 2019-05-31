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

        public static bool operator ==(Package a, Package b)
        {
            return a.Name == b.Name && a.Version == b.Version;
        }

        public static bool operator !=(Package a, Package b)
        {
            return a.Name != b.Name || a.Version != b.Version;
        }

        public bool Equals(Package b)
        {
            if(Name == b.Name && Version == b.Version)
            {
                return true;
            }

            return false;
        }
    }
}