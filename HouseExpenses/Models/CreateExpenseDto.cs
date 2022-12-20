using FluentValidation;

namespace HouseExpenses.Api.Models;

/// <summary>
/// DTO representing data needed to create expense
/// </summary>
public class CreateExpenseDto
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Jobs collection
    /// </summary>
    public List<JobDto> Jobs { get; set; }

    public class Validator : AbstractValidator<CreateExpenseDto>
    {
        public Validator()
        {
            RuleFor(expense => expense.Name).NotNull().NotEmpty().Length(1, 250);
            RuleFor(expense => expense.Jobs)
                .NotEmpty()
                .WithMessage("At least one job must be provided.");
        }
    }
}
