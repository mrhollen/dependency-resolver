using System;

namespace DependencyResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var dependencyFile = FileHelper.ParseDependencyFile("testdata/input005.txt");
            var packages = new PackageBuilder().BuildFromFile(dependencyFile);
            var result = new Resolver().CanInstallAllPackages(packages);

            Console.WriteLine(result ? "PASS" : "FAIL");
        }
    }
}
