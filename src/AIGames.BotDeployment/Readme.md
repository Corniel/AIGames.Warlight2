usage: source botname [version] [DEBUG]

AI Games Bot deployment
=======================
The deployment includes a compilation of all C# files of the source excluding 
the AssemblyInfo.cs and *.Debug.cs (if not in DEBUG mode).

The collection of C# files is added to a compressed zip file that can be 
uploaded to http://theaigames.com.

If a version is supplied this is added to the name of the generated executable.

The location of the core lib files should be configured in the App.config both 
with the location of the bots output directory.
