using Blazored.Toast.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace UI.Components
{
    public static class JsonDeserializer
    {
        public static Task<T> DeserializeValue<T>(string value, IToastService toastService) where T : new()
        {
            T deserializedValue = new T();
            try
            {
                deserializedValue = JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                toastService.ShowError("Nastąpił problem z serializacją danych");
            }
            return Task.FromResult(deserializedValue);
        }
    }
}
