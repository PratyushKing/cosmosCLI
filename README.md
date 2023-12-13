# CosmosCLI
CosmosCLI, the unofficial way to use cosmos on linux, conveniently.

## How to install
### Debian/Ubuntu or debian-based distributions
- Get the newest deb package from the Releases tab and download it.
- Use `sudo dpkg -i <deb_file>` to install it on your device :)
- Done! If everything installed correctly, just run cosmos and it should work. If not open a new issue in this github repo.

### RedHat/Fedora or RHEL-based distributions
- Get the latest rpm package from the Releases tab and download it.
- Use `sudo rpm -i <rpm_pkg>` to install it on your device.
- Done. Use `cosmos` in your terminal to access the tool, If it doesn't work, open a new issue in this repository.

### Arch or Arch-based distributions
- You can download the `.pkg.tar.zst` (the packaging format for Arch) file from the latest release in this repository.
- Then, to install just do `sudo pacman -U cosmos.pkg.tar.zst` to install it, and then it should work.
<br>(ARCH PACKAGE IS LEAST TESTED, PLEASE TEST IT AND SUBMIT YOUR REPORTS IF POSSIBLE AT DISCORD "pratyushking")

### Uncommon distributions (Gentoo, Slackware, etc.)
- These generally do have workarounds to get deb/rpm packages working, but if not, you can git clone the repository and then if dotnet's installed, just do ./prepare-executable.sh and it will automatically build and place the executable in the source directory, from there, you can copy executable to the bin folder and add it to your path.
- After proceeding to build, you can copy the final executable and put it in your environment path or alternatively paste it into your `bin` directory.
- Generally, because you are building it yourself, it is told to yourself clone the Cosmos repository and use the `make` command to build cosmos itself. The above method told, will only install the CLI which does have a cosmos install option but it's preferred to do manually.

## Post-Install
Generally, cosmos will not come with the package installer, instead you have to run `cosmos -ri` which means reinstall, that will not really reinstall your cosmos CLI but will "reinstall" cosmos itself, its named reinstall but it will do a fresh, clean install, the only requirements is having a stable-internet connection (becuase it takes a while) and having dotnet and nuget both installed, for which you can get through internet.

## How to use
This is how the help page looks by default!
![The default help page shown when the CLI is run.](https://github.com/PratyushKing/cosmosCLI/assets/83279568/dbf9ec3b-6a45-4e3e-99aa-b823095f352b)


### Options:
#### -r, --run, run
These are not fully completed yet and is there for testing purposes. The run parameters basically build the project and by either, using a cosmos project file [via cpf] or by using -ro parameters to know how to run the project.
The RO parameter is right now only having one option, which is for QEMU, as it is by far the easiest one to setup, all it does is use (if installed), the qemu-system-x86_64 command to launch qemu with specified iso and 512 megabytes of RAM followed by a default qemu hard disk image file. [PLEASE NOTE THAT THE RO PARAMETER HAS BEEN DISABLED FOR NOW, PLEASE USE RUN COMMAND IN YOUR BUILD FILE TO RUN BY UNCOMMENTING THE RUNCOMMAND PARAMETER AT THE END OF YOUR DEFAULT BUILD FILE]
If the RO parameter is not specified then it tries to get the cpf file contents to check how to run, which includes a command called `runCommand` followed by the bash command to run the project, if neither of those are found, it builds the ISO, and throws a warning message that no run methods were found.
#### -c, --create, create, new
These check if the cosmos project template are installed and if yes then it goes ahead and creates a new cosmos C# kernel in current directory and makes a pre-generated file called the `CosmosBuildFile` containing default pre-generated commands, specifying build options and other variables. To learn how to use it goto [create project help](docs/Create%20Project.md)

#### -cpf, --cosmosProjectFile
This command is what sources in the cosmos build file specified by the user, if this is not specified then the default is set to `CosmosBuildFile` from the source directory, which must be there, if not your project will fail to build, you can check cosmos build file's syntax to add to your existing projects by checking the [CPF Docs](./docs/CPF%20Docs.md)

#### -b, --build, build
These are the most important parameters and the ones that you will probably use the most, these will simply either use `-cpf` or fetch the default CosmosBuildFile in your source directory to build using dotnet and will create an ISO file for you to use and you can specify where to put it by specifying `buildLocation` in your CPF file. (Check out the [CPF docs](./docs/CPF%20Docs.md) for that.)

#### -ro [RUN OPTIONS]
Please refer the [run parameter options](#r---run-run) to learn about the -ro parameter.

#### -h/--help/help and -v/--version/version
The help parameter will obviously show the help page as shown in the image.
The version parameter can help you however in your `CosmosBuildFile`, check more on that in the [CPF Docs](./docs/CPF%20Docs.md).
