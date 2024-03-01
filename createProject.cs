using System;
using System.Diagnostics;

namespace cosmos {
    public static class createProject {
        public static void createNewProject(string name) {
            RunBash("mkdir " + name + " && cd " + name + " &&" + "dotnet new cosmosCSKernel -n " + name);
            // Directory.CreateDirectory(name + "/.cosmosCLI");
            // var cname = name;
            // name = "Cosmos";
            // File.WriteAllText(cname + "/.cosmosCLI/VMWARE/" + name + ".vmx", vmxConfig.Replace("__PROJECT_NAME__", name).Replace("__ISO__", Directory.GetCurrentDirectory() + "/" + name + "/.cosmosCLI/VMWARE/" + name + ".iso"));
            // File.WriteAllText(cname + "/.cosmosCLI/VMWARE/" + name + ".nvram", File.ReadAllText("/etc/CosmosCLI/Cosmos.nvram"));
            // File.WriteAllText(cname + "/.cosmosCLI/VMWARE/Filesystem.vmdk", File.ReadAllText("/etc/CosmosCLI/Filesystem.vmdk"));
            File.WriteAllText(name + "/CosmosBuildFile", RUN.DefaultBuildFileContents.Replace("__PROJECT_NAME__", name));
            File.WriteAllText(name + "/Kernel.cs", File.ReadAllText(name + "/Kernel.cs").Replace("$safeprojectname$", name));
        }

        public static void RunBash(string command) {
            using var p = Process.Start("/bin/bash", "-c \"" + command + "\"");
            p.WaitForExit();
        }
    }
}