using FluentValidation;
using VebTechTask.BLL.DTO;
using VebTechTask.DAL.Data;

namespace VebTechTask.BLL.Validation
{
    public class CreateUserValidator : AbstractValidator<CreateUserDTO>
    {
        private readonly AppDbContext _dbContext;

        public CreateUserValidator(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Поле Name обязательно для заполнения.")
              .MaximumLength(100);

            RuleFor(x => x.Age)
              .NotEmpty().WithMessage("Поле Age обязательно для заполнения.")
              .GreaterThan(0).WithMessage("Age должен быть положительным числом.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Поле Email обязательно для заполнения.")
                .EmailAddress().WithMessage("Email имеет неверный формат.")
                .Must(BeUniqueEmail).WithMessage("Этот email уже зарегистрирован.");
        }

        private bool BeUniqueEmail(string email)
        {
            return !_dbContext.Users.Any(u => u.Email == email);
        }
    }
}
