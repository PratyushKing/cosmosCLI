using System;
using System.Diagnostics;

namespace cosmos {
    public static class RunAndBuild {

        public static void run() {
            build();
            RUN.current.runCommand = RUN.current.runCommand.Replace("__ISO__", RUN.current.isoPath + RUN.current.csprojectPath.Replace(".csproj", ".iso"));
            RunCommandWithBash(RUN.current.runCommand.Split(' ')[0], RUN.current.runCommand.TrimStart(RUN.current.runCommand.Split(' ')[0].ToCharArray()));
        }

        public static void build() {
            RunCommandWithBash("dotnet", "build " + RUN.current.csprojectPath);
            RunCommandWithBash("mkdir", "-p " + RUN.current.isoPath);
            RunCommandWithBash("cp", "bin/cosmos/Debug/net6.0/" + RUN.current.csprojectPath.Replace(".csproj", ".iso") + " " + RUN.current.isoPath);
        }

        public static void RunCommandWithBash(string command, string args)
        {
            ProcessStartInfo psi = new()
            {
                FileName = "/bin/" + command,
                Arguments = args,
                UseShellExecute = false
            };

            using var p = Process.Start(psi);
            p.WaitForExit();
        }
    }

    public struct CosmosProjectConfig {
        public string csprojectPath;
        public bool versionMatches;
        public string isoPath;
        public string isoName;
        public string runCommand;

        public CosmosProjectConfig(string csProjectPath, bool versionMatching, string isoP, string isoname, string rCommand) { runCommand = rCommand; isoName = isoname; isoPath = isoP; csprojectPath = csProjectPath; versionMatches = versionMatching; }
    }
}