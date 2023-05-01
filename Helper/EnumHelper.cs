using Newtonsoft.Json;

namespace Agrobot.Helper;


public static class EnumHelper
{
    public static string ArrayToJson(Array array)
    {
        return JsonConvert.SerializeObject(array.Cast<object>().Select(x => x.ToString()));
    }

}