using FakeItEasy;
using MasterData.Controllers;
using MasterData.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MasterData.Tests.Controllers
{
   public class CowControllerTest
    {
        [Fact]
        public void CowRead_ValidEarTag_OkResult()
        {
            // We mock the storage because it is an external dependency
            // When unit testing it is very important to mock out external dependencies (Dependencies that are dependency injected).
            // If we dont do it, it is an integration test and not a unit test.
            var storage = A.Fake<IStorage>();

            // In this case we test the CowRead method so we dont need to mock it.
            var sut = new CowController(storage);

            // Here we call the method getting the result for assertion.
            var result = sut.CowRead(A.Dummy<string>()).Result;

            // We test the returned type is correct here, because we are testing an API controller returning http200.
            Assert.IsType<OkObjectResult>(result);           
        }

        [Fact]
        public void CowRead_ExceptionThrown_BadRequest()
        {
            var storage = A.Fake<IStorage>();
            var controller = new CowController(storage);

            A.CallTo(() => storage.Read(A<string>.Ignored)).Throws<ArgumentException>();

            var result = controller.CowRead(A.Dummy<string>()).Result;

            Assert.IsType<BadRequestResult>(result);
        }

    }
}
