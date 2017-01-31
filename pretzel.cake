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
    }

    if(configuration == "Release")
    {
        Replace(file, localhost, url);
    }
}

Task("Bake")
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
  .IsDependentOn("Bake")
  .Does(() =>
{
});

RunTarget(target);