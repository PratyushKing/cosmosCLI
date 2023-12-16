exe: clean
	dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true
	cp bin/Release/net7.0/linux-x64/publish/cosmos .
	@echo "Executable sent to base source directory!"

build: cosmos.csproj
	dotnet build cosmos.csproj

run: cosmos.csproj build
	@./bin/Debug/net7.0/linux-x64/cosmos $(ARGS)

clean:
	rm -rf bin/
	rm cosmos