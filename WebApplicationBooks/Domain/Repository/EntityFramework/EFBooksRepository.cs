using WebApplicationBooks.Domain.Repository.Entity;

namespace WebApplicationBooks.Domain.Repository.EntityFramework
{
    public class EFBooksRepository
    {
        private readonly AppDbContext context;
        public EFBooksRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddBooksAsync(Book book)
        {
            
        }

        public async Task GetBookByUserId()
        {

        }


        public async Task DeleteBooksAsync(Book book)
        {

        }
    }
}
