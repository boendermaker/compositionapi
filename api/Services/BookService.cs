namespace CompositionApi {
    public class BookService {

        private Api? api;
        private BookRepository? bookRepository;

        public BookService (Api _api) {
            api = _api;
            bookRepository = api?.repositories?["book"] as BookRepository;
            Init();
        }

    //############################################################

        public void Init() {
            if (api != null && api.appInstance != null) {
                api.appInstance.MapGet("/books", () => GetAllBooks());
                api.appInstance.MapGet("/books/{id}", (int id) => GetBookById(id));
                api.appInstance.MapPost("/books", (BookModel book) => AddBook(book));
            }
        }

    //############################################################

        public IEnumerable<BookModel> GetAllBooks() {
            return bookRepository?.GetAllBooks() ?? Enumerable.Empty<BookModel>();
        }

    //############################################################

        public BookModel GetBookById(int id) {
            return bookRepository?.GetBookById(id) ?? new BookModel();
        }
        
    //############################################################

        public IResult AddBook(BookModel book) {
            if(api != null && book != null) {
                bookRepository?.AddBook(book);
                return Results.Created($"/books", book);
            }else {
                return Results.NoContent();
            }
        }

    //############################################################

    }

}