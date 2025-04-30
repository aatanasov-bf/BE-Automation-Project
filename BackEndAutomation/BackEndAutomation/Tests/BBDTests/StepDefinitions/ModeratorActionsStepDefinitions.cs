using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Utilities;
using Reqnroll;
using RestSharp;

namespace BackEndAutomation.Tests.BBDTests.StepDefinitions
{
    [Binding]
    public class ModeratorActionsStepDefinitions
    {
        private const string baseUrl = "https://schoolprojectapi.onrender.com/";
        private RestCalls restCalls = new RestCalls();
        private string classId;
        private Dictionary<string, string> studentMoveDetails = new Dictionary<string, string>();
        private ResponseDataExtractors extractResponseData = new ResponseDataExtractors();
        private readonly ScenarioContext _scenarioContext;

        public ModeratorActionsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When("I enter student move details {string} {string}")]
        public void WhenIEnterStudentMoveDetails(string student_id, string target_class_id)
        {
            studentMoveDetails.Add(student_id, target_class_id);
        }

        [When("I enter delete class details {string}")]
        public void WhenIEnterDeleteClassDetails(string class_id)
        {
            this.classId = class_id;
        }

        [When("I delete the class")]
        public void WhenIDeleteTheClass()
        {
            string token = "Bearer " + _scenarioContext.Get<string>("UserToken");
            RestResponse response = restCalls.DeleteClasstCall(baseUrl, classId, token);

            string classDeleteMessage;
            if (response.IsSuccessStatusCode)
            {
                classDeleteMessage = extractResponseData.ExtractResponseMessage(response.Content);
                _scenarioContext.Add("ClassDeleteMessage", classDeleteMessage);
            }
            else
            {
                classDeleteMessage = extractResponseData.ExtractResponseMessage(response.Content, "detail");
                _scenarioContext.Add("ClassDeleteErrorMessage", classDeleteMessage);
            }

            UtilitiesMethods.LogMessage(message: "Deleting the class is done", scenarioContext: _scenarioContext);
        }

        [When("I move the student")]
        public void WhenIMoveTheStudent()
        {
            string student_id;
            string target_class_id;

            foreach (var smd in studentMoveDetails)
            {
                student_id = smd.Key;
                target_class_id = smd.Value;

                string token = "Bearer " + _scenarioContext.Get<string>("UserToken");
                RestResponse response = restCalls.MoveStudentCall(baseUrl, student_id, target_class_id, token);

                string studentMoveMessage;
                if (response.IsSuccessStatusCode)
                {
                    studentMoveMessage = extractResponseData.ExtractResponseMessage(response.Content);
                    _scenarioContext.Add("StudentMoveMessage", studentMoveMessage);
                }
                else
                {
                    studentMoveMessage = extractResponseData.ExtractResponseMessage(response.Content, "detail");
                    _scenarioContext.Add("StudentMoveErrorMessage", studentMoveMessage);
                }
  
                UtilitiesMethods.LogMessage(message: "Moving the student is done", scenarioContext: _scenarioContext);
            }
        }

        [Then("I get message {string} and the student is moved.")]
        public void ThenIGetMessageAndTheStudentIsMoved(string message)
        {
            Utilities.UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("StudentMoveMessage"), "Student was not moved", _scenarioContext);
        }

        [Then("I get message {string} and the student is not moved.")]
        public void ThenIGetMessageAndTheStudentIsNotMoved(string message)
        {
            Utilities.UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("StudentMoveErrorMessage"), "Student is moved", _scenarioContext);
        }

        [Then("I get message {string} and the class is deleted.")]
        public void ThenIGetMessageAndTheStudentIsDeleted(string message)
        {
            Utilities.UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("ClassDeleteMessage"), "Class was not moved", _scenarioContext);
        }

        [Then("I get message {string} and the class is not deleted.")]
        public void ThenIGetMessageAndTheStudentIsNotDeleted(string message)
        {
            Utilities.UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("ClassDeleteErrorMessage"), "Class is deleted", _scenarioContext);
        }

    }
}
