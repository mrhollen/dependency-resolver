using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DependencyResolver
{
    public static class FileHelper
    {
        public static DependencyFile ParseDependencyFile(string filePath)
        {
            var fileContents = File.ReadAllLines(filePath);
            var dependencyFile = new DependencyFile();

            if(fileContents.Length > 2 && int.TryParse(fileContents[0], out var numberOfPackages))
            {
                var packagesToInstall = fileContents.Skip(1).Take(numberOfPackages);
                List<string> dependenciesToInstall = new List<string>();
                
                if(fileContents.Length > 3)
                {
                    if(int.TryParse(fileContents[numberOfPackages + 1], out var numberOfDependencies))
                    {
                        dependenciesToInstall = fileContents.Skip(numberOfPackages+2).Take(numberOfDependencies).ToList();
                    }

                    foreach(var packageData in packagesToInstall)
                    {
                        var data = packageData.Split(',');
                        if(data.Length > 1)
                        {
                            dependencyFile.Packages.Add(new Package(data[0], data[1]));
                        }
                    }

                    foreach(var dependencyData in dependenciesToInstall)
                    {
                        var data = dependencyData.Split(',');
                        if(data.Length == 4)
                        {
                            dependencyFile.Dependencies.Add((Parent: new Package(data[0], data[1]), Dependency: new Package(data[2], data[3])));
                        }
                    }
                }
            }

            return dependencyFile;
        }
    }
}