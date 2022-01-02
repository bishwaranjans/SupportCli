#region Namespaces

using SupportCli.Domain.Models;

#endregion

namespace SupportCli.Domain.Seedwork;

/// <summary>
/// Contract for validation
/// </summary>
public interface IValidator
{
    /// <summary>
    /// Validates the specified input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>Returns the validation result.</returns>
    ValidationResult Validate(string input);
}

