using Microsoft.EntityFrameworkCore;

namespace CodesApp.Infrastructure.Data.Ef
{
    public static class DbInitializer
    {
        public static void Initialize(CodesContext context)
        {
            context.Database.Migrate();
            context.SaveChanges();
        }
    }
}
