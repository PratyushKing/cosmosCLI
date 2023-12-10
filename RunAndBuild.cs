using System;

namespace cosmos {
    public static class RunAndBuild {
        public static CosmosProjectConfig? toBuildConfig;

        public static void run(string runMethod) {
            
        }
    }

    public struct CosmosProjectConfig {
        public string csprojectPath;
        
        public CosmosProjectConfig(string csProjectPath) => csprojectPath = csProjectPath;
    }
}