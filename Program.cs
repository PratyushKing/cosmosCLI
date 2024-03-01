using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace cosmos {
    public static class RUN {

        public const string version = "v1.3";
        public static string buildFile = "CosmosBuildFile";
        public static CosmosProjectConfig current = new();
        public static bool success = true;
        public static bool verbose = false;
        
        public const string DefaultBuildFileContents = "[__PROJECT_NAME__]\ncosmosProjectFile = __PROJECT_NAME__.csproj\ncliBuildVersion = " + version + "\nbuildLocation = ISO/\nbuildISOName = __PROJECT_NAME__\nrunProfile = VMWare";

        public static void Main(string[] args) {
            current.runCommand = "qemu-system-x86_64 -cdrom __ISO__ -m 512M";
            if (args.Length <= 0) {
                Console.WriteLine("No commands specified.");
                helpPage();
                return;
            }
            if (args[0] == "--setup") {
                Console.WriteLine("Setting up!");
                if (isRoot()) {
                    RunAndBuild.RunCommandWithBash("/usr/bin/echo", "Adding permissions for $(logname).");
                    RunAndBuild.RunCommandWithBash("/usr/bin/chown", "$(logname):$(logname) /etc/CosmosCLI/");
                    RunAndBuild.RunCommandWithBash("/usr/bin/chown", "$(logname):$(logname) /etc/CosmosCLI/skel");
                    RunAndBuild.RunCommandWithBash("/usr/bin/chown", "$(logname):$(logname) /etc/CosmosCLI/VMWARE/");
                    RunAndBuild.RunCommandWithBash("/usr/bin/chown", "$(logname):$(logname) /etc/CosmosCLI/VMWARE/Cosmos.nvram");
                    RunAndBuild.RunCommandWithBash("/usr/bin/chown", "$(logname):$(logname) /etc/CosmosCLI/VMWARE/Cosmos.vmx");
                    RunAndBuild.RunCommandWithBash("/usr/bin/chown", "$(logname):$(logname) /etc/CosmosCLI/VMWARE/currentiso.iso");
                    RunAndBuild.RunCommandWithBash("/usr/bin/chown", "$(logname):$(logname) /etc/CosmosCLI/VMWARE/Filesystem.vmdk");
                    File.Create("/etc/CosmosCLI/instdone");
                    Console.WriteLine("FINISHED!");
                    return;
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: `cosmos --setup` must run as root. Please use `sudo cosmos --setup`.\n(PLEASE AVOID USING ROOT USER ITSELF)");
                    Console.ResetColor();
                    Environment.Exit(-1);
                }
            }
            if (!File.Exists("/etc/CosmosCLI/instdone")) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please use `cosmos --setup` after install is finished.");
                Console.ResetColor();
                return;
            }
            args = args.Append<string>(" ").ToArray<string>();
            for (var i = 0; i < args.Length - 1; i++) {
                var cArg = args[i];
                var nArg = "BLANK";
                if (args.Length > i + 1) {
                    nArg = args[i + 1];
                }
                switch (cArg.ToLower()) {
                    case "-cpf":
                    case "-cosmosprojectfile":
                        CosmosProjectFile.ReadCosmosProjectFile(SafeRead(buildFile));
                        buildFile = nArg;
                        break;
                    case "-r":
                    case "--run":
                    case "run":
                        CosmosProjectFile.ReadCosmosProjectFile(SafeRead(buildFile));
                        RunAndBuild.run();
                        goto quit;
                    case "-b":
                    case "--build":
                    case "build":
                        CosmosProjectFile.ReadCosmosProjectFile(SafeRead(buildFile));
                        RunAndBuild.build();
                        goto quit;
                    case "-h":
                    case "--help":
                    case "help":
                        helpPage();
                        return;
                    case "-v":
                    case "--version":
                    case "version":
                        Console.WriteLine("CosmosCLI " + version);
                        return;
                    case "--verbose":
                        verbose = true;
                        continue;
                    case "-c":
                    case "--create":
                    case "create":
                    case "new":
                        Console.WriteLine("Creating Cosmos C# Kernel.");
                        if (nArg == "") {
                            success = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No name specified,\nUsage: cosmos " + cArg + " <yourProjectName>");
                            goto quit;
                        }
                        createProject.createNewProject(nArg);
                        return;
                    case "-ro":
                        if (nArg == " ") {
                            success = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No option specified (example: -q/--qemu)");
                            goto quit;
                        }
                        if (nArg == "-q" || nArg == "--qemu") {
                            current.runCommand = "qemu-system-x86_64 -cdrom __ISO__ -m 512M";
                        }
                        continue;
                    case "-ri":
                        Console.WriteLine("WARNING: Please make sure that you have git, make, xorriso, yasm, dotnet and nuget properly installed before proceeding, or it will fail.\n Press Enter to continue.");
                        Console.ReadLine();
                        var p = Process.Start("/bin/bash", "-c \"cd ~ && mkdir CosmosFiles && cd ~/CosmosFiles && git clone https://github.com/CosmosOS/Cosmos && cd ~/CosmosFiles/Cosmos && make && echo FINISHED\"");
                        p.WaitForExit();
                        return;
                    default:
                        success = false;
                        goto quit;
                }
            }
            quit:
            if (!success || args.Length < 1)
                helpPage();
        }

        public static void helpPage() {
            Console.WriteLine("cosmos: The unofficial Cosmos tool for Linux! Please read the github for detailed info [https://github.com/PratyushKing/cosmosCLI].");
            Console.WriteLine("Usage:\n  cosmos [OPTIONS]\n");
            Console.WriteLine("Options:\n   -r, --run, run                          Run a project according to -cpf and -ro parameters.\n" +
                                        "   -c, --create, create, new               Create new C# Cosmos Kernel.\n" +
                                        "   -cpf, --cosmosProjectFile               This is a optional parameter for run and build, if not specified then\n" + 
                                        "                                           it specifies a custom cosmos CLI build file. [PRE-GENERATED WITH --create]\n" +
                                        "   -b, --build, build                      Build a project according to -cpf parameter.\n" +
                                        "   -ro, --runOptions                       This is a optional parameter for run to specify how to run a specific project. [EXPERIMENTAL]\n" +
                                        "   -h, --help, help                        To show this help page.\n" +
                                        "   -v, --version, version                  Version of CosmosCLI.\n" +
                                        "   -ri, --reinstall                        Fetches cosmos to your user folder, and installs it for you! [EXPERIMENTAL]\n" +
                                        "   --setup                                 It is a must-required command that must be used at post installation time.\n" +
                                        "\n" +
                              "Run Options is now in the CosmosBuildFile and add a `run profile` parameter to make it work. For more info, read the docs on the github page." +
                              "\n");
        }

        public static string? SafeRead(string path) {
            if (File.Exists(path)) {
                return File.ReadAllText(path);
            } else { return null; }
        }

        public static void PrintArray(string[] arr) {
            if (arr.Length - 1 < 0) { return; }
            var i = 0;
            foreach (var str in arr) {
                if (i > arr.Length - 2) {
                    break;
                }
                Console.Write(str + ", ");
                i++;
            }
            Console.WriteLine(arr[arr.Length - 1] + ".");
        }

        [DllImport("libc")] public static extern uint getuid();
        
        public static bool isRoot() {
            return getuid() == 0;
        }
    }

}