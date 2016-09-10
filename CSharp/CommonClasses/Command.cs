using Newtonsoft.Json;

namespace CSharp.CommonClasses
{
    internal class Command 
    {
        [JsonProperty("command")]
        internal string CommandType {get; set; }

        [JsonProperty("params")]
        internal dynamic CommandParams { get; set; }
    }
}
