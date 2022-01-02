#region Namespaces

using NUnit.Framework;
using SupportCli.Infrastructure;

#endregion

namespace SupportCli.Tests;

internal class ValidatorTests
{
    #region Test Methods

    [Test]
    public void ValidateEmptyInput()
    {
        // Prepare
        var input = "      ";
        var validator = new Validator();

        // Act
        var validationResult = validator.Validate(input);

        // Assert
        Assert.IsNotNull(validationResult);
        Assert.IsFalse(validationResult.IsSuccess);
        Assert.AreEqual($"input is empty.", validationResult.Result);
    }

    [Test]
    public void ValidateIntegerTicketNumber()
    {
        // Prepare
        var input = "show       1  ";
        var validator = new Validator();

        // Act
        var validationResult = validator.Validate(input);

        // Assert
        Assert.IsNotNull(validationResult);
        Assert.IsTrue(validationResult.IsSuccess);
        Assert.IsNull(validationResult.Result);
    }

    [Test]
    public void ValidateInvalidTicketNumber()
    {
        // Prepare
        var input = "show                       1ghtyu      ";
        var validator = new Validator();

        // Act
        var validationResult = validator.Validate(input);

        // Assert
        Assert.IsNotNull(validationResult);
        Assert.IsFalse(validationResult.IsSuccess);
        Assert.AreEqual($"Provided ticket id: 1ghtyu is not a number.", validationResult.Result);
        Assert.IsNotNull(validationResult.Result);
    }

    [Test]
    public void ValidateCommentField()
    {
        // Prepare
        var input = "comment           1            This is my test comment.      ";
        var validator = new Validator();

        // Act
        var validationResult = validator.Validate(input);

        // Assert
        Assert.IsNotNull(validationResult);
        Assert.IsTrue(validationResult.IsSuccess);
        Assert.AreEqual($"This is my test comment.", validationResult.Comment);
        Assert.IsNull(validationResult.Result);
    }

    [Test]
    public void ValidateAssignUserField()
    {
        // Prepare
        var input = "assign           1            Bishwaranjan Sandhu      ";
        var validator = new Validator();

        // Act
        var validationResult = validator.Validate(input);

        // Assert
        Assert.IsNotNull(validationResult);
        Assert.IsTrue(validationResult.IsSuccess);
        Assert.AreEqual($"Bishwaranjan Sandhu", validationResult.AssignedUser);
        Assert.IsNull(validationResult.Result);
    }

    [Test]
    public void ValidateCaseInSensitivityOfCommand()
    {
        // Prepare
        var input = "        SHOW  1           ";
        var validator = new Validator();

        // Act
        var validationResult = validator.Validate(input);

        // Assert
        Assert.IsNotNull(validationResult);
        Assert.IsTrue(validationResult.IsSuccess);
        Assert.IsNull(validationResult.Result);
    }

    [Test]
    public void ValidateInvalidCommand()
    {
        // Prepare
        var input = "  CLOSE    ";
        var validator = new Validator();

        // Act
        var validationResult = validator.Validate(input);

        // Assert
        Assert.IsNotNull(validationResult);
        Assert.IsFalse(validationResult.IsSuccess);
        Assert.IsNotNull(validationResult.Result);
        Assert.AreEqual($"input is not in correct format.", validationResult.Result);
    }

    #endregion
}

