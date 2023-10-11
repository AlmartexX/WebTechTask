using Microsoft.Extensions.Logging;

namespace VebTechTask.BLL.GlobalExceptionHandler
{
    public class GlobalErrorHandler
    {
        private readonly ILogger<GlobalErrorHandler> _logger;

        public GlobalErrorHandler(ILogger<GlobalErrorHandler> logger)
        {
            _logger = logger;
        }

        public void HandleError(string errorMessage)
        {
            _logger.LogError(errorMessage);
        }

        public void HandleNotFoundError(string entityName, int entityId)
        {
            var errorMessage = $"Object of type {entityName} with ID {entityId} not found.";
            _logger.LogError(errorMessage);
        }

        public void HandleValidationError(string errorMessage)
        {
            _logger.LogError(errorMessage);
        }
    }
}

