using Newtonsoft.Json.Linq;

class Program
{
    static void Main(string[] args)
    {
        string json = @"
            [
             {
             ""Name"": ""Jose Luis"",
             ""Channel"": ""ParametricCamp"",
             ""Active"": true
             }
            ]";

        var jsonArray = JArray.Parse(json);

        ParseJson(jsonArray);
    }

    static void ParseJson(JToken token)
    {
        if (token.Type == JTokenType.Object)
        {
            ParseJsonObject((JObject)token);
        }
        else if (token.Type == JTokenType.Array)
        {
            ParseJsonArray((JArray)token);
        }
        else
        {
            Console.WriteLine(token.ToString());
        }
    }

    static void ParseJsonObject(JObject jsonObject)
    {
        foreach (var property in jsonObject.Properties())
        {
            Console.WriteLine($"Property: {property.Name}, Value: {property.Value}");
            ParseJson(property.Value);
        }
    }

    static void ParseJsonArray(JArray jsonArray)
    {
        foreach (var item in jsonArray)
        {
            ParseJson(item);
        }
    }
}