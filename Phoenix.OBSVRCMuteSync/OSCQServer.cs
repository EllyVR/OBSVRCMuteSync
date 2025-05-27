using System.Net;
using OscQueryLibrary;
using Serilog;

namespace Phoenix.OBSVRCMuteSync;

public class OSCQServer
{
    public static OscQueryServer LocalServer;
    public static ILogger Logger = Log.ForContext<OSCQServer>();
    public static void StartServer()
    {
        LocalServer = new OscQueryServer("Phoenix", IPAddress.Loopback);
        LocalServer.FoundVrcClient.SubscribeAsync(LocalServerFoundVRCClientAsync);
           
        LocalServer.Start();
        Logger.Information("OSCQuery Server started with service name Phoenix");
    }

    private static Task LocalServerFoundVRCClientAsync(IPEndPoint arg)
    {
        OSCClient.Initialize(arg.Address,arg.Port);
        return Task.CompletedTask;
    }
    
}