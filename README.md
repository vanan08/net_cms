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

