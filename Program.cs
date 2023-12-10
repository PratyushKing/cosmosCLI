using System;
using System.Reflection.Metadata.Ecma335;

namespace cosmos {
    public static class RUN {

        public const string version = "alpha 0.1";

        public static void Main(string[] args) {
            for (var i = 0; i < args.Length - 1; i++) {
                var cArg = args[i];
                var nArg = args[i + 1];
                
                switch (cArg.ToLower()) {
                    case "-cpf":
                    case "-cosmosprojectfile":
                        CosmosProjectFile.ReadCosmosProjectFile(SafeRead(nArg));
                        break;
                    default:
                        helpPage();
                        return;
                }
            }
            helpPage();
        }

        public static void helpPage() {
            Console.WriteLine("cosmos: The unofficial Cosmos tool for Linux! Please read the github for detailed info [].");
            Console.WriteLine("Usage:\n  cosmos [OPTIONS]\n");
            Console.WriteLine("Options:\n   -r, --run, run                          Run a project according to -cpf and -ro parameters.\n" +
                                        "   -c, --create, create, new               Create new C# Cosmos Kernel.\n" +
                                        "   -cpf, --cosmosProjectFile               This is a optional parameter for run and build, if not specified then\n" + 
                                        "                                           it specifies a custom cosmos CLI build file. [PRE-GENERATED WITH --create]\n" +
                                        "   -b, --build, build                      Build a project according to -cpf parameter.\n" +
                                        "   -ro, --runOptions                       This is a optional parameter for run to specify how to run a specific project. [EXPERIMENTAL]\n" +
                                        "   -h, --help                              To show this help page.\n" +
                                        "   -v, --version                           Version of CosmosCLI.\n" +
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