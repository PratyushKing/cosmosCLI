using System;

namespace cosmos {
    public static class RunAndBuild {
        public static CosmosProjectConfig? toBuildConfig;

        public static void run() {
            checkVersion();
            build();
        }

        public static void build() {
            checkVersion();
        }

        public static void checkVersion() {
            
        }
    }

    public struct CosmosProjectConfig {
        public string csprojectPath;
        
        public CosmosProjectConfig(string csProjectPath) => csprojectPath = csProjectPath;
    }
}