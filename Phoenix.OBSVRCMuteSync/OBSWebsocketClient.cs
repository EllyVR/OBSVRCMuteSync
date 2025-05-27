using System.Diagnostics;
using OBSWebSocket5;
using OBSWebSocket5.Events;
using Serilog;

namespace Phoenix.OBSVRCMuteSync;

public class OBSWebsocketClient
{
    public static OBSWebSocket OBSWebSocket { get; set; } = new OBSWebSocket();
    public static ILogger Logger { get; set; } = Serilog.Log.ForContext<OBSWebsocketClient>();
    
    public static void Connect()
    {
        while(Process.GetProcessesByName("obs64").Length == 0)
        {
            Logger.Information("Waiting for OBS to start...");
            Task.Delay(3000).GetAwaiter().GetResult();
        }
        Logger.Information("OBS Started, waiting 3 seconds to connect...");
        Task.Delay(3000).GetAwaiter().GetResult();
        OBSWebSocket.Connected += OBSWebSocketOnConnected;
        OBSWebSocket.Disposed += OBSWebSocketOnDisposed;
        OBSWebSocket.InputsEvents.InputMuteStateChanged += InputsEventsOnInputMuteStateChanged;
        OBSWebSocket.MessageDispatcher.Log += Log;
        OBSWebSocket.ConnectAsync(new Uri(ConfigManager.Config.OBSWebsocketURL), 
                new OBSWebSocketAuthPassword(ConfigManager.Config.ObsControlPassword))
            .GetAwaiter().GetResult();
    }

    private static void InputsEventsOnInputMuteStateChanged(object? sender, InputsEvents.InputMuteStateChangedEventArgs e)
    {
        if (e.InputName.Equals(ConfigManager.Config.OBSMicName))
        {
            if (e.InputMuted != OSCClient.isMuted)
            {
                OBSWebsocketClient.OBSWebSocket.InputsRequests.ToggleInputMuteAsync(ConfigManager.Config.OBSMicName);
                Logger.Information("Resynced Mic State");
            }
        }
    }

    private static void Log(string obj)
    {
        Logger.Information($"OBS: {obj}");
    }

    private static void OBSWebSocketOnDisposed(object? sender, EventArgs e)
    {
        Logger.Information("OBS Disposed, reconnecting...");
        Connect();
    }

    private static void OBSWebSocketOnConnected(object? sender, EventArgs e)
    {
        Logger.Information("Successfully Connected to obs!");
    }
}