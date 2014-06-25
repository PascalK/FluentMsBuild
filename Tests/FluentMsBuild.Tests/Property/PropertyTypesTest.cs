using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FluentMsBuild
{
    /// <summary>
    /// Summary description for PropertyTypesTest
    /// </summary>
    [TestClass]
    public class PropertyTypesTest
    {
        [TestClass]
        public class TheGuidPropertyType
        {
            [TestMethod]
            public void ShouldResultInTheCorrectDefaultFormat()
            {
                PropertyTypes<Guid> guidPropertyType;
                string acutalGuidText;
                Guid actualGuid;
                //Arrange
                acutalGuidText = "0376ee63-a34d-4acc-bec3-2afa91e4820f";
                actualGuid = new Guid(acutalGuidText);
                guidPropertyType = PropertyTypes<Guid>.Get();
                //Act
                var actualResult = guidPropertyType.ToValue(actualGuid);
                //Assert
                actualResult.Should().Match(string.Format("{{{0}}}", acutalGuidText));
            }
        }
        [TestClass]
        public class TheGuidsPropertyType
        {
            [TestMethod]
            public void ShouldResultInTheCorrectDefaultFormat()
            {
                PropertyTypes<IEnumerable<Guid>> guidsPropertyType;
                string actualGuidText1;
                string actualGuidText2;
                Guid actualGuid1;
                Guid actualGuid2;
                IEnumerable<Guid> actualGuids;
                //Arrange
                actualGuidText1 = "0376ee63-a34d-4acc-bec3-2afa91e4820f";
                actualGuidText2 = "615da1ab-058d-4723-a27f-cb995fc923da";
                actualGuid1 = new Guid(actualGuidText1);
                actualGuid2 = new Guid(actualGuidText2);
                actualGuids = new[] 
                {
                    actualGuid1, 
                    actualGuid2 
                };
                guidsPropertyType = PropertyTypes<IEnumerable<Guid>>.Get();
                //Act
                var actualResult = guidsPropertyType.ToValue(actualGuids);
                //Assert
                actualResult.Should().Match(string.Format("{{{0}}};{{{1}}}", actualGuidText1, actualGuidText2));
            }
        }
        [TestClass]
        public class AnUndefinedPropertyType
        {
            [TestMethod]
            public void ShouldResultInTheDefaultToStringValueOfThatObjectType_Object()
            {
                PropertyTypes<object> objectPropertyType;
                object actualObject;
                //Arrange
                actualObject = new object();
                objectPropertyType = PropertyTypes<object>.Get();
                //Act
                var actualResult = objectPropertyType.ToValue(actualObject);
                //Assert
                actualResult.Should().Match("System.Object");
            }
            [TestMethod]
            public void ShouldResultInTheDefaultToStringValueOfThatObjectType_Random()
            {
                PropertyTypes<Random> randomPropertyType;
                Random actualRandom;
                string expectedResult;
                //Arrange
                expectedResult = new Random().ToString();
                actualRandom = new Random();
                randomPropertyType = PropertyTypes<Random>.Get();
                //Act
                var actualResult = randomPropertyType.ToValue(actualRandom);
                //Assert
                actualResult.Should().Match(expectedResult);
            }
        }
    }
}