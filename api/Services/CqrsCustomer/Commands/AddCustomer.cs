using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CompositionApi
{
    public class AddCustomerCommand
    {
        private readonly Api api;
        private readonly CustomerRepository customerRepository;

        public AddCustomerCommand(CustomerRepository customerRepository, Api api)
        {
            this.api = api;
            this.customerRepository = customerRepository;
            Init();
        }

        //############################################################

        public void Init()
        {
            if (api != null && api.appInstance != null)
            {
                api.appInstance.MapPost("/cqrscustomers", (HttpRequest request) => AddCustomer(request));
            }
        }

        //############################################################

        public async Task<IResult> AddCustomer(HttpRequest request)
        {
            if (api == null || request.Body == null)
            {
                return Results.NoContent();
            }

            CustomerModel requestBody = await request.ReadFromJsonAsync<CustomerModel>() ?? new CustomerModel();
            if (!TryValidateModel(requestBody, out var validationResults))
            {
                return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
            }

            customerRepository?.AddCustomer(requestBody);
            return Results.Created($"/cqrscustomers", requestBody);
        }

        //############################################################

        private static bool TryValidateModel(object model, out List<ValidationResult> results)
        {
            var context = new ValidationContext(model);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results, true);
        }

        //############################################################

    }

}