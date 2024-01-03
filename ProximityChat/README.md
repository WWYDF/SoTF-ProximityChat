# Mumble Link in Sons of The Forest
*Spoiler Alert: It's hella scuffed.*


I'm not very good at programming, so there are very clear hiccups here and there. (Please submit bug reports in issues!)

## Table of Contents:
- [Installation Guide](https://github.com/WWYDF/SoTF-ProximityChat#installation-guide)
- [Mumble Client Setup](https://github.com/WWYDF/SoTF-ProximityChat#mumble-client-setup)
  - [Recommended Settings](https://github.com/WWYDF/SoTF-ProximityChat#recommended-settings)
- [Mumble Server Setup](https://github.com/WWYDF/SoTF-ProximityChat#mumble-server-setup)
- [Known Issues](https://github.com/WWYDF/SoTF-ProximityChat#known-issues)
- [Thunderstore](https://github.com/WWYDF/SoTF-ProximityChat#why-isnt-it-on-thunderstore)



## Installation Guide
Might require some tinkering if you have extra mods. This process will become a LOT smoother once I release the mod on Thunderstore. For now, you can [download a release here](https://github.com/WWYDF/SoTF-ProximityChat/releases).


1. Install **r2modmanplus**. It also works with Thunderstore Mod Manager, but I find r2modmanplus to be better since it doesn't rely on Overwolf. You can download it here: https://github.com/ebkr/r2modmanPlus/releases
2. Select **Sons of The Forest** as your Game and Create a Profile.
3. Click **Online**, and install "**BepInExPack_IL2CPP**". It should be right at the top.
4. Click **START VANILLA** at the top left. *Wait a bit for the game to start, then close it.
5. Back on r2modman, click on **Settings**, then **Browse Profile Folder**. Navigate to `<Profile>\BepInEx\plugins\`.
6. Make a new folder called **ProximityChat**, and move the released `.dll` in this folder.
7. Your file structure should now look something like this: `\SonsOfTheForest\profiles\ProximityChat\BepInEx\plugins\ProximityChat\ProximityChat.dll`
8. Go back to r2modmanplus. You won't see the mod on the list, but its there.
9. Click **START MODDED** at the top left.

> *Depending on the speed of your computer, the first launch could take a while. Just let it run as BepInEx needs to run first time setup. You can even watch it progress in the console window.

---

## Mumble Client Setup
This guide assumes you already have a server, if you don't, you can look below for that guide.

1. Install Mumble Client You can get the latest here: https://www.mumble.info/downloads/
2. Once installed, open Mumble. Go through all the audio setup, or skip if you're good at this.
3. Click "Configure", then "Settings".
4. Click "Audio Output" tab, then make sure "Positional Audio" is Enabled and Headphones is checked. **For SoTF, I recommend the settings below.**
5. Go to the Plugins tab on the left. Make sure "Link to Game and Transmit Position" is enabled. (At the top)
6. Scroll down on the list and make sure every box next to "Link" is checked. (Enable, PA, & KeyEvents)
7. Click "OK" to Save and Close the window.
8. Connect to a Server.

> When you're connected to a Server and are CURRENTLY IN GAME, it should say "Link (SoTF) Linked" in Mumble Chat.


### Recommended Settings
- Minimum Distance: 6.0m
- Maximum Distance: 65.0m
- Minimum Volume: 0%
- Bloom: 35%

### Disclaimers & Volume Fixes
1. Everyone needs to have done these steps, including the mod install in order for this to work. Anyone who hasn't will just be full, normal audio. (Like talking on Discord.)

2. If someone's mic is bad or farther away (irl), you can right click on their name in Mumble and click "Local Volume Adjustment" to change their Gain specifically.

3. If you want to change the distance you can hear people, change "Maximum Distance". I recommend leaving the others alone.

---

## Mumble Server Setup

### For Standard Users
*i.e. 99% of people*


#### You have four options:
1. Check this guide to host your own Mumble Server. https://wiki.mumble.info/wiki/Murmurguide
2. Find a free-to-use server on the Internet Browser in Mumble Client.
3. Buy your own Mumble Server from a Hosting Company.
4. Join my Personal Mumble Server for these projects. <3
> IP: mumble.devante.net


### For Developers
If you are using the Mumble Position Debugger by jrobsonchase, (https://gitlab.com/jrobsonchase/mumble-position-debug), and you need gRPC, here's how. This guide is based on Windows.

1. Download Murmur 1.4.230. You can get that here: https://github.com/mumble-voip/mumble/releases/tag/v1.4.230
2. I recommend the mumble_server-1.4.230.x64.msi. (Microsoft Installer).
3. Once installed, Open Run. (CTRL + R).
4. Type in `%localappdata%` and press enter.
5. Create a new folder and call it `Mumble`.
6. Open the new folder and create a new one in that folder called `Murmur`.
7. Create a new file called `murmur.ini`. Open it in Notepad.
8. Add the following line: `grpc="127.0.0.1:50051"`
9. Open Start, and type in `Mumble Server` to start.
10. Murmur will start in a tray icon. Expand your tray and right click the Murmur Icon.
11. Click Show Log
12. You shouldn't see any "gRPC Disabled" errors. Open Mumble Client and connect.
13. Download the Position Debugger here: https://gitlab.com/jrobsonchase/mumble-position-debug/-/package_files/7654457/download
14. Open Command Prompt in the directory with the .exe.
15. Run the following command: `mumble-position-debug-windows.exe -host localhost -game SoTF -ctx InWorld`
16. It should now show you the log output in the command prompt window for debugging the mod. <3

# Known Issues

- [No Known Issues](https://github.com/WWYDF/SoTF-ProximityChat/issues)! :)


# Why isn't it on Thunderstore?
Think of it as an early access stage. Once I am happy with the state of the plugin, I will release it on Thunderstore.
