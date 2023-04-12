using Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace Application.Data
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
    }
}
