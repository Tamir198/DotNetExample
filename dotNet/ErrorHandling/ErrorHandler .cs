using System.Text.Json;

namespace dotNet.ErrorHandling {
    public class ErrorHandler {
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(ILogger<ErrorHandler> logger) {
            _logger = logger;
        }

        public void HandleError(Exception ex, HttpContext context) {
            if (ex == null) return;

            _logger.LogError(ex, "An error occurred");

            var error = new ErrorResponse {
                Message = "An error occurred while processing your request. Please try again later.",
                Details = ex.Message
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}