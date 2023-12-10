using System;

namespace cosmos {
    public static class CosmosProjectFile {

        public static void ReadCosmosProjectFile(string? fileText) {
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
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(variable + ": " + varVal);
                if (variable == "cosmosProjectFile") {
                    
                }
                Console.ResetColor();
            }
        }
    }
}