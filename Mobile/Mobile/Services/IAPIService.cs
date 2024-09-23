using Mobile.Models;
using System.Threading.Tasks;

namespace Mobile.Services;

public interface IAPIService
{
    Task<Response> GetListAsync<T>(string urBase, string servicePrefix, string controller);
}

