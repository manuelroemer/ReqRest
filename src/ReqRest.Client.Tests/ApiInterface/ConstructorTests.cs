﻿namespace ReqRest.Client.Tests.ApiInterface
{
    using System;
    using FluentAssertions;
    using ReqRest.Client;
    using Moq;
    using Xunit;
    using System.Reflection;

    public class ConstructorTests : ApiInterfaceTestBase
    {

        [Fact]
        public void Throws_ArgumentNullException_For_ApiClient()
        {
            Action testCode = () => CreateInterface(null, null);
            testCode.Should().Throw<TargetInvocationException>().WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void Sets_BaseUrlProvider_To_Specified_ApiClient_If_Null()
        {
            var @interface = CreateInterface(DefaultApiClient, baseUrlProvider: null);
            @interface.BaseUrlProvider.Should().BeSameAs(@interface.Client);
        }

        [Fact]
        public void Sets_BaseUrlProvider_To_Specified_Value_If_Not_Null()
        {
            var baseUrlProvider = new Mock<IUrlProvider>().Object;
            var @interface = CreateInterface(DefaultApiClient, baseUrlProvider);
            @interface.BaseUrlProvider.Should().BeSameAs(baseUrlProvider);
        }

    }

}
