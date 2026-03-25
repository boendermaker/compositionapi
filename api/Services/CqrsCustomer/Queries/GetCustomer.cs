namespace CompositionApi
{
    public class GetCustomerQuery
    {

        private readonly Api api;
        private readonly CustomerRepository customerRepository;

        public GetCustomerQuery(CustomerRepository customerRepository, Api api)
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
                api.appInstance.MapGet("/cqrscustomers/{id}", (int id) => GetCustomerById(id));
            }
        }

        //############################################################

        public CustomerModel GetCustomerById(int id)
        {
            return customerRepository?.GetCustomerById(id) ?? new CustomerModel();
        }

        //############################################################

    }

}