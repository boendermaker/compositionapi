namespace CompositionApi
{
    public class Api
    {
        public WebApplicationBuilder? builderInstance;
        public WebApplication? appInstance;
        public static IServiceProvider? serviceProvider { get; private set; }
        public MySqlClient mySqlClient = new MySqlClient();
        public MongoDbClient mongoDbClient = new MongoDbClient();

        public Api(WebApplicationBuilder builder)
        {
            builderInstance = builder;
            serviceProvider = appInstance?.Services;
            InitDependencyInjection();
            InitRepositories();
            InitServices();
            InitApplication();
            InitDefaults();
            Run();
        }

        //########################

        public void InitDependencyInjection()
        {
            builderInstance?.Services.AddSingleton<Api>(this);
            //builderInstance.Services.AddSingleton<MySqlClient>();
            /*builderInstance.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new() { Title = "CompositionApi", Version = "v1" });
            });*/
        }

        //########################

        public void InitRepositories()
        {
            builderInstance?.Services.AddSingleton<BookRepository>();
            builderInstance?.Services.AddSingleton<CustomerRepository>();
        }

        //########################

        public void InitServices()
        {
            builderInstance?.Services.AddSingleton<BookService>();
            builderInstance?.Services.AddSingleton<CustomerService>();
            builderInstance?.Services.AddSingleton<AddCustomerCommand>();
            builderInstance?.Services.AddSingleton<AddCustomerPersonalInfoCommand>();
            builderInstance?.Services.AddSingleton<GetAllCustomersQuery>();
            builderInstance?.Services.AddSingleton<GetCustomerQuery>();
        }

        //########################
        public void InitApplication()
        {
            appInstance = builderInstance?.Build();
        }

        //########################
        public void InitDefaults()
        {
            appInstance?.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));
        }

        //########################

        public void Run()
        {
            appInstance?.Run("http://*:8080");
        }

        //########################

    }

}