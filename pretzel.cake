#addin "Cake.Npm"
#addin "Cake.Powershell"

var target = Argument("target", "Default");
var configuration = Argument("Configuration", "Release");
var port = Argument("Port", "5001");

public static void Replace(string file, string @this, string withThis)
{
    var config = System.IO.File.ReadAllText(file, Encoding.UTF8);
    config = config.Replace(@this, withThis);
    System.IO.File.WriteAllText(file, config, Encoding.UTF8);
}

public static void Prepare(ICakeContext context, string configuration = null)
{
    configuration = configuration ?? context.Argument<string>("Configuration");
    var url = context.Argument<string>("Url", "http://devonburriss.me/");
    var port = context.Argument<string>("Port", "5001");
    var file = "./_config.yml";
    var localhost = "http://localhost:" + port + "/";    

    if(configuration == "Debug")
    {
        Replace(file, url, localhost);
        Replace(file, "is_production: true", "is_production: false");
    }

    if(configuration == "Release")
    {
        Replace(file, localhost, url);
        Replace(file, "is_production: false", "is_production: true");
    }
}

Task("Install-LessC")
    .Does(() =>
{
    Npm.Install(settings => settings.Package("less").Globally());
    Npm.Install(settings => settings.Package("less-plugin-clean-css").Globally());
});

Task("Less-Compile")
    .Does(() =>
{
    StartPowershellScript("Invoke-Expression", new PowershellSettings()
        .SetFormatOutput()
        .SetLogOutput()
        .WithArguments(args =>
        {
            args.AppendQuoted("lessc --clean-css ./less/clean-blog.less ./css/clean-blog.css");
        }));
    // var settings = new ProcessSettings
    // {
    //     Arguments = new ProcessArgumentBuilder().Append("--clean-css").Append("../less/clean-blog.less").Append("../css/clean-blog.css")
    // };
    // StartProcess("lessc", settings);
    //Npm.FromPath(".").RunScript("lessc --clean-css ./less/clean-blog.less ./css/clean-blog.css");
});

Task("Bake")
  .IsDependentOn("Less-Compile")
  .Does(() =>
{    
    Prepare(Context);
    var settings = new ProcessSettings
    {
        Arguments = new ProcessArgumentBuilder().Append("bake")
    };
    StartProcess("C:\\tools\\Pretzel\\Pretzel.exe", settings);
});

Task("Taste")
  .Does(() =>
{
    Prepare(Context, "Debug");
    var settings = new ProcessSettings{
        Arguments = new ProcessArgumentBuilder().Append("taste").Append("--port " + port)
    };
    StartProcess("C:\\tools\\Pretzel\\Pretzel.exe", settings);
});

Task("Default")
  .IsDependentOn("Install-LessC")
  .IsDependentOn("Bake")
  .Does(() =>
{
});

RunTarget(target);