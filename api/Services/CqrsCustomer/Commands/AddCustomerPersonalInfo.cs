using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CompositionApi
{
    public class AddCustomerPersonalInfoCommand
    {

        private readonly Api api;
        private readonly CustomerRepository customerRepository;

        public AddCustomerPersonalInfoCommand(CustomerRepository customerRepository, Api api)
        {
            this.api = api;
            this.customerRepository = customerRepository;
            Init();
        }

        //############################################################

        private static bool TryValidateModel(object model, out List<ValidationResult> results)
        {
            var context = new ValidationContext(model);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results, true);
        }

        //############################################################

        public void Init()
        {
            if (api != null && api.appInstance != null)
            {
                api.appInstance.MapPost("/customers/personalinfo", (HttpRequest request) => AddCustomerPersonalInfo(request));
            }
        }

        //############################################################

        public async Task<IResult> AddCustomerPersonalInfo(HttpRequest request)
        {
            if (api == null || request.Body == null)
            {
                return Results.NoContent();
            }

            CustomerPersonalInfoModel resultBody = await request.ReadFromJsonAsync<CustomerPersonalInfoModel>() ?? new CustomerPersonalInfoModel();
            if (!TryValidateModel(resultBody, out var validationResults))
            {
                return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
            }

            customerRepository?.AddCustomerPersonalInfo(resultBody);
            return Results.Created($"/customers", resultBody);
        }

        //############################################################

    }

}