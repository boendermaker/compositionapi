namespace CompositionApi {
    public class Api {
        public WebApplicationBuilder? builderInstance;
        public WebApplication? appInstance;
        public MySqlClient mySqlClient = new MySqlClient();
        public MongoDbClient mongoDbClient = new MongoDbClient();
        public Dictionary<string, object> repositories = new Dictionary<string, object>();
        public Dictionary<string, object> services = new Dictionary<string, object>();
        
        public Api(WebApplicationBuilder builder) {
            builderInstance = builder;
            InitDefaults();
            InitDependencyInjection();
            InitApplication();
            InitRepositories();
            InitServices();
            Run();
        }

        public void InitDependencyInjection() {
            //builderInstance.Services.AddSingleton<MySqlClient>();
            //builderInstance.Services.AddSingleton<BookRepository>();
            //builderInstance.Services.AddSingleton<BookService>();
            /*builderInstance.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new() { Title = "CompositionApi", Version = "v1" });
            });*/
        }
        public void InitApplication() {
            appInstance = builderInstance?.Build();
        }

        public void InitRepositories() {
            repositories.Add("book", new BookRepository(this));
            repositories.Add("customer", new CustomerRepository(this));
        }

        public void InitServices() {
            services.Add("book", new BookService(this));
            services.Add("customer", new CustomerService(this));
            //cqrs services
            services.Add("addCustomerCommand", new AddCustomerCommand(this));
            services.Add("addCustomerPersonalInfoCommand", new AddCustomerPersonalInfoCommand(this));
            services.Add("getAllCustomerQuery", new GetAllCustomersQuery(this));
            services.Add("getCustomerQuery", new GetCustomerQuery(this));
        }

        public void InitDefaults() {
            appInstance?.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));
        }

        public void Run() {
            appInstance?.Run("http://*:8080");
        }

    }

}