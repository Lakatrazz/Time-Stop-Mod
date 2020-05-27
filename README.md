# zCubed's Boneworks Multi-Tool v1.3.1 - Release

### THIS IS NOW CONSIDERED LEGACY AND IS BEING REMADE IN A SERIES OF MODS

### For unstable releases, download from the source here
### For stable releases, download from https://bonetome.com/mods/10 or releases tab here

**Notice**
* PLEASE READ THE CONTROLS. The mod has undergone major core changes between v1.2.2 and v1.3.0, meaning that all the controls have changed, sorry for this.

**Changelog**  
* This has been moved to a new file called CHANGELOG.md

**Feature List**  
* Gravity Modifier  
* Time Scale Modifier  
* Free Camera
* Free Camera tracking player
* Free Camera third person
* Free Camera first person
* Free Camera physics mode
* Free Camera turn table view
* Free Camera control using WASD + Mouse  
* Proper Ford Head for filming!
* Action Camera Post Processing Toggle
* Gravity Cube (Object Oriented Gravity)  
* Black Hole (Sucks everything around it inside it)
* Green Screen for filming
* Turn Table for filming guns and asset mods
* Scene Light Modifier (WIP, ONLY DARKENS AND BRIGHTENS LIGHTS)
* Loaded Scene Outputter  
* Object ID Lister (Entire AssetDatabase)
* Object ID Lister (Scene's Objects)
* Material Stripper (Whiteworld)
* Entire Scene Outputter, goes through the entire scene, listing GameObjects + their Children + their Components
allowing for other modders to locate Components or GameObjects they want to modify.

**Normal Controls**  
* 6 = Lock / Unlock the mod
* G = Spawn Camera / Take Control of Camera
* F = Fun Controls
* T = Tool Controls

**Camera Mode Controls**  
* E = Exit Mode
* F = Face Player Mode
* T = Third Person
* H = Pilot Mode
* J = First Person
* U = TurnTable Mode
* B = Physics Mode
* G = Recenter On Player
* R = Toggle FOV Modifier
* Y = Toggle Speed Modifier
* P = Toggle Post Processing
* WS = Pilot Mode Forward / Back
* AD = Pilot Mode Left / Right
* Scroll Wheel = Offset Active Modifier
* M = List Modifiers
* (Third Person) B = Switch Axis Offset


**Fun Controls**
* E = Exit Mode
* B = Black Hole Spawn / Remove
* C = Gravity Cube Spawn / Remove
* N = Chroma Screen Spawn / Remove
* M = Chroma Screen Flip Color
* V = TurnTable Spawn / Remove
* 0 = Toggle Zero G / Revert Gravity
* Q / W = Incremenet / Decrement Gravity Scale
* A / D = Increment / Decrement Time Scale
* Z / X = Increment / Decrement Light Modifier (WIP)
* R = Reset Values To Defaults
* T = Output Values

**Tool Controls**
* E = Exit Mode
* Q = Output Object Instance IDs (Entire AssetDatabase, WILL FREEZE YOUR GAME FOR A BIT)
* W = Strip Scene's Materials
* A = List Entire Scene (GameObjects, Children, and Components, WILL FREEZE YOUR GAME FOR A BIT)
* S = List Scene's Object Instance IDs (WILL FREEZE YOUR GAME FOR A BIT)
* D = Log the Player's head position

**Known Issues**
* Zero Gravity will break the game, spawn AI before entering Zero Gravity, when they disappear, keep Zero Gravity enabled and reload the scene.  
* Loading into a new scene with Zero Gravity will make the player invisible, reload the scene again to fix.  
* Gravity Cube can't be picked up with hands, you have to use Gravity Cups, Gravity Plates, or Dev Manipulators (Gravity Guns).  
* Free Camera will **NOT** work unless your spectator options are set to **Position = Center, Action Mode = Enabled**, this is a fault of the game that I can not fix.  
* Free Camera will break spectator options, this is entirely the fault of how I have the Free Camera set up, there is no fix except changing the setting before creating the Free Camera.

**Troubleshooting**
* If nothing happens, no console appears, absolutely nothing, you may have not installed MelonLoader
* If your console is being spammed with yellow errors, please create the libraries required, join the MelonLoader Discord for instructions and help, https://discord.gg/BpkCdrW.
* If your mod isn't loading, you may have not put it inside the BONEWORKS/Mods folder
* If you have an unknown error, please contact me on Discord at zCubed#2312

**Issue Reporting**
* If an issue has been noted on the current release's changelog, we are well aware of the issue and we will try to fix it if we can.
* If you are getting an error, post the console output alongside your issue report.

**Feature Requesting**
* Request for features inside issues or on the Boneworks Discord server.
* Fork the repo and add your changes and pull request your changes back into it.

**Requirements For Contributing**
* A pull request with your changes inside.
* The request has been reviewed, if it is fine, you will have no issues. If there is a discrepancy of any sort, we will tell you about it.

**Crediting**
* If you make a video for this mod, you can share it with me and I will consider adding it as a showcase video.
* If you use this mod in a video, make sure to give credit to the GitHub page.
* If you use this mod as a library for your mod, there is no need to give credit.
