using System.Threading.Tasks;

namespace HealthBridgeClinical.Common.Rest
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string url);
    }
}
