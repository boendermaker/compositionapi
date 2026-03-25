using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CompositionApi {
    public class CustomerService {
        private readonly Api api;
        private readonly CustomerRepository customerRepository;

        public CustomerService(CustomerRepository customerRepository, Api api) {
            this.api = api;
            this.customerRepository = customerRepository;
            Init();
        }

    //############################################################

        public void Init() {
            if (api != null && api.appInstance != null) {
                api.appInstance.MapGet("/customers", () => GetAllCustomers());
                api.appInstance.MapGet("/customers/{id}", (int id) => GetCustomerById(id));
                api.appInstance.MapPost("/customers", (HttpRequest request) => AddCustomer(request));
                api.appInstance.MapPost("/customers/personalinfo", (HttpRequest request) => AddCustomerPersonalInfo(request));
            }
        }

    //############################################################

        public IEnumerable<CustomerModel> GetAllCustomers() {
            return customerRepository?.GetAllCustomers() ?? Enumerable.Empty<CustomerModel>();
        }

    //############################################################

        public CustomerModel GetCustomerById(int id) {
            return customerRepository?.GetCustomerById(id) ?? new CustomerModel();
        }
        
    //############################################################

        private static bool TryValidateModel(object model, out List<ValidationResult> results)
        {
            var context = new ValidationContext(model);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        }

        public async Task<IResult> AddCustomer(HttpRequest request) {
            if (api == null || request.Body == null) {
                return Results.NoContent();
            }

            CustomerModel requestBody = await request.ReadFromJsonAsync<CustomerModel>() ?? new CustomerModel();
            if (!TryValidateModel(requestBody, out var validationResults)) {
                return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
            }

            customerRepository?.AddCustomer(requestBody);
            return Results.Created($"/customers", requestBody);
        }

    //############################################################

        public async Task<IResult> AddCustomerPersonalInfo(HttpRequest request) {
            if (api == null || request.Body == null) {
                return Results.NoContent();
            }

            CustomerPersonalInfoModel resultBody = await request.ReadFromJsonAsync<CustomerPersonalInfoModel>() ?? new CustomerPersonalInfoModel();
            if (!TryValidateModel(resultBody, out var validationResults)) {
                return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
            }

            customerRepository?.AddCustomerPersonalInfo(resultBody);
            return Results.Created($"/customers/personalinfo", resultBody);
    //############################################################

    }

}