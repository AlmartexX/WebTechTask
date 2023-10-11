using FluentValidation;
using Library.BLL.Validation;
using VebTechTask.BLL.DTO;
using VebTechTask.BLL.Validation.Interfaces;
using VebTechTask.DAL.Data;

namespace VebTechTask.BLL.Validation
{
    public class ValidationPipelineBehavior<TRequest, TResult> : IValidationPipelineBehavior<TRequest, TResult>
    {
        private readonly AppDbContext _dbContext;

        public ValidationPipelineBehavior(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TResult> Process(TRequest request, Func<Task<TResult>> next)
        {
            var validator = GetValidator<TRequest>();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidationException($"The entry is incorrect - {result}");
            }

            return await next();
        }

        private IValidator<TRequest> GetValidator<TRequest>()
        {
            if (typeof(TRequest) == typeof(UpdateUserDTO))
            {
                return new UpdateUserValidator(_dbContext) as IValidator<TRequest>;
            }
            else if (typeof(TRequest) == typeof(CreateUserDTO))
            {
                return new CreateUserValidator(_dbContext) as IValidator<TRequest>;
            }
            
            throw new InvalidOperationException("No validator found for the given request type.");
        }
    }
}
