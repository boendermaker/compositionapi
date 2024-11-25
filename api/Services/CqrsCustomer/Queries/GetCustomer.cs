namespace CompositionApi {
    public class GetCustomerQuery {

        private Api? api;
        private CustomerRepository? customerRepository;

        public GetCustomerQuery (Api _api) {
            api = _api;
            customerRepository = api?.repositories?["customer"] as CustomerRepository;
            Init();
        }

    //############################################################

        public void Init() {
            if (api != null && api.appInstance != null) {
                api.appInstance.MapGet("/cqrscustomers/{id}", (int id) => GetCustomerById(id));
            }
        }

    //############################################################

        public CustomerModel GetCustomerById(int id) {
            return customerRepository?.GetCustomerById(id) ?? new CustomerModel();
        }

    //############################################################

    }

}