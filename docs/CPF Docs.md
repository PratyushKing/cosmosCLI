[Return back to repository](../README.md)
# CPF (CosmosProjectFile)
This doc file helps in understanding the CosmosBuildFile and how it works with CosmosCLI.
Let's see what options we have to work with.

## CPF Options:

- Header:<br>
Structurally, this must be your first line in the `CosmosBuildFile`, it contains the project name between the square brackets (`[]`). Generally being like `[WonderfulCosmosOS]`.

(Please note, everything from this point on is case sensitive)

- CosmosProjectFile:<br>
This must not be confused with the file `CosmosBuildFile`, this variable instead deals with your .csproj file to use while building, incase you have multiple projects, by default it's set to the default `.csproj` file that dotnet/cosmosCLI creates for you, denoted via `cosmosProjectFile`.

- CLIBuildVersion:<br>
The `cliBuildVersion` variable is what was mentioned in the README file, you can check your current CosmosCLI version via `cosmos -v` and this must match with the project's `cliBuildVersion`, the CLI will only build if the version of the build file matches with your CLI's version.

- BuildLocation:<br>
This is another crucial variable that is mandatory to be used in a build file, this specifies where to put the ISO file post-building, generally it will build to the `bin/Debug/......` directory, but the build location parameter will be used to then move the ISO from default directory back to the `buildLocation`. Denoted via `buildLocation`.

- BuildISOName:<br>
This is optional mainly, it will like the name suggests, name the ISO according to what you want after copying the ISO to `buildLocation`, this ISO name will by default be the project name so while mentioning it is mandatory, you can ofcourse specify as `__PROJECT_NAME__` and that being a pre-defined variable inside the code, will be replaced with your project name.

- RunCommand:<br>
This is an experimental feature, mainly used with the run parameter, this removes the need of run options as specified in the main README, you may use this to specify your run command, rather than using preset values with the run options, one use case can be if you want to use other architecture versions of QEMU, than the default x86_64 thats there with the run options, you can use specify that in run command, by default the run command in all new projects is commented out (like in bash with the `#` symbol) because it's experimental and not really there yet, so it's all coming soon. As of right now, its entirely for QEMU or other emulators and is no longer the default option like it used to be.

That covers all the default options, if you want to see how a default build file is put together, it looks something similiar to this:
```
[__PROJECT_NAME__]
cosmosProjectFile = cosmos.csproj
cliBuildVersion = alpha 0.1
buildLocation = ISO/
buildISOName = __PROJECT_NAME__
# runCommand = qemu-system-x86_64 -cdrom __ISO__ -m 512M
```
Please note, that the `runCommand` can use the ISO parameter to get to ISO path and ISO, both.<br>
This is what will be created with your new project via the create parameter in the CLI, difference being that the project name on the top instead being change to the name specified in the create parameter.

[Return back to repository](../README.md)