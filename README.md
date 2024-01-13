# Spray Paint Assignment

This is a WPF application that performs the following requirements/tasks:

Requirements:

1.	Create a form that allows users to select and load an image (JPG, PNG, BMP, etc. )
2.	The user will be able to spray paint the image using the mouse. Line drawing is not accepted.
3.	The user will be able to change the color and the density of the paint.
4.	The user can erase some or all of the spray using the mouse.
5.	The user will be able to save changes to a new image.
6.	The user can save the changes and close, open the application again, and edit and update the spray paint on the same image. Spray should NOT be saved on the image in this case; they should be saved into a separate file.


Generating the executable file:
The executable version of the code is created using Advanced Installer Version 21.2.2 - https://www.advancedinstaller.com/download.html . The executable file is named: “CaseGuard Spray Paint Project” which can be found in the parent folder and can also be found in the Setup Files folder


Main Technologies:

The Technologies I Used to create this Application are listed below:

•	Visual Studio 2022: Download Visual Studio Tools - Install Free for Windows, Mac, Linux (microsoft.com)

•	.NET Core: .NET 8.0 SDK (comes with Visual Studio) - https://dotnet.microsoft.com/en-us/download/dotnet/8.0

•	Target Framework: .NET Framework 4.8 - https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48


Packages:

Packages Used in creating the Application: These secondary packages came in handy to customize the UI interface, I opted for packages with easy and extensive documentation. They are listed below:

•	I used the “Extended.Wpf.Toolkit” package for the ColorPicker functionality in the application – Version 4.5.1 : https://github.com/xceedsoftware/wpftoolkit

•	“ControlzEx” – Version 4.3.0 : https://www.nuget.org/packages/ControlzEx/4.3.0

•	I used “MahApps.Metro” package for some inspiration with the UI look – Version 2.0.0 : https://github.com/MahApps/MahApps.Metro https://mahapps.com/

•	“MaterialDesignColors” package – Version 2.1.4 : https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit

•	“MaterialDesignThemes” package – Version 4.9.0 : https://www.nuget.org/packages/MaterialDesignThemes/ https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit

•	“MaterialDesignThemes.MahApps” package for WPF controls– Version 0.3.0: https://www.nuget.org/packages/MaterialDesignThemes.MahApps/

•	“Microsoft.Xaml.Behavior.Wpf” NuGet package for reusable code interaction in WPF – Version 1.1.39: https://github.com/microsoft/XamlBehaviorsWpf

•	“WriteableBitmapEx” package – Version 1.6.8: https://www.nuget.org/packages/WriteableBitmapEx/ https://github.com/reneschulte/WriteableBitmapEx

