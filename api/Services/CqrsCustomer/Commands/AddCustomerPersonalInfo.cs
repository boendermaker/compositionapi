namespace CompositionApi {
    public class AddCustomerPersonalInfoCommand {

        private Api? api;
        private CustomerRepository? customerRepository;

        public AddCustomerPersonalInfoCommand (Api _api) {
            api = _api;
            customerRepository = api?.repositories?["customer"] as CustomerRepository;
            Init();
        }

    //############################################################

        public void Init() {
            if (api != null && api.appInstance != null) {
                api.appInstance.MapPost("/customers/personalinfo", (HttpRequest request) => AddCustomerPersonalInfo(request));
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