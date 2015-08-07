# net_cms
Design and Develop a website using ASP.NET MVC 4, EF, Knockoutjs and Bootstrap

# How to use code

Download Database script file here:
Application_DB.sql
It contains SQL Script to create database and master table entry for table PhoneType and AddressType.

To run this application your Visual Studio setting should be enable for “Allow NuGet to download missing packages during build”. Or else refer to below link:

http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages

And finally, modify the connection string under Application.Web project.
References

http://knockoutjs.com/
https://github.com/ericmbarnard/Knockout-Validation/wiki/Configuration
http://twitter.github.com/bootstrap/
http://docs.castleproject.org/Windsor.MainPage.ashx
http://microsoftnlayerapp.codeplex.com/
http://msdn.microsoft.com/en-us/library/ff921348.aspx

# MVC Application: Introduction

All websites are growing faster these days, and once it grows, it is very hard to write, organize and maintain. As we add new functionality or developer to a project, any large web applications with poor design may be go out of control. So the idea behind this article is to design a website architecture that must be simple, easily understandable by any web designer (beginner to intermediate) and Search Engines. For this article I am trying to design a website for any individuals to maintain their contact details online. However, in future, the same application could be used by large community all over the world with added functionality and modules. So, the design should be easily adaptable in order to cope with the future growth of business.

In this article I will talk about creating and designing User Interface (UI) in such a manner so that UI will be separated from the business logic, and can be created independently by any designer/developer. For this part we will use ASP.Net MVC, Knockout Jquery and Bootstrap. later in this article we will discuss more about database design and implementing business logic using structured layers using SQl Server 2008, Entity Framework, and Castle Windsor for Dependency Injection.
Separation of Concern: Primary Objective

The key concept is stripping out most or all logic. Logic should not be bound to a page. What if we need to re-use the logic from one page in another? In that case we will be tempted to copy-and-paste. If we are doing this then our project will become maintainable. Another important concept is to separate data access layer from any business logic, as we are planning to use Entity Framework this is less of a problem as EF should already have this end separate. We should be able to easily move all our EF files to another project and simply add a reference to the projects that need it. Below is the high level design:

![ScreenShot](https://github.com/vanan08/net_cms/blob/master/images/1.png) 

And, the final solution will look like below image in Visual Studio:
![ScreenShot](https://github.com/vanan08/net_cms/blob/master/images/2.png) 

# Seven Projects in One Solution : Is it required ?

It is all about your decision… The proposed designed will offer some rather relevant benefits, which include:
Separation of Concern: Design should allow clear and defined layers; means segregate application into distinct areas whose functionality does not overlap. such that UI-designers can focus on their job without dealing with the business logic (Application.Web), and the core developer can only work on main business logic's (Application.DTO or Application.Manager).
Productivity: It is easier to add new features to existing software, since the structure is already in place, and the location for every new piece of code is known beforehand, so any issue can be easily identified and separated to cope with complexity, and to achieve the required engineering quality factors such as * * robustness, adaptability, maintainability, and re-usability.
Maintainability: It is easier to maintain application, due to clear and defined structure of the code is visible and known, so it’s easier to find bugs and anomalies, and modify them with minimal risk.
Adaptability: New technical features, such a different front ends, or adding a business rule engine is easier to achieve, as your software architecture creates a clear separation of concerns.
Re-usability: Re-usability is another concern on designing any application, because it is one of the main factors to decrease the total cost of ownership, and our design should consider to what extent we can reuse the created Web application and different layers.
In last section of this article, we will discuss the functionality of each individual layer’s in details.
Tools & Technology

# To achieve the final solution, we need below tools/dll:
Visual Studio 2012
ASP.NET MVC 4 with Razor View Engine
Entity Framework 5.0
Castle Windsor for DI
SQL Server 2008/2012
Knockout.js & JQuery
Castle Windsor for DI
Bootstrap CSS
# What we are trying to achieve: Requirement

# Screen 1: Contact List - View all contacts

1.1 This screen should display all the contacts available in Database. 
1.2 User should be able to delete any contact.
1.3 User should able to edit any contact details.
1.4 User should be able to create a new contact.

Initial sketch:
![ScreenShot](https://github.com/vanan08/net_cms/blob/master/images/10.png) 
# Screen 2: Create New Contact

# This screen should display one blank screen to provide functionality as.

2.1 User should be able to Enter his/her First name, Last Name and Email Address.
2.2 User should able to add any number of Phone numbers by clicking on Add numbers. 
2.3 User should able to remove any phone number. 
2.4 User should able to add any number of Addresses by clicking on Add new address. 
2.5 User should able to remove any address. 
2.6 Click on save button should save Contact details in Database and user will return back in Contact List page. 
2.7 Click on Back to Profile button should return back the user to Contact List page. 

Initial sketch:
![ScreenShot](https://github.com/vanan08/net_cms/blob/master/images/11.png) 

# Screen 3: Update Existing Contact

# This screen should display screen with selected contact information details.

3.1 User should be able to modify his/her First name, Last Name and Email Address.
3.2 User should able to modify /delete/Add any number of Phone numbers by clicking on Add numbers or delete link.
3.3 User should able to modify /delete/Add any number of Addresses by clicking on Add new address or delete link.
3.4 Click on save button should update Contact details in Database and user will return back in Contact List page.
3.5 Click on Back to Profile button should return back the user to Contact List page.

Initial Sketch:

![ScreenShot](https://github.com/vanan08/net_cms/blob/master/images/12.png) 

# Part 1: Create Web Application (Knockout.js, Asp.Net MVC and Bootstrap): For Designers

# Before kick-off the UI part, let us see what benefits we are getting using Knockoutjs and Bootstrap along with ASP.NET MVC 4.

Why Knockoutjs: Knockout is an MVVM pattern that works with a javascript ViewModel. The reason this works well with MVC is that serialization to and from javascript models in JSON is very simple, and it is also included with MVC 4. It allows us to develop rich UI's with a lot less coding and whenever we modify our data, Immediate it reflect in the user interface.

Why Bootstrap: Twitter Bootstrap is a simple and flexible HTML, CSS, and Javascript for popular user interface components and interactions. It comes with bundles of CSS styles, Components and Javascript plugins. It provides Cross platform support, which eliminates major layout inconsistencies. Everything just works! Good documentation and the Twitter Bootstrap's website itself is a very good reference for real life example. And, finally, it saves me plenty of times, as it cut the development time to half with very less testing and almost zero browser issues. Some other benefits we can get by using this framework are:
12-Column Grid, fixed layout, fluid or responsive layout.
Base CSS for Typography, code (syntax highlighting with Google prettify), Tables, Forms, Buttons and uses Icons by Glpyhicons .
Web UI Components like Buttons, Navigation menu, Labels, Thumbnails, Alerts, Progress bars and misc.
Javascript plugins for Modal, Dropdown, Scrollspy, Tab, Tooltip, Popover, Alert, Button, Collapse, Carousel and Typehead.

In below steps, we will work through the layout and design to build UI for above requirement by using dummy javascript data.
Step 1:

# Create a new project as Blank Solution; name it as “Application”
![ScreenShot](https://github.com/vanan08/net_cms/blob/master/images/14.png) 

# Step 2:

# Right Click on Solution folder and add new Project of type ASP.NET MVC 4 as an Internet Application Template with View engine as Razor.
![ScreenShot](https://github.com/vanan08/net_cms/blob/master/images/15.png) 
![ScreenShot](https://github.com/vanan08/net_cms/blob/master/images/16.png) 

# After Step 2 - the project structure should look like the below image
![ScreenShot](https://github.com/vanan08/net_cms/blob/master/images/17.png) 
# Step 3:

Right click on References and click on Manage NuGet Packages. Type Bootstrap on search bar then click on Install button.
![ScreenShot](https://github.com/vanan08/net_cms/blob/master/images/18.png) 

# Step 4:

add below line of code into BundleConfig.cs file under App_Start folder to add Knockoutjs and Bootstrap for every page
*
bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                                    "~/Scripts/knockout-{version}.js"));
bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css"));

Also in _Layout,cshtml file under Views/Shared folder add below line to register knockout files as :

@Scripts.Render("~/bundles/knockout")
*

# Step 5:

Add a new folder name as Contact inside Views, and then add Index.cshtml as new View page. Then add a new Controller name it ContactController.cs inside Controller folder, and add a new Contact.js file under Scripts folder. Refer to below image.
