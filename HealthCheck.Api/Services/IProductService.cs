using HealthCheck.Api.Dtos;
using HealthCheck.Api.Models;

namespace HealthCheck.Api.Services;

public interface IProductService
{
    Task<Response<NoContentDto>> Add(ProductAddDto model);
    Task<Response<List<Product>>> GetAll();
    Task<Response<NoContentDto>> Delete(int id);
}
