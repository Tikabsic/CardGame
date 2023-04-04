﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace Application.Data
{
    public interface IAppDbContext
    {
        DbSet<User> User { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
