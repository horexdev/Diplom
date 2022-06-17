using Newtonsoft.Json;

namespace EmployeeAccounting;

public struct UploadData
{
    public UploadData(string name, object[] args)
    {
        Name = name;
        Args = args;
    }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("args")]
    public object[] Args { get; set; }
}