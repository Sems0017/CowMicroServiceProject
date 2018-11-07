using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CowLocation.Controllers;
using CowLocation.Storage;
using CowLocation.InterService;
using CowLocation.Dto;
using Xunit;

namespace CowLocation.Test.Controllers
{
    public class LocationController_1
    {
        [Test]
        public void LocationReport_OkResult()
        {
            var storage = A.Fake<IStorage>();
            var masterData = A.Fake<IMasterData>();

           

        }
        
        [Fact]
        public void LocationCreation_Created()
        {
            //Arrange
            var storage = A.Fake<IStorage>();
            var masterData = A.Fake<IMasterData>();           

            var sut = new LocationController(storage, masterData);

            //Act
            sut.LocationCreate(A.Fake<LocationCreate>());

            //Assert
            A.CallTo(() => storage.LocationCreateUpdate(A<string>._, A<double>._, A<double>._)).MustHaveHappened();            

        }
    }
}
