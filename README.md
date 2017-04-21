Lib.Web.Mvc 
===========
[![NuGet version](https://badge.fury.io/nu/Lib.Web.Mvc.svg)](http://badge.fury.io/nu/Lib.Web.Mvc)

Lib.Web.Mvc is a library which contains some helper classes for ASP.NET MVC such as strongly typed jqGrid helper, attribute and helper providing support for HTTP/2 Server Push with Cache Digest, attribute and helpers providing support for Content Security Policy Level 2, FileResult providing support for Range Requests, action result and helper providing support for XSL transformation and more.

<a href='https://pledgie.com/campaigns/33546'><img alt='Click here to lend your support to: Lib.Web.Mvc and make a donation at pledgie.com !' src='https://pledgie.com/campaigns/33546.png?skin_name=chrome' border='0' ></a>

## ASP.NET Core

The ASP.NET Core version of this library has been splitted into several independent libraries:
- **[Lib.AspNetCore.Mvc.Security](https://github.com/tpeczek/Lib.AspNetCore.Mvc.Security)** - security features like Content Security Policy and Strict Transport Security
- **[Lib.AspNetCore.Mvc.JqGrid](https://github.com/tpeczek/Lib.AspNetCore.Mvc.JqGrid)** - support for jqGrid usage in ASP.NET Core

## Getting Started

Lib.Web.Mvc is available on [NuGet](https://www.nuget.org/packages/Lib.Web.Mvc/).

```
PM> Install-Package Lib.Web.Mvc
```

Alpha packages are available on [MyGet](http://tpeczek.com/2013/01/using-alpha-libwebmvc-nuget-packages.html).

## Documentation

The library documentation is available on [NuDoq](http://www.nudoq.org/#!/Projects/Lib.Web.Mvc) or as a part of the release (chm file).

There is also a series of blog posts describing key features of the library:

- [jqGrid Strongly Typed Helper - Introduction](http://tpeczek.com/2011/03/jqgrid-and-aspnet-mvc-strongly-typed.html)
- [jqGrid Strongly Typed Helper - Caption layer, dynamic scrolling and grouping](http://tpeczek.com/2011/07/jqgrid-strongly-typed-helper-caption.html)
- [jqGrid Strongly Typed Helper - jQuery UI Integrations](http://tpeczek.com/2013/02/jqgrid-strongly-typed-helper-jquery-ui.html)
- [Range Requests in ASP.NET MVC – RangeFileResult](http://tpeczek.com/2011/10/range-requests-in-aspnet-mvc.html) (How RangeFileResult works)
- [Content Security Policy in ASP.NET MVC - Scripts](http://tpeczek.com/2015/06/content-security-policy-in-aspnet-mvc.html) (How ContentSecurityPolicyAttribute and ContentSecurityPolicyExtensions work)
- [HTTP/2 Server Push and ASP.NET MVC](http://tpeczek.com/2016/12/one-of-new-features-in-http2-is-server.html)
- [HTTP/2 Server Push and ASP.NET MVC - Cache Digest](http://tpeczek.com/2017/01/http2-server-push-and-aspnet-mvc-cache.html)

## Usage examples

There are several sample projects available, showing different features of the library:

- [jqGrid in ASP.NET MVC - Strongly typed helper](http://tpeczek.codeplex.com/releases/view/62741)
- [jqGrid in ASP.NET MVC 3 and Razor](http://tpeczek.codeplex.com/releases/view/61796)
- [VideoJS in ASP.NET MVC](http://tpeczek.codeplex.com/releases/view/74711)
- [XSL Transformation in ASP.NET MVC](http://tpeczek.codeplex.com/releases/view/45199)

## Donating
Lib.Web.Mvc is a personal open source project. If Lib.Web.Mvc has been helpful to you, consider donating. Donating helps support Lib.Web.Mvc.

<a href='https://pledgie.com/campaigns/33546'><img alt='Click here to lend your support to: Lib.Web.Mvc and make a donation at pledgie.com !' src='https://pledgie.com/campaigns/33546.png?skin_name=chrome' border='0' ></a>

## Questions
You are encouraged to ask questions related to Lib.Web.Mvc on [Stack Overflow](http://stackoverflow.com/).

The [lib.web.mvc tag](http://stackoverflow.com/questions/tagged/lib.web.mvc) has been created specially for this purpose and it is being monitored daily by the library author.


## Contribute

Visit the [Contributor Guidelines](https://github.com/tpeczek/Lib.Web.Mvc/blob/master/CONTRIBUTING.md) for more details.

## Copyright and License

Copyright © 2009 - 2017 Tomasz Pęczek

From October 2009 to August 2014 licensed under the Ms-PL License.

From August 2014 licensed under the [MIT License](https://github.com/tpeczek/Lib.Web.Mvc/blob/master/LICENSE.md)
