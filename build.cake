var target = Argument("target", "Default");

Task("Bake")
  .Does(() =>
{
    var settings = new ProcessSettings{
        Arguments = new ProcessArgumentBuilder().Append("bake")
    };
    StartProcess("C:\\tools\\Pretzel\\Pretzel.exe", settings);
});

Task("Taste")
  .Does(() =>
{
    var settings = new ProcessSettings{
        Arguments = new ProcessArgumentBuilder().Append("taste").Append("--port 5001")
    };
    StartProcess("C:\\tools\\Pretzel\\Pretzel.exe", settings);
});

Task("Default")
  .IsDependentOn("Bake")
  .Does(() =>
{
});

RunTarget(target);