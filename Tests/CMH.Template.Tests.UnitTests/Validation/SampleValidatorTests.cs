using CMH.MobileHomeTracker.Dto;
using CMH.MobileHomeTracker.Infrastructure.Validation;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CMH.MobileHomeTracker.Domain.UnitTests.Validation
{
    [TestFixture]
    public class SampleValidatorTests
    {
        private IValidator<Home> _validator;
        public static List<char> InvalidCharacters = new List<char> { ';', '<', '>', '[', ']', '{', '}', '&', '$', '%' };

        [SetUp]
        public void SetUp()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;
            _validator = new HomeValidator();
        }

        [Test]
        public void Valid()
        {
            var dto = GetValidSample();
            var result = _validator.Validate(dto);

            Assert.IsTrue(result.IsValid);
            Assert.IsNotNull(result.Errors);
            Assert.AreEqual(0, result.Errors.Count);
        }

        [Test]
        public void NameTooLong_Invalid()
        {
            var dto = GetValidSample();

            dto.Name = dto.Name.PadRight(26, 'z');

            var result = _validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("The length of 'Name' must be 25 characters or fewer. You entered 26 characters.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void DescriptionTooLong_Invalid()
        {
            var dto = GetValidSample();

            dto.Description = dto.Description.PadRight(101, 'z');

            var result = _validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("The length of 'Description' must be 100 characters or fewer. You entered 101 characters.", result.Errors[0].ErrorMessage);
        }

        [Test]
        [TestCaseSource(nameof(InvalidCharacters))]
        public void NameInvalidCharacters_Invalid(char c)
        {
            var dto = GetValidSample();

            dto.Name = $"Na{c}e";

            var result = _validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("'Name' contains an invalid character.", result.Errors[0].ErrorMessage);
        }

        [Test]
        [TestCaseSource(nameof(InvalidCharacters))]
        public void DescriptionInvalidCharacters_Invalid(char c)
        {
            var dto = GetValidSample();

            dto.Description = $"Sample Desc{c}iption";

            var result = _validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("'Description' contains an invalid character.", result.Errors[0].ErrorMessage);
        }

        private Home GetValidSample()
        {
            return new Home
            {
                Id = Guid.NewGuid(),
                Name = "Sample",
                Description = "A sample object for the api"
            };
        }
    }
}
