using Newtonsoft.Json.Linq;

namespace BackEndAutomation.Rest.DataManagement
{
    public class ResponseDataExtractors
    {

        public string ExtractLoggedInUserToken(string jsonResponse, string tokenType = "token")
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject[tokenType]?.ToString();
        }

        public int ExtractUserId(string jsonResponse)
        {
            var jsonObject = JObject.Parse(jsonResponse);
            return jsonObject["user"]?["id"]?.Value<int>() ?? 0;
        }

        public string ExtractResponseMessage(string jsonResponse, string messageProperty = "message")
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject[messageProperty]?.ToString();
        }
    }
}
