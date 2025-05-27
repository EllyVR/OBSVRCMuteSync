using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace Phoenix.OBSVRCMuteSync;

class Program
{
    static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(new ExpressionTemplate(
                "[{@t:HH:mm:ss}][{SourceContext}][{@l}]: {@m}\n{@x}",
                theme: TemplateTheme.Literate))
            .CreateLogger();
        ConfigManager.Load();
        OBSWebsocketClient.Connect();
        OSCQServer.StartServer();
        Task.Delay(-1).GetAwaiter().GetResult();
    }
}