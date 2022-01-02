using System.Threading.Tasks;

namespace UI.Services.Interfaces
{
    public interface IHttpService
    {
        Task<T> Delete<T>(string uri, object value);
        Task<T> Get<T>(string uri);
        Task<T> Patch<T>(string uri, object value);
        Task<T> Post<T>(string uri, object value);

        Task<T> Put<T>(string uri, object value);
    }
}
