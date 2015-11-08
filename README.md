<h1><img src="https://raw.githubusercontent.com/ivaylokenov/MyWebApi/master/documentation/logo.png" align="left" alt="MyWebApi" width="100">&nbsp;&nbsp;&nbsp; MyWebApi - ASP.NET Web API <br />&nbsp;&nbsp;&nbsp; Fluent Testing Framework</h1>
====================================

MyWebApi is unit testing library providing easy fluent interface to test the ASP.NET Web API 2 framework. Inspired by [TestStack.FluentMVCTesting](https://github.com/TestStack/TestStack.FluentMVCTesting) and [ChaiJS](https://github.com/chaijs/chai).

[![Build status](https://ci.appveyor.com/api/projects/status/738pm1kuuv7yw1t5?svg=true)](https://ci.appveyor.com/project/ivaylokenov/mywebapi) [![NuGet Version](http://img.shields.io/nuget/v/MyWebApi.svg?style=flat)](https://www.nuget.org/packages/MyWebApi/) [![NuGet Downloads](http://img.shields.io/nuget/dt/MyWebApi.svg?style=flat)](https://www.nuget.org/packages/MyWebApi/) [![Coverage Status](https://coveralls.io/repos/ivaylokenov/MyWebApi/badge.svg?branch=master&service=github)](https://coveralls.io/github/ivaylokenov/MyWebApi?branch=master)

## Documentation

Please see the [documentation](https://github.com/ivaylokenov/MyWebApi/tree/master/documentation) for full list of available features. Everything listed in the documentation is 100% covered by [more than 800 unit tests](https://github.com/ivaylokenov/MyWebApi/tree/master/src/MyWebApi.Tests) and should work correctly. Almost all items in the [issues page](https://github.com/ivaylokenov/MyWebApi/issues) are expected future features and enhancements.

## Installation

You can install this library using NuGet into your Test class project. It will automatically reference the needed dependencies of Microsoft.AspNet.WebApi.Core (≥ 5.1.0) and Microsoft.Owin.Testing (≥ 3.0.1) for you. .NET 4.5+ is needed.

    Install-Package MyWebApi

## How to use

Make sure to check out [the documentation](https://github.com/ivaylokenov/MyWebApi/tree/master/documentation) for full list of available features.
You can also check out [the provided samples](https://github.com/ivaylokenov/MyWebApi/tree/master/samples) for real-life ASP.NET Web API application testing.

Basically you can create a test case by using the fluent API the library provides.

```c#
namespace MyApp.Tests.Controllers
{
    using MyApp.Controllers;
	using MyWebApi;
	using NUnit.Framework;

    [TestFixture]
    public class HomeControllerShould
    {
        [Test]
        public void ReturnOkWhenCallingGetAction()
        {
            MyWebApi
                .Controller<HomeController>()
                .Calling(c => c.Get())
                .ShouldReturn()
				.Ok();
        }
	}
}
```

The example uses NUnit but you can use whatever testing framework you want.
Basically, the framework throws an unhandled exception if the assertion does not pass and the test fails.

Here are some random examples of what the fluent testing API is capable of:

```c#
// tests a route for correct controller, action and resolved route values
MyWebApi
	.Routes()
	.ShouldMap("api/WebApiController/SomeAction/5")
	.WithJsonContent(@"{""SomeInt"": 1, ""SomeString"": ""Test""}")
	.And()
	.WithHttpMethod(HttpMethod.Post)
	.To<WebApiController>(c => c.SomeAction(5, new RequestModel
	{
		SomeInt = 1,
		SomeString = "Test"
	}))
	.AndAlso()
	.ToNoHandler()
	.AndAlso()
	.ToValidModelState();

// injects dependencies into controller
// and mocks authenticated user
// and tests for valid model state
// and tests response model from Ok result with specific assertions
MyWebApi
	.Controller<WebApiController>()
	.WithResolvedDependencyFor<IInjectedService>(mockedInjectedService)
	.WithResolvedDependencyFor<IAnotherInjectedService>(anotherMockedInjectedService);
	.WithAuthenticatedUser(user => user.WithUsername("NewUserName"))
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.ValidModelState()
	.AndAlso()
	.ShouldReturn()
	.Ok()
	.WithResponseModelOfType<ResponseModel>()
	.Passing(m =>
	{
		Assert.AreEqual(1, m.Id);
		Assert.AreEqual("Some property value", m.SomeProperty);
	});;

// tests whether model state error exists by using lambda expression
// and specific tests for the error messages
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.ModelStateFor<RequestModel>()
	.ContainingModelStateErrorFor(m => m.SomeProperty).ThatEquals("Error message") 
	.AndAlso()
	.ContainingModelStateErrorFor(m => m.SecondProperty).BeginningWith("Error") 
	.AndAlso()
	.ContainingModelStateErrorFor(m => m.ThirdProperty).EndingWith("message") 
	.AndAlso()
	.ContainingModelStateErrorFor(m => m.SecondProperty).Containing("ror mes"); 
	
// tests whether the action throws internal server error
// with exception of certain type and with certain message
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.InternalServerError()
	.WithException()
	.OfType<SomeException>()
	.AndAlso()
	.WithMessage("Some exception message");
	
// run full pipeline integration test
MyWebApi
	.Server()
	.Working(httpConfiguration)
	.WithHttpRequestMessage(
		request => request
			.WithMethod(HttpMethod.Post)
			.WithRequestUri("api/WebApiController/SomeAction/1"))
	.ShouldReturnHttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK)
	.AndAlso()
	.ContainingHeader("MyCustomHeader");
```

## License

Code by Ivaylo Kenov. Copyright 2015 Ivaylo Kenov.
This library is licensed under [GNU General Public License v3](https://tldrlegal.com/license/gnu-general-public-license-v3-(gpl-3)) (full terms and conditions [here](https://www.gnu.org/licenses/gpl.html)). Basically:

 - If you create software that uses GPL, you must license that software under GPL v3 (see [GPL FAQ](http://www.gnu.org/licenses/gpl-faq.html#IfLibraryIsGPL))
 - If you create software that uses GPL, you must release your source code (see [GPL FAQ](http://www.gnu.org/licenses/gpl-faq.html#IfLibraryIsGPL))
 - If you start with a GPL license, you cannot convert to another license
 - **You cannot include MyWebApi in a closed source distribution under this license**

If you have a really-coolish-and-nice open source or just closed source commercial project and you want to include MyWebApi in it, leave a question on the [issues page](https://github.com/ivaylokenov/MyWebApi/issues) and another license with the latest version of the library will be provided **free of charge** to you.
 
## Any questions, comments or additions?

Leave an issue on the [issues page](https://github.com/ivaylokenov/MyWebApi/issues) or send a [pull request](https://github.com/ivaylokenov/MyWebApi/pulls).