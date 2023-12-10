using System;

namespace cosmos {
    public static class CosmosProjectFile {

        public static void ReadCosmosProjectFile(string? fileText) {
            CosmosProjectConfig final = new();
            if (fileText == null) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("cosmos: Invalid File"); Console.ResetColor(); RUN.helpPage(); return; }
            var fLines = fileText.Split('\n');
            foreach (var line in fLines) {
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
                Console.WriteLine(variable + ": " + varVal);
                if (variable == "cosmosProjectFile") {
                    final.csprojectPath = varVal;
                } else if (variable == "cliBuildVersion") {
                    if (RUN.version != varVal) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("cosmos: The version of cosmosCLI installed on this machine (" + RUN.version + ") is not matching with what the project is made with/using (" + varVal + ")");
                        Console.ResetColor();
                        System.Environment.Exit(1);
                    }
                }
                Console.ResetColor();
            }
        }
    }
}