using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using Reqnroll;
using RestSharp;

namespace BackEndAutomation.Tests.OnlineStore.StepDefinitions
{
    [Binding]
    public class UserActionsStepDefinitions

    {
        private const string baseUrl = "https://schoolprojectapi.onrender.com/";
        private RestCalls restCalls = new RestCalls();
        private ResponseDataExtractors extractResponseData = new ResponseDataExtractors();
        private readonly ScenarioContext _scenarioContext;

        public UserActionsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [StepDefinition("I login with (.*) and (.*)")]
        public void GivenIEnterUsernameAndPassword(string username, string password)
        {
            RestResponse response = restCalls.LoginCall(baseUrl, username, password);

            string tokenValue = extractResponseData.ExtractLoggedInUserToken(response.Content, "access_token");
            _scenarioContext.Add("UserToken", tokenValue);
        }

        [Then("I get bearer token")]
        public void ThenIGetBearerToken()
        {
            bool isTokenExtracted = string.IsNullOrEmpty(_scenarioContext.Get<string>("UserToken"));
            Utilities.UtilitiesMethods.AssertEqual(
                false,
                isTokenExtracted,
                "Token is not extracted or user is not logged in",
                _scenarioContext);
        }
    }
}
