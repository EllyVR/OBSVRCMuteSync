# OBSVRCMuteSync

This is a quick standalone implementation of the VRC to OBS Mute Sync feature from Project Phoenix. 

# Quick Instructions

Start by downloading the binary from here https://github.com/EllyVR/OBSVRCMuteSync/releases/latest

From there put it in an empty folder and run it once It will auto close so you can fill in the config. 

In that folder open a file called Config.json in notepad and open obs too. 

Within obs go to tools → Websocket Server Settings. 

Make sure the plugin is enabled by checking the enable checkbox. 

Next hit Generate Password then hit show connect info. 

Copy the server password and paste it into the config. 

Next go to your audio mixer in obs and find the mic you want to be synced.

In my case it is Index Mic

So In the OBSMicName field i would enter Index Mic. 

Finally save that config file and the WebSocket server settings in obs. 

Lastly a note to have is make sure your starting it with your mic muted in vrchat and obs. This is due to some syncing issues with osc that i have to make an assumption.