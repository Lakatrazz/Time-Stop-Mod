using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

#if !DEBUG
[assembly: AssemblyTitle(Lakatrazz.BuildInfo.Name)]
#else
[assembly: AssemblyTitle(Lakatrazz.BuildInfo.Name + "-DEBUG")]
#endif
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(Lakatrazz.BuildInfo.Company)]
[assembly: AssemblyProduct(Lakatrazz.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + Lakatrazz.BuildInfo.Author)]
[assembly: AssemblyTrademark(Lakatrazz.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(Lakatrazz.BuildInfo.Version)]
[assembly: AssemblyFileVersion(Lakatrazz.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonModInfo(typeof(Lakatrazz.TimeStop), Lakatrazz.BuildInfo.Name, Lakatrazz.BuildInfo.Version, Lakatrazz.BuildInfo.Author, Lakatrazz.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonModGame("Stress Level Zero", "BONEWORKS")]