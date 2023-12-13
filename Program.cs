using System;
using System.Reflection.Metadata.Ecma335;

namespace cosmos {
    public static class RUN {

        public const string version = "alpha 0.1";
        public static string buildFile = "CosmosBuildFile";
        public static CosmosProjectConfig current = new();
        public static bool success = true;
        
        public const string DefaultBuildFileContents = "[__PROJECT_NAME__]\r\ncosmosProjectFile = cosmos.csproj\r\ncliBuildVersion = alpha 0.1\r\nbuildLocation = ISO/\r\nbuildISOName = __PROJECT_NAME__\r\nrunCommand = qemu-system-x86_64 -cdrom __ISO__ -m 512M";

        public static void Main(string[] args) {
            current.runCommand = "qemu-system-x86_64 -cdrom __ISO__ -m 512M";
            if (args.Length <= 0) {
                Console.WriteLine("No commands specified.");
                helpPage();
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
                                        "\n" +
                              "Run Options:\n" +
                              "   cosmos -r -cpf [BUILD_FILE] -ro [RUN_OPTIONS] (-ro is optional if the CLI build file includes run method)\n" +
                                        "   -ro Options:\n" +
                                        "      -q, --qemu                           Runs project with Qemu x86_64 with a default hda file.\n" +
                                        "    [MORE OPTIONS COMING SOON]\n" +
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
    }
}