using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomProjectRPG.Core.Utilities
{

    public static class SerializeJSON
    {
        public static string ToJson<T>(T data)
        {
            return JsonSerializer.Serialize(data);
        }

        public static T FromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }

}
