using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MySqlConnector;

namespace CompositionApi {
    public class CustomerRepository {

        private Api api;
        public CustomerRepository(Api _api) {
            api = _api;
        }

    //############################################################

        public IQueryable<T> GetAllCustomersQuery<T>() {
            return api.mongoDbClient.QueryCollection<T>("apidemo", "customers");
        }

    //############################################################
        public List<CustomerModel> GetAllCustomers() {
            return api.mongoDbClient.QueryCollection<CustomerModel>("apidemo", "customers")
                .Select(doc => doc)
                .ToList();
        }

    //############################################################

        public CustomerModel GetCustomerById(int id) {
            return new CustomerModel();
        }

    //############################################################

        public void AddCustomer(CustomerModel customer) {
            if(customer != null) {
                api.mongoDbClient.InsertDocument("apidemo", "customers", customer);
            }else {
                Results.NoContent();
            }
        }

    //############################################################
        public void AddCustomerPersonalInfo(CustomerPersonalInfoModel customerPersonalInfo) {
            if(customerPersonalInfo != null) {
                api.mongoDbClient.InsertDocument("apidemo", "customers", customerPersonalInfo);
            }else {
                Results.NoContent();
            }
        }

    //############################################################

    }

}