using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace Phoenix.OBSVRCMuteSync;

class Program
{
    public const string Version = "1.0.0";
    static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(new ExpressionTemplate(
                "[{@t:HH:mm:ss}][{SourceContext}][{@l}]: {@m}\n{@x}",
                theme: TemplateTheme.Literate))
            .CreateLogger();
        Log.Information($"Phoenix.OBSVRCMuteSync version: {Version} Started.");
        ConfigManager.Load();
        OBSWebsocketClient.Connect();
        OSCQServer.StartServer();
        Task.Delay(-1).GetAwaiter().GetResult();
    }
}