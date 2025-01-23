# UltraKillYourself - The dynamic difficulty mod

## What is UKY?
UKY aims to fix one of the oldest (and ironically one of the longest solved) issues in game design, the absolutely braindead approach to how difficulty is handled. It does this by adjusting the game's difficulty in real time - ensuring you get sufficient ass-kicking, while not kicking your ass too much.

## How does it work?
UKY has an internal counter that ticks up every second, starting at zero and ending after 7 minutes by default (we have configs, yippie!). This counter determines how much damage enemies do, as well as how much health is recovered by parrying. There were a few other effect planned although those haven't been implemented yet. Of course, that alone would make a game that becomes more and more difficult, so to counteract this, every time the player takes damage or dies, the counter ticks back by a configurable amount. The idea is that the counter should then balance itself based on the player's performance. The game's base difficulty options still take effect, UKY acts completely separately from it.

## What values does the counter use?
The counter will tick up every second, and tick down depending on certain conditions:

* The player takes damage (flat, disabled by default)
* The player takes damage, again (instead of a flat value, the value subtracted is the effective damage taken times a configurable multiplier)
* The player dies

You may be thinking, "if the timer ticks up every second, does that mean certain bossfights will start of rather difficult due to longer sections with no enemies?", and of course I thought about that issue, therefore, the timer only starts ticking up once an enemy has been hit with a player's weapon (although internally some other things still trigger this - no clue why), and stop after 10 seconds of no enemy being hurt. Therefore, walking segments will not affect the timer at all.

## Configuring
The mod generates a config file in the BBepInEx config folder, simply open it with a text editor, the rest should be self-explanatory. You can change things like the max counter, how fast the counter ticks down based on player action, the mulitpliers used for damage and parry healing as well as toggling the original behavior for full heals on parrying.

## So is this any good?
Sorta? I was quite limited in how to approach this project due to the fact that I had to worm my way through the game's source code with a shitty disasembler, mostly guessing what certain things do because of backend stuff that Unity does which I can't really know about. The mod currently only adjusts damage taken and amount healed by parrying, initially it was also supposed to change enemy damage taken (although way less drastically, no one likes bullet sponges) as well as remove the style system (I tried and tried and couldn't do it). There are way more ways of tweaking difficulty properly, and just having players take way less or more damage might feel a tad artificial - because it is.

UKY has sorta been tested, not for very long, so I can't estimate how well it's going to work other than from an initial 30 minute test. I will try to play a larger chunk of the game once I get the time to do so.

## The future?
Who knows how long I will stay interested in a project made out of pure spite. Probably not very long at all. One thing I had planned for the initial release was a bullshit dampener - if the dynamic damage modifier is higher than 1 and the modified damage is lethal (while the original would not have been), the modifier should be capped at a lower number, which may or may not render the damage lethal, in any case reducing the amount of bullshit instakills. Who knows if that will ever be necessary, we'll see. Currently the "Cheats enabled" HUD has been hijacked to show if UKY is enabled and what the counter is like right now, this had to be done because I couldn't figure out how to make my own text panel. I know jackshit about Unity, and even less about adding objects in Unity without its editor, just by C# code.

## Installing
UKY is a BepInEx plugin, therefore installing is quite simple:

* Install BepInEx onto Ultrakill
* Throw the UKY DLL file into the BepInEx plugin folder

Installing BepInEx is as easy as downloading the BepInEx release zip and then extracting its contents into the Ultrakill install folder.

If you're on Linux, there are certain things to watch out for:
* Despite there being BepInEx Linux releases, you will need the Windows release, since Ultrakill runs on Proton
* You will likely need to add a DLL override for winhhtp.dll (one of the files included in BepInEx) using protontricks (it's like winetricks but for Proton)
* If you have installed protontricks using flatpak, and your Ultrakill install folder is not on your system drive, you likely need to grant flatpak acces to the install directory using `flatpak override --user --filesystem=<path to install dir> com.github.Matoking.protontricks`

## Building UKY yourself
Simply follow the install instructions for BepInEx to get access to the necessary DLLs. Then open the sln file with a C# IDE of your choice (I was forced using MonoDevelop, ow). Follow the instructions on how to sett up the dotnet workspace on the BepInEx website (which includes installing the dotnet distribution and some wacky command shit), then change the currently hardcoded paths to the game's DLLs in `UltraKillYoursef.csproj` to the right ones.

## Doesn't this defeat the purpose of gitting gud as our godemperor Hakita intended?
Hakita can suck my nuts
