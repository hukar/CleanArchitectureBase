namespace API.Filters;

public class ValidationFilter<TToValidate> : IEndpointFilter
    where TToValidate : class
{
    private readonly IValidator<TToValidate> _validator;

    public ValidationFilter(IValidator<TToValidate> validator)
    {
        _validator = validator;
    }
    
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if(context.Arguments.Count > 0)
        {
            var entityReceive = context.Arguments.FirstOrDefault(arg => arg!.GetType() == typeof(TToValidate));

            if(entityReceive is TToValidate entityToValidate)
            {
                var result = _validator.Validate(entityToValidate);

                if(result.IsValid == false)
                {
                    var errors = result.Errors.Select(err => err.ErrorMessage).ToList();

                    return BadRequest(errors);
                }
            }    
        }
        
        return await next(context);
    }
}
