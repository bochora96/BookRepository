using FluentValidation;

namespace TemoDemo.Application.Features.Book.Commands.Add;

public class AddCommandValidator : AbstractValidator<AddCommand>
{
    public AddCommandValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("Title cannot be empty");

        RuleFor(book => book.Author)
            .NotEmpty().WithMessage("Author cannot be empty")
            .Must(author => author.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length >= 1)
            .WithMessage("Author must have at least one name");

        RuleFor(book => book.DateOfPublication)
            .NotEmpty().WithMessage("Date of Publication cannot be empty")
            .LessThan(DateTime.Now).WithMessage("Date of Publication must be in the past");
    }
}
