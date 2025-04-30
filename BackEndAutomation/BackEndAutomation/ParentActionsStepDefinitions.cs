using System;
using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Utilities;
using Reqnroll;
using RestSharp;

namespace BackEndAutomation
{
    [Binding]
    public class ParentActionsStepDefinitions
    {
        private const string baseUrl = "https://schoolprojectapi.onrender.com/";
        private RestCalls restCalls = new RestCalls();
        string student_id;
        private ResponseDataExtractors extractResponseData = new ResponseDataExtractors();
        private readonly ScenarioContext _scenarioContext;

        public ParentActionsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When("I enter child's details {string}")]
        public void WhenIEnterChildDetails(string student_id)
        {
            this.student_id = student_id;
        }

        [When("I open the grades")]
        public void WhenIOpenTheGrades()
        {
            string token = "Bearer " + _scenarioContext.Get<string>("UserToken");
            RestResponse response = restCalls.ViewGradesCall(baseUrl, student_id, token);

            string viewGradesMessage;
            if (response.IsSuccessStatusCode)
            {
                viewGradesMessage = extractResponseData.ExtractResponseMessage(response.Content, "grades");
                _scenarioContext.Add("ViewGradesMessage", viewGradesMessage);
            }
            else
            {
                viewGradesMessage = extractResponseData.ExtractResponseMessage(response.Content, "detail");
                _scenarioContext.Add("ViewGradesErrorMessage", viewGradesMessage);
            }

            UtilitiesMethods.LogMessage(message: "Viweing the grades is done", scenarioContext: _scenarioContext);
        }

        [Then("I get message {string} and the grades are shown.")]
        public void ThenIGetMessageAndTheStudentIsMoved(string message)
        {
            UtilitiesMethods.AssertNotEmpty(_scenarioContext.Get<string>("ViewGradesMessage"), "Grades were not shown", _scenarioContext);
        }

        [Then("I get message {string} and the grades are not shown.")]
        public void ThenIGetMessageAndTheStudentIsNotMoved(string message)
        {
            UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("ViewGradesErrorMessage"), "Grades were shown", _scenarioContext);
        }

    }
}
