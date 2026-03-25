using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompositionApi
{
    public class GetAllCustomersQuery
    {

        private readonly Api api;
        private readonly CustomerRepository customerRepository;

        public GetAllCustomersQuery(CustomerRepository customerRepository, Api api)
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
                api.appInstance.MapGet("/cqrscustomers", () => GetAllCustomers());
            }
        }

        //############################################################

        public List<GetAllCustomersModel> GetAllCustomers()
        {
            return customerRepository?.GetAllCustomersQuery<GetAllCustomersModel>()
            .Select(doc => doc).ToList() ?? new List<GetAllCustomersModel>();
        }


        //############################################################

    }

}