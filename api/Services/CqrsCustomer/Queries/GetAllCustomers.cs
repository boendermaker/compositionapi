using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompositionApi {
    public class GetAllCustomersQuery {

        private Api? api;
        private CustomerRepository? customerRepository;

        public GetAllCustomersQuery (Api _api) {
            api = _api;
            customerRepository = api?.repositories?["customer"] as CustomerRepository;
            Init();
        }

    //############################################################

        public void Init() {
            if (api != null && api.appInstance != null) {
                api.appInstance.MapGet("/cqrscustomers", () => GetAllCustomers());
            }
        }

    //############################################################

        public List<GetAllCustomersModel> GetAllCustomers() {
            return customerRepository?.GetAllCustomersQuery<GetAllCustomersModel>()
            .Select(doc => doc).ToList() ?? new List<GetAllCustomersModel>();
        }


    //############################################################

    }

}