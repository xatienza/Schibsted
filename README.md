# Overview

Implement a Web Application using C# language.

This application will have 3 different private pages and a login form.

In order to access any of these private pages, the user must have a session started through the login form and must have the right role to be able to access the page.

The application will also have a REST API endpoint exposing the “User” resource.

Creating, deleting and modifying users and their permissions will be done through this API.

#Installation

  - Download the code and open with Visual Studio 2013 Express or above
  - NuGet packages are included. If you have any problem please compile the solution
    which will also restore the missing packages.

#Run the application

Prerequisites:

  - App and tests require Visual Studio 2013 on the machine to run.

If you have Visual Studio 2013

  Open Schibsted.sln in Visual Studio 2013 and run the Schibsted.WebAPI and Schibsted.WebSPA applications on IIS Express.

NOTE: Defaul user => Admin | password => 1234

#Run the test

You use the executable program MSTest.exe to run tests from the command line. This program can run any tests that can be automatically run, that is, any tests other than manual tests.

To run tests from the command line:

  1.- Open a Visual Studio command prompt.
    To do this, choose Start, point to All Programs, point to Microsoft Visual Studio 2013, point to Visual Studio Tools, and then choose Developer Command Prompt.

    By default, the Visual Studio command prompt opens to the following folder:
        <drive letter>:\Program Files\Microsoft Visual Studio 12.0\VC

  2.- Either change directory to your solution folder or, when you run the MSTest.exe     program in step 3, specify a full or relative path to the metadata file or to the test container.

  3.- Run the MSTest.exe program.
