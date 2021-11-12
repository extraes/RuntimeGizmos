using MelonLoader;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle(Gizmos.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(Gizmos.BuildInfo.Company)]
[assembly: AssemblyProduct(Gizmos.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + Gizmos.BuildInfo.Author)]
[assembly: AssemblyTrademark(Gizmos.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(Gizmos.BuildInfo.Version)]
[assembly: AssemblyFileVersion(Gizmos.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(Gizmos.RuntimeGizmos), Gizmos.BuildInfo.Name, Gizmos.BuildInfo.Version, Gizmos.BuildInfo.Author, Gizmos.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]