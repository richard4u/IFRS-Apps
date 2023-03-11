using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web.UI;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ELMAH")]
[assembly: AssemblyDescription("Error Logging Modules and Handlers (ELMAH) for ASP.NET")]
[assembly: AssemblyConfiguration(Elmah.Build.Configuration)]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ELMAH")]
[assembly: AssemblyCopyright("Copyright \u00a9 2004, Atif Aziz. All rights reserved.")]
//[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("79d52df0-3744-4f21-9322-8faebd1e6de2")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2.0.15523.27")]
[assembly: AssemblyFileVersion("2.0.15523.27")]

[assembly: CLSCompliant(true)]


[assembly: Elmah.Scc("$Id: AssemblyInfo.cs 923 2011-12-23 22:02:10Z azizatif $")]

[assembly: InternalsVisibleTo("Elmah.Tests")]
[assembly: WebResource("Bootstrap.css", "text/css")]