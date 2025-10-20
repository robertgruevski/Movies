using System;
using System.ComponentModel.DataAnnotations;
using API.Validations;

namespace MoviesAPITests;

[TestClass]
public sealed class FirstLetterUppercaseAttributeTests
{
    [TestMethod]
    [DataRow("Robert")]
    [DataRow("")]
    [DataRow("      ")]
    [DataRow(null)]
    public void IsValid_ShouldReturnSuccess_WhenValueIsEmptyOrNullOrAWordThatStartsWithUppercase(string value)
    {
        // Preparation
        var firstLetterUppercaseAttribute = new FirstLetterUppercaseAttribute();
        var validationContext = new ValidationContext(new object());

        // Testing
        var result = firstLetterUppercaseAttribute.GetValidationResult(value, validationContext);

        // Verification
        Assert.AreEqual(expected: ValidationResult.Success, actual: result);
    }

    [TestMethod]
    [DataRow("robert")]
    public void IsValid_ShouldReturnFail_WhenValueStartsWithLowercase(string value)
    {
        // Preparation
        var firstLetterUppercaseAttribute = new FirstLetterUppercaseAttribute();
        var validationContext = new ValidationContext(new object());

        // Testing
        var result = firstLetterUppercaseAttribute.GetValidationResult(value, validationContext);

        // Verification
        Assert.AreEqual(expected: "The first letter should be uppercase", actual: result!.ErrorMessage);
    }
}
