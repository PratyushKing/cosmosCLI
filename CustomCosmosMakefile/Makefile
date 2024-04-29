THISDIR := Cosmos
DESTDIR ?= /opt/cosmos

IL2CPU_URL = https://github.com/CosmosOS/IL2CPU
XSHARP_URL = https://github.com/CosmosOS/XSharp
COMMON_URL = https://github.com/CosmosOS/Common

IL2CPU_BRANCH = master
XSHARP_BRANCH = master
COMMON_BRANCH = master

IL2CPU_DIR = ../IL2CPU
XSHARP_DIR = ../XSharp
COMMON_DIR = ../Common

GIT = git
DOTNET = dotnet

GITFLAGS = clone --depth=1
DOTNETFLAGS = -v:q -nologo

.PHONY: all
all:
	@$(MAKE) build
	@$(MAKE) publish
	@sudo $(MAKE) install
	@$(MAKE) nuget-install
	@echo "============================================"
	@echo "| Cosmos has been installed successfully!  |"
	@echo "============================================"

.PHONY: build
build:
	@echo "Building IL2CPU"
	@$(DOTNET) clean $(IL2CPU_DIR)/IL2CPU.sln
	@$(DOTNET) build $(IL2CPU_DIR)/IL2CPU.sln $(DOTNETFLAGS)
	@$(DOTNET) pack $(IL2CPU_DIR)/IL2CPU.sln $(DOTNETFLAGS)

	@echo "Building Cosmos"

	@cd ~/Cosmos\ Files/Cosmos
	@$(DOTNET) clean ./source/Cosmos.Common/Cosmos.Common.csproj
	@$(DOTNET) clean ./source/Cosmos.Debug.Kernel/Cosmos.Debug.Kernel.csproj
	@$(DOTNET) clean ./source/Cosmos.Debug.Kernel.Plugs.Asm/Cosmos.Debug.Kernel.Plugs.Asm.csproj
	@$(DOTNET) clean ./source/Cosmos.Core/Cosmos.Core.csproj
	@$(DOTNET) clean ./source/Cosmos.Core_Asm/Cosmos.Core_Asm.csproj
	@$(DOTNET) clean ./source/Cosmos.Core_Plugs/Cosmos.Core_Plugs.csproj
	@$(DOTNET) clean ./source/Cosmos.HAL2/Cosmos.HAL2.csproj
	@$(DOTNET) clean ./source/Cosmos.System2/Cosmos.System2.csproj
	@$(DOTNET) clean ./source/Cosmos.System2_Plugs/Cosmos.System2_Plugs.csproj
	@$(DOTNET) clean ./source/Cosmos.Build.Tasks/Cosmos.Build.Tasks.csproj
	@$(DOTNET) clean ./source/Cosmos.Plugs/Cosmos.Plugs.csproj


	@$(DOTNET) pack ./source/Cosmos.Common/Cosmos.Common.csproj $(DOTNETFLAGS)
	@$(DOTNET) pack ./source/Cosmos.Debug.Kernel/Cosmos.Debug.Kernel.csproj $(DOTNETFLAGS)
	@$(DOTNET) pack ./source/Cosmos.Debug.Kernel.Plugs.Asm/Cosmos.Debug.Kernel.Plugs.Asm.csproj $(DOTNETFLAGS)
	@$(DOTNET) pack ./source/Cosmos.Core/Cosmos.Core.csproj $(DOTNETFLAGS)
	@$(DOTNET) pack ./source/Cosmos.Core_Asm/Cosmos.Core_Asm.csproj $(DOTNETFLAGS)
	@$(DOTNET) pack ./source/Cosmos.Core_Plugs/Cosmos.Core_Plugs.csproj $(DOTNETFLAGS)
	@$(DOTNET) pack ./source/Cosmos.HAL2/Cosmos.HAL2.csproj $(DOTNETFLAGS)
	@$(DOTNET) pack ./source/Cosmos.System2/Cosmos.System2.csproj $(DOTNETFLAGS)
	@$(DOTNET) pack ./source/Cosmos.System2_Plugs/Cosmos.System2_Plugs.csproj $(DOTNETFLAGS)
	@$(DOTNET) pack ./source/Cosmos.Build.Tasks/Cosmos.Build.Tasks.csproj $(DOTNETFLAGS)
	@$(DOTNET) pack ./source/Cosmos.Plugs/Cosmos.Plugs.csproj $(DOTNETFLAGS)

	@echo "Building X#"
	@$(DOTNET) clean $(XSHARP_DIR)/source/XSharp/XSharp/XSharp.csproj
	@$(DOTNET) clean $(XSHARP_DIR)/source/Spruce

	$(DOTNET) pack $(XSHARP_DIR)/source/XSharp/XSharp/XSharp.csproj $(DOTNETFLAGS)
	$(DOTNET) pack $(XSHARP_DIR)/source/Spruce/Spruce.csproj $(DOTNETFLAGS)


.PHONY: publish
publish:
	@echo "Publishing IL2CPU"
	$(DOTNET) publish $(IL2CPU_DIR)/source/IL2CPU/IL2CPU.csproj -r linux-x64 --self-contained $(DOTNETFLAGS)

	@echo "Publishing Cosmos"
	@$(DOTNET) publish ./source/Cosmos.Core_Plugs/Cosmos.Core_Plugs.csproj $(DOTNETFLAGS)
	@$(DOTNET) publish ./source/Cosmos.Debug.Kernel.Plugs.Asm/Cosmos.Debug.Kernel.Plugs.Asm.csproj $(DOTNETFLAGS)
	@$(DOTNET) publish ./source/Cosmos.HAL2/Cosmos.HAL2.csproj $(DOTNETFLAGS)
	@$(DOTNET) publish ./source/Cosmos.System2_Plugs/Cosmos.System2_Plugs.csproj $(DOTNETFLAGS)
	@$(DOTNET) publish ./source/Cosmos.Plugs/Cosmos.Plugs.csproj $(DOTNETFLAGS)

	@echo "Publishing X#"
	@$(DOTNET) publish $(XSHARP_DIR)/source/XSharp/XSharp/XSharp.csproj $(DOTNETFLAGS)
	@$(DOTNET) publish $(XSHARP_DIR)/source/Spruce/Spruce.csproj $(DOTNETFLAGS)

.PHONY: install
install:
	@echo "Installing to" $(DESTDIR)
	@mkdir -p $(DESTDIR)/Cosmos
	@mkdir -p $(DESTDIR)/XSharp/DebugStub
	@mkdir -p $(DESTDIR)/Build/ISO
	@mkdir -p $(DESTDIR)/Build/IL2CPU
	@mkdir -p $(DESTDIR)/Build/HyperV
	@mkdir -p $(DESTDIR)/Build/VMware/Workstation
	@mkdir -p $(DESTDIR)/Packages
	@mkdir -p $(DESTDIR)/Kernel
	@cp -r $(IL2CPU_DIR)/artifacts/Debug/nupkg/*.nupkg $(DESTDIR)/Packages/
	@cp -r ./artifacts/Debug/nupkg/*.nupkg $(DESTDIR)/Packages/
	@cp -r $(XSHARP_DIR)/artifacts/Debug/nupkg/*.nupkg $(DESTDIR)/Packages/
	@cp -r $(IL2CPU_DIR)/source/Cosmos.Core.DebugStub/*.xs $(DESTDIR)/XSharp/DebugStub/

	@cp -r ./Artwork/XSharp/XSharp.ico $(DESTDIR)/XSharp/
	@cp -r ./Artwork/Cosmos.ico $(DESTDIR)/

	@cp -r $(IL2CPU_DIR)/source/IL2CPU/bin/Debug/*/linux-x64/publish/* $(DESTDIR)/Build/IL2CPU/
	@cp -r ./source/Cosmos.Core_Plugs/bin/Debug/*/publish/*.dll $(DESTDIR)/Kernel/
	@cp -r ./source/Cosmos.System2_Plugs/bin/Debug/*/publish/*.dll $(DESTDIR)/Kernel/
	@cp -r ./source/Cosmos.HAL2/bin/Debug/*/publish/*.dll $(DESTDIR)/Kernel/
	@cp -r ./source/Cosmos.Debug.Kernel.Plugs.Asm/bin/Debug/netstandard2.0/publish/*.dll $(DESTDIR)/Kernel/

	@cp -r ./Build/VMWare/Workstation/* $(DESTDIR)/Build/VMware/Workstation/
	@cp -r ./Build/syslinux/* $(DESTDIR)/Build/ISO/
	@echo $(DESTDIR) > /etc/CosmosUserKit.cfg

.PHONY: nuget-install
nuget-install:
	@echo "Installing Nuget packages"

	@rm -r -f ~/.nuget/packages/cosmos.*/
	@rm -r -f ~/.nuget/packages/il2cpu.*/

	@$(DOTNET) nuget remove source "Cosmos Local Package Feed" || true
	@$(DOTNET) nuget add source $(DESTDIR)/Packages/ -n "Cosmos Local Package Feed"
