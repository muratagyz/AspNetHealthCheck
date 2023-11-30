using HealthCheck.Api.Context;
using HealthCheck.Api.Dtos;
using HealthCheck.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCheck.Api.Services;

public class ProductService : IProductService
{
    private readonly HealthContext _context;

    public ProductService(HealthContext context)
    {
        _context = context;
    }

    public async Task<Response<NoContentDto>> Add(ProductAddDto model)
    {
        await _context.Products.AddAsync(new Product(model.Name, model.Stock, model.Price));
        var response = await _context.SaveChangesAsync();

        if (response > 0)
        {
            return Response<NoContentDto>.Success(200);
        }

        return Response<NoContentDto>.Fail("an error occurred", 500);
    }

    public async Task<Response<NoContentDto>> Delete(int id)
    {
        var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (product is null)
        {
            return Response<NoContentDto>.Fail("product not found", 404);
        }

        _context.Products.Remove(product);
        var response = await _context.SaveChangesAsync();

        if (response > 0)
        {
            return Response<NoContentDto>.Success(200);
        }

        return Response<NoContentDto>.Fail("an error occurred", 500);
    }

    public async Task<Response<List<Product>>> GetAll()
    {
        var products = await _context.Products.ToListAsync();

        return Response<List<Product>>.Success(products, 200);
    }
}