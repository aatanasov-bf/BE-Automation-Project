using AventStack.ExtentReports;
using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Utilities;
using Reqnroll;
using RestSharp;

namespace BackEndAutomation.Tests.BBDTests.StepDefinitions
{
    [Binding]
    public class AdminActionsStepDefinitions
    {
        private const string baseUrl = "https://schoolprojectapi.onrender.com/";
        private RestCalls restCalls = new RestCalls();
        private Dictionary<string, Dictionary<string, string>> userDetails = new Dictionary<string, Dictionary<string, string>>();
        private ResponseDataExtractors extractResponseData = new ResponseDataExtractors();
        private readonly ScenarioContext _scenarioContext;

        public AdminActionsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When("I create the user")]
        public void WhenICreateTheUser()
        {
            string username;
            Dictionary<string, string> passwordRolePair = new Dictionary<string, string>();
            string password;
            string role;

            foreach (var user in userDetails)
            {
                username = user.Key;
                passwordRolePair = user.Value;
                password = passwordRolePair["password"];
                role = passwordRolePair["role"];

                string token = "Bearer " + _scenarioContext.Get<string>("UserToken");
                RestResponse response = restCalls.CreateUserCall(baseUrl, username, password, role, token);

                string userAddMessage;

                if(role.Equals("parent")|| role.Equals("teacher")|| role.Equals("moderator"))
                    userAddMessage = extractResponseData.ExtractResponseMessage(response.Content);
                else
                    userAddMessage = extractResponseData.ExtractResponseMessage(response.Content, "detail");

                _scenarioContext.Add("UserAddMessge", userAddMessage);
                UtilitiesMethods.LogMessage(message: "Creation of the user is done", scenarioContext: _scenarioContext);
            }
        }

        [When("I enter user details {string} {string} {string}")]
        public void WhenIEnterUsertDetails(string username, string password, string role)
        {
            userDetails.Add(username, new Dictionary<string, string> { { "password", password }, { "role", role } });
        }

        [Then("I get message {string} and the user is not created")]
        public void ThenIGetMessageAndTheUserIsNotCreated(string message)
        {
            Utilities.UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("UserAddMessge"), "User was not created as expected", _scenarioContext);
        }

        [Then("I get message {string} and the user is created")]
        public void ThenIGetMessageAndTheUserIsCreated(string message)
        {
            Utilities.UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("UserAddMessge"), "User was not created as expected", _scenarioContext);
        }

        [Then("{string} is created")]
        public void ThenIsCreated(string teacher)
        {
            throw new PendingStepException();
        }

    }
}
