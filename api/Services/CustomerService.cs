namespace CompositionApi {
    public class CustomerService {

        private Api? api;
        private CustomerRepository? customerRepository;

        public CustomerService (Api _api) {
            api = _api;
            customerRepository = api?.repositories?["customer"] as CustomerRepository;
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

        public async Task<IResult> AddCustomer(HttpRequest request) {
            if(api != null && request.Body != null) {
                CustomerModel requestBody = await request.ReadFromJsonAsync<CustomerModel>() ?? new CustomerModel();
                customerRepository?.AddCustomer(requestBody);
                return Results.Created($"/customers/personalinfo", requestBody);
            }else {
                return Results.NoContent();
            }
        }

    //############################################################

        public async Task<IResult> AddCustomerPersonalInfo(HttpRequest request) {
            if(api != null && request.Body != null) {
                CustomerPersonalInfoModel resultBody = await request.ReadFromJsonAsync<CustomerPersonalInfoModel>() ?? new CustomerPersonalInfoModel();
                customerRepository?.AddCustomerPersonalInfo(resultBody);
                return Results.Created($"/customers", resultBody);
            }else {
                return Results.NoContent();
            }
        }

    //############################################################

    }

}