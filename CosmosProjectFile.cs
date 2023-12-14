using System;

namespace cosmos {
    public static class CosmosProjectFile {

        public static void ReadCosmosProjectFile(string? fileText) {
            CosmosProjectConfig final = new();
            if (fileText == null) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("cosmos: Invalid File"); Console.ResetColor(); RUN.helpPage(); return; }
            var fLines = fileText.Split('\n');
            var i = 0;
            foreach (var line in fLines) {
                if (i == 0) { i = 1; continue; }
                if (line == "") { continue; }
                var variable = "";
                var varVal = "";
                var cWord = "";
                var postVariable = false;
                foreach (var c in line) {
                    if (c == ' ' && !postVariable) { continue; }
                    else if (c == '=') { variable = cWord; cWord = ""; postVariable = true; }
                    else { cWord += c; }
                }
                varVal = cWord.TrimStart(' ');
                Console.ForegroundColor = ConsoleColor.Black;
                if (!variable.StartsWith("#")) {
                    Console.WriteLine(variable + ": " + varVal);
                }
                if (variable == "cosmosProjectFile") {
                    final.csprojectPath = varVal;
                } else if (variable == "cliBuildVersion") {
                    if (RUN.version != varVal) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("cosmos: The version of cosmosCLI installed on this machine (" + RUN.version + ") is not matching with what the project is made with/using (" + varVal + ")");
                        Console.ResetColor();
                        Console.WriteLine("Do you still want to proceed? (Enter to continue, else exit)");
                        var key = Console.ReadKey();
                        if (!(key.Key == ConsoleKey.Enter)) { System.Environment.Exit(1); }
                    }
                    final.versionMatches = true;
                } else if (variable == "buildLocation") {
                    final.isoPath = varVal;
                } else if (variable == "buildISOName") {
                    final.isoName = varVal;
                } else if (variable == "runCommand") {
                    final.runCommand = varVal;
                } else if (variable.StartsWith("#")) {}
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("cosmos: Your CosmosBuildFile contains errors and has unsupported variables, please recheck your file!");
                    Console.ResetColor();
                    System.Environment.Exit(1);
                }
                Console.ResetColor();
            }
            RUN.current = final;
            return;
        }
    }
}