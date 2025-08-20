Mono
https://www.mono-project.com/download/stable/#download-mac

https://www.nuget.org/packages/csharp-ls

Remove mono
sudo rm -rf /Library/Frameworks/Mono.framework
sudo pkgutil --forget com.xamarin.mono-MDK.pkg
sudo rm /etc/paths.d/mono-commands

If you want to use a custom theme, create a file with the theme's name (e.g., `mytheme.toml`) and place it in the `~/.config/helix/themes` directory. After placing the file, you can load it using the `:theme` command or by setting it in your `config.toml` file.