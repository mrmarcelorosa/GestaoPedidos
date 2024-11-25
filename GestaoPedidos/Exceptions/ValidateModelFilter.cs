using Microsoft.AspNetCore.Mvc.Filters;

namespace GestaoPedidos.Exceptions
{
    public class ValidateModelFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    })
                    .ToList();

                throw new ApiExceptions(400, "Erro de validação", details: Newtonsoft.Json.JsonConvert.SerializeObject(errors));
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
