# NetStandard2Demo
A demo solution with NET Standard 2.0, referencing the .NETFx 2.0 SharpZipLib library

The SharpZipLib library is available in the refs folder, and was downloaded from [http://www.icsharpcode.net/OpenSource/SharpZipLib/Default.aspx](http://www.icsharpcode.net/OpenSource/SharpZipLib/Default.aspx).  I have included the .NET Framework 2.0 version of the DLL in this project.

ConsoleApp1 is the starting point project written against .NET Framework 4.6.1

CoreConsole2App is a .NET Core 2.0 preview 1 console project that was written with an initial reference DIRECTLY to the ICSharpCode.SharpZipLib.dll.  In its current state in the repository, it now references and uses the output of the Compressor2 project

Compressor2 is a .NET Standard 2.0 preview 1 project that has the exact same compression code from ConsoleApp1 and a direct reference on the ICSharpCode.SharpZipLib.dll

As a follow-up to showing how this project works on Windows in Visual Studio 2017 Update 3 Preview, I copied the compiled output of the CoreConsole2App to a USB stick and ran it on my Mac with .NET Core 2 preview 1.