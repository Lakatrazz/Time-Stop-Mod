# zCubed's Boneworks Multi-Tool v1.3.0 - UNSTABLE BRANCH

### For unstable releases, download from the source here
### For stable releases, download from https://bonetome.com/mods/10 or releases tab here

**Update v1.2.1**
* The entire project has been refactored and restructured for easy modification and usage as a library.  
* The project is now open source, anyone can help contribute via pull requests and forks!  

**Update v1.2.2**
* ADDED - Instance ID Finder, allows for people to find Instance IDs so they can precache prefabs and other game content.
* ADDED - Instanced ID Keybind.

**Update v1.3.0**
* ADDED - Free Camera has the ability to act as a Third Person Camera.
* ADDED - New Input System, allowing for the mod to lock controls into categorys, meaning that it is now easier to classify and 
prevent control conflicts.
* FIXED - Time Scale broke ingame Time Controls.
* FIXED - Camera LookAt().
* FIXED - Performance Issues, the mod had some functions that were misbehaving, or were hogging resources.
* MORE BEING ADDED!

**Feature List**  
* Gravity Modifier  
* Time Scale Modifier  
* Free Camera
* Free Camera tracking player
* Free Camera control using WASD + Mouse  
* Gravity Cube (Object Oriented Gravity)  
* Loaded Scene Outputter  
* Entire Scene Outputter, goes through the entire scene, listing GameObjects + their Children + their Components
allowing for other modders to locate Components or GameObjects they want to modify.
* Modular systems, allowing for other modders to utilize this mod as an extension of their mod
* It's up to you if you count this as a feature, but this mod is open source! Add onto it and submit a pull request and I may add it to the mod! 

**Normal Controls**  
* Q = Increment Gravity  
* W = Decrement Gravity  
* A = Increment TimeScale  
* D = Decrement TimeScale  
* 0 (Zero) = Zero Gravity Toggle  
* C = Gravity Cube spawn and despawn  
* G = Spawn Free Camera / Take control of camera
* R = Reset Gravity and TimeScale  
* Tab = Output Gravity and TimeScale
* Insert = Output all Object IDs in the entire scene
* Home = Output all Scene Objects and Components in the entire scene

**Camera Mode Controls**  
* W & S = Forward and Back (Piloting) 
* A & D = Left and Right (Piloting) 
* C & V = Up and Down (Piloting) 
* O = Orthographic mode
* H = Pilot Mode
* F = Follow Mode
* T = Third Person Mode
* B = Cycle Camera Offset Axis (Third Person Mode)
* Scroll Wheel = Offset Active Axis (Third Person Mode)
* R = Toggle FOV Modifier (Change with Scroll Wheel)  
* Y = Toggle Speed Multiplier Scaler (Change with Scroll Wheel)
* Tab = Output current value being modified (Will be changed later for a GUI)
* Mouse = Rotate Camera

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

**Requirements For Contributing**
* A pull request with your changes inside.
* The request has been reviewed, if it is fine, you will have no issues. If there is a discrepancy of any sort, we will tell you about it.

**Crediting**
* If you make a video for this mod, you can share it with me and I will consider adding it.
* If you use this mod in a video, consider giving credit to the GitHub page.
* If you use this mod as a library for your mod, there is no need to give credit.
