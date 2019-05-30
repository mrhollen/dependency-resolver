using System;
using System.Collections.Generic;
using Xunit;

namespace DependencyResolver.Tests
{
    public class ResolverTests
    {
        [Fact]
        public void CanTellIfPackageHasValidDependencies()
        {
            // Setup
            var resolver = new Resolver();
            var packages = new List<Package>();
            packages.Add(new Package("p1", "v1")
            {
                Dependencies = new List<Package>(){new Package("p2", "v1")}
            });
            packages.Add(new Package("p2", "v1")
            {
                Dependencies = new List<Package>(){new Package("p1", "v1")}
            });

            // Test
            var valid = resolver.CanInstallAllPackages(packages);
            
            // Assert
            Assert.True(valid);
        }
        
        [Fact]
        public void CanTellIfPackageHasInvalidDependencies()
        {
            // Setup
            var resolver = new Resolver();
            var packages = new List<Package>();
            packages.Add(new Package("p1", "v1")
            {
                Dependencies = new List<Package>(){new Package("p2", "v1")}
            });
            packages.Add(new Package("p2", "v2")
            {
                Dependencies = new List<Package>(){new Package("p1", "v2")}
            });

            // Test
            var valid = resolver.CanInstallAllPackages(packages);
            
            // Assert
            Assert.False(valid);
        }
        
        [Fact]
        public void CanDealWithValidCyclicDependencies()
        {
            // Setup
            var resolver = new Resolver();
            var packages = new List<Package>();
            packages.Add(new Package("p1", "v1")
            {
                Dependencies = new List<Package>(){new Package("p2", "v1")}
            });
            packages.Add(new Package("p2", "v1")
            {
                Dependencies = new List<Package>(){new Package("p1", "v1")}
            });

            // Test
            var valid = resolver.CanInstallAllPackages(packages);
            
            // Assert
            Assert.True(valid);
        }
        
        [Fact]
        public void CanDealMultilayeredValidDependencies()
        {
            // Setup
            var resolver = new Resolver();
            var packages = new List<Package>();
            packages.Add(new Package("p1", "v1")
            {
                Dependencies = new List<Package>(){new Package("p2", "v1")}
            });
            packages.Add(new Package("p2", "v1")
            {
                Dependencies = new List<Package>()
                {
                    new Package("p3", "v1")
                    {
                        Dependencies = new List<Package>(){ new Package("p1", "v1") }
                    }
                }
            });

            // Test
            var valid = resolver.CanInstallAllPackages(packages);
            
            // Assert
            Assert.True(valid);
        }
        
        [Fact]
        public void CanDealMultilayeredInvalidDependencies()
        {
            // Setup
            var resolver = new Resolver();
            var packages = new List<Package>();
            packages.Add(new Package("p1", "v1")
            {
                Dependencies = new List<Package>(){new Package("p2", "v1")}
            });
            packages.Add(new Package("p2", "v1")
            {
                Dependencies = new List<Package>()
                {
                    new Package("p3", "v1")
                    {
                        Dependencies = new List<Package>(){ new Package("p1", "v2") }
                    }
                }
            });

            // Test
            var valid = resolver.CanInstallAllPackages(packages);
            
            // Assert
            Assert.False(valid);
        }
        
        [Fact]
        public void CanTellDependenciesHaveValidDependencies()
        {
            // Setup
            var resolver = new Resolver();
            var packages = new List<Package>();
            packages.Add(new Package("p1", "v1")
            {
                Dependencies = new List<Package>(){
                    new Package("p2", "v1")
                    {
                        Dependencies = new List<Package>()
                        { 
                            new Package("p1", "v1"),
                            new Package("p3", "v1") 
                        }
                    }
                }
            });
            packages.Add(new Package("p2", "v1")
            {
                Dependencies = new List<Package>()
                {
                    new Package("p3", "v1")
                    {
                        Dependencies = new List<Package>()
                        { 
                            new Package("p1", "v1") 
                        }
                    }
                }
            });

            // Test
            var valid = resolver.CanInstallAllPackages(packages);
            
            // Assert
            Assert.True(valid);
        }
        
        [Fact]
        public void CanTellDependenciesHaveInvalidDependencies()
        {
            // Setup
            var resolver = new Resolver();
            var packages = new List<Package>();
            packages.Add(new Package("p1", "v1")
            {
                Dependencies = new List<Package>(){
                    new Package("p2", "v1")
                    {
                        Dependencies = new List<Package>()
                        { 
                            new Package("p1", "v1"),
                            new Package("p3", "v2") 
                        }
                    }
                }
            });
            packages.Add(new Package("p2", "v1")
            {
                Dependencies = new List<Package>()
                {
                    new Package("p3", "v1")
                    {
                        Dependencies = new List<Package>()
                        { 
                            new Package("p1", "v1") 
                        }
                    }
                }
            });

            // Test
            var valid = resolver.CanInstallAllPackages(packages);
            
            // Assert
            Assert.False(valid);
        }
    }
}
