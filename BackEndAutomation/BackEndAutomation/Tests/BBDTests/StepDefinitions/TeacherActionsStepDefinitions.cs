using System;
using System.Reactive.Subjects;
using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Utilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Reqnroll;
using RestSharp;

namespace BackEndAutomation.Tests.BBDTests.StepDefinitions
{
    [Binding]
    public class TeacherActionsStepDefinitions
    {
        private const string baseUrl = "https://schoolprojectapi.onrender.com/";
        private RestCalls restCalls = new RestCalls();
        private Dictionary<string, List<string>> classDetails = new Dictionary<string, List<string>>();
        private Dictionary<string,string> studentDetails = new Dictionary<string, string>();
        private Dictionary<string, Dictionary<string,string>> gradeDetails = new Dictionary<string, Dictionary<string,string>>();
        private ResponseDataExtractors extractResponseData = new ResponseDataExtractors();
        private readonly ScenarioContext _scenarioContext;

        public TeacherActionsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When("I enter class details {string} {string} {string} {string}")]
        public void WhenIEnterClassDetails(string className, string subject1, string subject2, string subject3)
        {
            classDetails.Add(className, new List<string>{subject1,subject2,subject3});
        }

        [When("I create the class")]
        public void WhenICreateTheClass()
        {
            string className;
            string subject1;
            string subject2;
            string subject3;

            foreach (var cl in classDetails)
            {
                className = cl.Key;
                subject1 = cl.Value[0];
                subject2 = cl.Value[1];
                subject3 = cl.Value[2];

                string token = "Bearer " + _scenarioContext.Get<string>("UserToken");
                RestResponse response = restCalls.CreateClassCall(baseUrl, className, subject1, subject2, subject3, token);

                string classAddMessage = extractResponseData.ExtractResponseMessage(response.Content);
                _scenarioContext.Add("ClassAddMessage", classAddMessage);
                string classIdentifier = extractResponseData.ExtractResponseMessage(response.Content, "class_id");
                _scenarioContext.Add("ClassId", classAddMessage);
                UtilitiesMethods.LogMessage(message: "Creation of the class is done", scenarioContext: _scenarioContext);
            }
        }

        [Then("I get message {string} and the class is created")]
        public void ThenIGetMessageAndTheClassIsCreated(string message)
        {
            Utilities.UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("ClassAddMessage"), "Class was not created as expected", _scenarioContext);
        }

        [Then("I get the class identifier")]
        public void ThenIGetTheClassIdentifier()
        {
            Utilities.UtilitiesMethods.AssertNotEmpty(_scenarioContext.Get<string>("ClassId"), "Class Id was not created as expected", _scenarioContext);
        }

        [When("I enter stident details {string} {string}")]
        public void WhenIEnterStudentDetails(string studentName, string classId)
        {
            studentDetails.Add(studentName, classId);
        }

        [When("I add the student to the class")]
        public void WhenIAddStudentToClass()
        {
            string studentName;
            string classId;

            foreach (var student in studentDetails)
            {
                studentName = student.Key;
                classId = student.Value;

                string token = "Bearer " + _scenarioContext.Get<string>("UserToken");
                RestResponse response = restCalls.AddStudentToClassCall(baseUrl, studentName, classId, token);

                string studentAddMessage = extractResponseData.ExtractResponseMessage(response.Content);
                _scenarioContext.Add("StudentAddMessage", studentAddMessage);
                string studentIdentifier = extractResponseData.ExtractResponseMessage(response.Content, "student_id");
                _scenarioContext.Add("StudentId", studentIdentifier);
            }
        }

        [Then("I get message {string} and the student is added")]
        public void ThenIGetMessageAndTheStudentIsAdded(string message)
        {
            Utilities.UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("StudentAddMessage"), "Student was not added", _scenarioContext);
        }

        [Then("I get the student identifier")]
        public void ThenIGetTheStudentIdentifier()
        {
            Utilities.UtilitiesMethods.AssertNotEmpty(_scenarioContext.Get<string>("StudentId"), "Student Id was not created", _scenarioContext);
        }

        [When("I enter new grade details {string} {string} {string}")]
        public void WhenIEnterNewGradeDetails(string student_Id, string subject, string grade)
        {
            gradeDetails.Add(student_Id, new Dictionary<string, string> { { "subject", subject}, {"grade", grade } });
        }

        [When("I add new grade for the student")]
        public void WhenIAddNewGradeForStudent()
        {
            string studentId;
            Dictionary<string, string> subjectGradePair = new Dictionary<string, string>();
            string subject;
            string subjectGrade;

            foreach (var grade in gradeDetails)
            {
                studentId = grade.Key;
                subjectGradePair = grade.Value;
                subject = subjectGradePair["subject"];
                subjectGrade = subjectGradePair["grade"];

                string token = "Bearer " + _scenarioContext.Get<string>("UserToken");
                RestResponse response = restCalls.AddUpdateStudentGrade(baseUrl, studentId, subject, subjectGrade, token);

                string gradeAddMessage = extractResponseData.ExtractResponseMessage(response.Content);
                _scenarioContext.Add("GradeAddMessage", gradeAddMessage);
            }
        }

        [When("I update student grade")]
        public void WhenIUpdateGradeForStudent()
        {
            string studentId;
            Dictionary<string, string> subjectGradePair = new Dictionary<string, string>();
            string subject;
            string subjectGrade;

            foreach (var grade in gradeDetails)
            {
                studentId = grade.Key;
                subjectGradePair = grade.Value;
                subject = subjectGradePair["subject"];
                subjectGrade = subjectGradePair["grade"];

                string token = "Bearer " + _scenarioContext.Get<string>("UserToken");
                RestResponse response = restCalls.AddUpdateStudentGrade(baseUrl, studentId, subject, subjectGrade, token);

                string gradeUpdateMessage = extractResponseData.ExtractResponseMessage(response.Content);
                _scenarioContext.Add("GradeUpdateMessage", gradeUpdateMessage);
            }
        }

        [Then("I get message {string} and the grade is added")]
        public void ThenIGetMessageAndTheGradeIsAdded(string message)
        {
            Utilities.UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("GradeAddMessage"), "Grade was not added", _scenarioContext);
        }

        [Then("I get message {string} and the grade is updated")]
        public void ThenIGetMessageAndTheGradeIsUpdated(string message)
        {
            Utilities.UtilitiesMethods.AssertEqual(message, _scenarioContext.Get<string>("GradeUpdateMessage"), "Grade was not updated", _scenarioContext);
        }
    }
}
