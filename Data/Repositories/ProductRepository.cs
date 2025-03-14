﻿using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;

namespace Data.Repositories;

public class ProductRepository(DataContext context) : BaseRepository<ProductEntity>(context) , IProductRepository
{
    private readonly DataContext _context = context;
}
