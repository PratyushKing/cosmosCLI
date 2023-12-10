using System;

namespace cosmos {
    public static class RunAndBuild {
        public static CosmosProjectConfig? toBuildConfig;

        public static void run() {
            build();
        }

        public static void build() {
            
        }
    }

    public struct CosmosProjectConfig {
        public string csprojectPath;
        
        public CosmosProjectConfig(string csProjectPath) => csprojectPath = csProjectPath;
    }
}