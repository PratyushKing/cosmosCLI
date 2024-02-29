using System;
using System.Diagnostics;

namespace cosmos {
    public static class createProject {
        public const string vmxConfig = ".encoding = \"windows-1252\"\nconfig.version = \"8\"\nvirtualHW.version = \"7\"\nmaxvcpus = \"4\"\nscsi0.present = \"TRUE\"\nmemsize = \"256\"\nide1:0.present = \"TRUE\"\nide1:0.autodetect = \"TRUE\"\nide1:0.deviceType = \"cdrom-image\"\npciBridge0.present = \"TRUE\"\npciBridge4.present = \"TRUE\"\npciBridge4.virtualDev = \"pcieRootPort\"\npciBridge4.functions = \"8\"\npciBridge5.present = \"TRUE\"\npciBridge5.virtualDev = \"pcieRootPort\"\npciBridge5.functions = \"8\"\npciBridge6.present = \"TRUE\"\npciBridge6.virtualDev = \"pcieRootPort\"\npciBridge6.functions = \"8\"\npciBridge7.present = \"TRUE\"\npciBridge7.virtualDev = \"pcieRootPort\"\npciBridge7.functions = \"8\"\nvmci0.present = \"TRUE\"\nroamingVM.exitBehavior = \"go\"\ndisplayName = \"__PROJECT_NAME__\"\nguestOS = \"other\"\nnvram = \"__PROJECT_NAME__.nvram\"\nvirtualHW.productCompatibility = \"hosted\"\nextendedConfigFile = \"__PROJECT_NAME__.vmxf\"\nide1:0.fileName = \"__ISO__\"\nuuid.location = \"56 4d f9 4f 97 56 1a 1b-44 e7 23 b2 db e7 91 6f\"\nuuid.bios = \"56 4d f9 4f 97 56 1a 1b-44 e7 23 b2 db e7 91 6f\"\ncleanShutdown = \"TRUE\"\nreplay.supported = \"FALSE\"\nreplay.filename = \"\"\npciBridge0.pciSlotNumber = \"17\"\npciBridge4.pciSlotNumber = \"21\"\npciBridge5.pciSlotNumber = \"22\"\npciBridge6.pciSlotNumber = \"23\"\npciBridge7.pciSlotNumber = \"24\"\nscsi0.pciSlotNumber = \"16\"\nvmci0.pciSlotNumber = \"32\"\nvmotion.checkpointFBSize = \"134217728\"\nvmci0.id = \"1821907405\"\nmonitor.allowLegacyCPU = \"TRUE\"\nserial0.present = \"TRUE\"\nserial0.yieldOnMsrRead = \"TRUE\"\nserial0.fileType = \"pipe\"\nserial0.fileName = \"\\\\.\\pipe\\Cosmos\\Serial\"\nserial0.pipe.endPoint = \"client\"\nserial0.tryNoRxLoss = \"TRUE\"\nsound.present = \"TRUE\"\nethernet0.present = \"TRUE\"\nethernet0.connectionType = \"nat\"\nethernet0.addressType = \"generated\"\nethernet0.generatedAddress = \"00:0c:29:e7:91:6f\"\nethernet0.generatedAddressOffset = \"0\"\nethernet0.pciSlotNumber = \"34\"\nusb.present = \"TRUE\"\nusb.generic.allowHID = \"TRUE\"\nehci.present = \"TRUE\"\nsound.fileName = \"-1\"\nsound.autodetect = \"TRUE\"\nide0:0.present = \"TRUE\"\nide0:0.fileName = \"Filesystem.vmdk\"\nfloppy0.present = \"FALSE\"\nvmxstats.filename = \"__PROJECT_NAME__.scoreboard\"\nnuma.autosize.cookie = \"10001\"\nnuma.autosize.vcpu.maxPerVirtualNode = \"1\"\nusb.pciSlotNumber = \"32\"\nsound.pciSlotNumber = \"33\"\nehci.pciSlotNumber = \"35\"\nide0:0.redo = \"\"\nsvga.vramSize = \"134217728\"\nmonitor.phys_bits_used = \"40\"\nsoftPowerOff = \"FALSE\"\ncheckpoint.vmState = \"__PROJECT_NAME__-2f444ca6.vmss\"";
        public const string nvramConfig = "<?xml version=\"1.0\"?>\n<Foundry>\n<VM>\n<VMId type=\"string\">52 56 c1 9f 9b d8 de 96-d3 9d 90 ec 57 db b0 37</VMId>\n<ClientMetaData>\n<clientMetaDataAttributes/>\n<HistoryEventList/></ClientMetaData>\n<vmxPathName type=\"string\">__PROJECT_NAME__.vmx</vmxPathName></VM></Foundry>";

        public static void createNewProject(string name) {
            RunBash("mkdir " + name + " && cd " + name + " &&" + "dotnet new cosmosCSKernel -n " + name);
            Directory.CreateDirectory(name + "/.cosmosCLI");
            Directory.CreateDirectory(name + "/.cosmosCLI/VMWARE/");
            var cname = name;
            name = "Cosmos";
            File.WriteAllText(cname + "/.cosmosCLI/VMWARE/" + name + ".vmx", vmxConfig.Replace("__PROJECT_NAME__", name).Replace("__ISO__", Directory.GetCurrentDirectory() + "/" + name + "/.cosmosCLI/VMWARE/" + name + ".iso"));
            File.WriteAllText(cname + "/.cosmosCLI/VMWARE/" + name + ".nvram", File.ReadAllText("/etc/CosmosCLI/Cosmos.nvram"));
            File.WriteAllText(cname + "/.cosmosCLI/VMWARE/Filesystem.vmdk", File.ReadAllText("/etc/CosmosCLI/Filesystem.vmdk"));
            File.WriteAllText(cname + "/CosmosBuildFile", RUN.DefaultBuildFileContents.Replace("__PROJECT_NAME__", cname));
            File.WriteAllText(cname + "/Kernel.cs", File.ReadAllText(name + "/Kernel.cs").Replace("$safeprojectname$", cname));
        }

        public static void RunBash(string command) {
            using var p = Process.Start("/bin/bash", "-c \"" + command + "\"");
            p.WaitForExit();
        }
    }
}