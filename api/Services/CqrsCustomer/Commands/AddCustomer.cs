namespace CompositionApi {
    public class AddCustomerCommand {

        private Api? api;
        private CustomerRepository? customerRepository;

        public AddCustomerCommand (Api _api) {
            api = _api;
            customerRepository = api?.repositories?["customer"] as CustomerRepository;
            Init();
        }

    //############################################################

        public void Init() {
            if (api != null && api.appInstance != null) {
                api.appInstance.MapPost("/cqrscustomers", (HttpRequest request) => AddCustomer(request));
            }
        }

    //############################################################

        public async Task<IResult> AddCustomer(HttpRequest request) {
            if(api != null && request.Body != null) {
                CustomerModel requestBody = await request.ReadFromJsonAsync<CustomerModel>() ?? new CustomerModel();
                customerRepository?.AddCustomer(requestBody);
                return Results.Created($"/cqrscustomers", requestBody);
            }else {
                return Results.NoContent();
            }
        }

    //############################################################

    }

}