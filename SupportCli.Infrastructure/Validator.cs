#region Namespaces

using SupportCli.Domain.Models;
using SupportCli.Domain.Seedwork;

#endregion

namespace SupportCli.Infrastructure;

/// <summary>
/// Input Validator
/// </summary>
public class Validator : IValidator
{
    /// <summary>
    /// Validates the specified input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>Returns validation result.</returns>
    public ValidationResult Validate(string input)
    {
        var validationResult = new ValidationResult()
        {
            IsSuccess = true
        };

        if (string.IsNullOrWhiteSpace(input))
        {
            validationResult.IsSuccess = false;
            validationResult.Result = $"input is empty.";
            return validationResult;
        }

        var splitInput = input.Trim().Split(null).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
        validationResult.Command = splitInput[0];

        if (splitInput.Any() && splitInput.Length >= 2)
        {
            // Extract ticket id
            if (validationResult.Command.Contains("show", StringComparison.OrdinalIgnoreCase) ||
                validationResult.Command.Contains("comment", StringComparison.OrdinalIgnoreCase) ||
                validationResult.Command.Contains("assign", StringComparison.OrdinalIgnoreCase) ||
                validationResult.Command.Contains("close", StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(splitInput[1], out int id))
                {
                    validationResult.TicketId = id;
                }
                else
                {
                    validationResult.IsSuccess = false;
                    validationResult.Result = $"Provided ticket id: {splitInput[1]} is not a number.";
                }
            }

            var data = input.Substring(input.Trim().IndexOf(splitInput[1]) + splitInput[1].Length).Trim();

            // Extract Comment
            if (validationResult.Command.Contains("comment", StringComparison.OrdinalIgnoreCase))
            {
                validationResult.Comment = data;
            }

            // Extract User
            if (validationResult.Command.Contains("assign", StringComparison.OrdinalIgnoreCase))
            {
                validationResult.AssignedUser = data;
            }
        }
        else
        {
            validationResult.IsSuccess = false;
            validationResult.Result = $"input is not in correct format.";
        }

        return validationResult;
    }
}

