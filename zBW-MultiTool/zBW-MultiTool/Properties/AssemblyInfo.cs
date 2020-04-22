using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

#if !DEBUG
[assembly: AssemblyTitle(zCubed.BuildInfo.Name)]
#else
[assembly: AssemblyTitle(zCubed.BuildInfo.Name + "-DEBUG")]
#endif
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(zCubed.BuildInfo.Company)]
[assembly: AssemblyProduct(zCubed.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + zCubed.BuildInfo.Author)]
[assembly: AssemblyTrademark(zCubed.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(zCubed.BuildInfo.Version)]
[assembly: AssemblyFileVersion(zCubed.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonModInfo(typeof(zCubed.MultiTool), zCubed.BuildInfo.Name, zCubed.BuildInfo.Version, zCubed.BuildInfo.Author, zCubed.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonModGame("Stress Level Zero", "BONEWORKS")]