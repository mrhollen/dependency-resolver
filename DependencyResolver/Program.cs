using System;
using System.IO;

namespace DependencyResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var file in Directory.EnumerateFiles("testdata"))
            {
                var fileInfo = new FileInfo(file);
                if(fileInfo.Name.StartsWith("input"))
                {
                    var dependencyFile = FileHelper.ParseDependencyFile(file);

                    // These two steps could be combined into one to make everything faster
                    var packages = new PackageBuilder().BuildFromFile(dependencyFile);
                    var result = new Resolver().CanInstallAllPackages(packages);

                    File.WriteAllText($"testdata/output-for-{fileInfo.Name}", result ? "PASS" : "FAIL");
                    Console.WriteLine($"{fileInfo.Name} " + (result ? "PASS" : "FAIL"));
                }
            }
        }
    }
}
