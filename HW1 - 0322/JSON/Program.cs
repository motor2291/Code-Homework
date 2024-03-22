using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


class Program
{

    static void Main(string[] args)
    {
        string json = File.ReadAllText("D:/Project/JSON/IterateParseJSON.json");

        var stack = new Stack<KeyValuePair<string, JToken>>();
        stack.Push(new KeyValuePair<string, JToken>("", JObject.Parse(json)));

        while (stack.Count > 0)
        {
            var (parentKey, token) = stack.Pop();

            //用來印出widget、window、images、text
            if (token.Type != JTokenType.String && token.Type != JTokenType.Integer)
            {
                if (token.Parent != null)
                {
                    if (token.Parent.Type != JTokenType.Array)
                    {
                        Console.WriteLine($"{parentKey}");
                    }
                }
            }

            //印出其餘的Key-Value值
            if (token.Type == JTokenType.Object)
            {
                var obj = (JObject)token;
                foreach (var prop in obj.Properties().Reverse())
                {
                    stack.Push(new KeyValuePair<string, JToken>(prop.Name, prop.Value));
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                var arr = (JArray)token;
                for (int i = arr.Count - 1; i >= 0; i--)
                {
                    stack.Push(new KeyValuePair<string, JToken>($"[{i}]", arr[i]));
                }
            }
            else
            {
                Console.WriteLine($"{parentKey}: {token}");
            }
        }
    }
}