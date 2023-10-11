using FluentValidation;
using VebTechTask.BLL.DTO;

namespace VebTech.BLL.Validation
{
    public class RegisterUserValidator : AbstractValidator<CreateUserDTO>
    {

        public RegisterUserValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Поле Name обязательно для заполнения.")
              .MaximumLength(100);

            RuleFor(x => x.Age)
              .NotEmpty().WithMessage("Поле Age обязательно для заполнения.")
              .GreaterThan(0).WithMessage("Age должен быть положительным числом.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Поле Email обязательно для заполнения.")
                .EmailAddress().WithMessage("Email имеет неверный формат.");

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(64);
        }
    }
}
