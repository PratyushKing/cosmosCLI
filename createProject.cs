using System;
using System.Diagnostics;

namespace cosmos {
    public static class createProject {

        public static void createNewProject(string name) {
            RunBash("mkdir " + name + " && cd " + name + " &&" + "dotnet new cosmosCSKernel -n " + name);
            File.WriteAllText(name + "/CosmosBuildFile", RUN.DefaultBuildFileContents.Replace("__PROJECT_NAME__", name));
            File.WriteAllText(name + "/Kernel.cs", File.ReadAllText(name + "/Kernel.cs").Replace("$safeprojectname$", name));
        }

        public static void RunBash(string command) {
            using var p = Process.Start("/bin/bash", "-c \"" + command + "\"");
            p.WaitForExit();
        }
    }
}