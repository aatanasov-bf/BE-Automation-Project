using RestSharp;

namespace BackEndAutomation.Rest.Calls
{
    public class RestCalls
    {

        public RestResponse generalRestCall(string baseUrl, string endpoint, Method method)
        {

            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest(endpoint, method);
            RestResponse response = client.Execute(request);

            return response;
        }

        public RestResponse generalRestCall(string baseUrl, string endpoint, Method method, string token)
        {

            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest(endpoint, method);
            request.AddHeader("Authorization", token);
            RestResponse response = client.Execute(request);

            return response;
        }

        public RestResponse LoginCall(string baseUrl, string username, string password)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };

            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest($"{baseUrl}auth/login", Method.Post);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            RestResponse response = client.Execute(request);

            return response;
        }
        public RestResponse CreateUserCall(string baseUrl, string username, string password, string role, string token)
        {
            string endpoint = $"users/create?username={username}&password={password}&role={role}";
            RestResponse response = this.generalRestCall(baseUrl, endpoint, Method.Post, token);

            return response;
        }

        public RestResponse CreateClassCall(string baseUrl, string className, string subject1, string subject2, string subject3, string token)
        {
            string endpoint = $"classes/create?class_name={className}&subject_1={subject1}&subject_2={subject2}&subject_3={subject3}";
            RestResponse response = this.generalRestCall(baseUrl, endpoint, Method.Post, token);

            return response;
        }

        public RestResponse AddStudentToClassCall(string baseUrl, string studentName, string classId, string token)
        {
            string endpoint = $"classes/add_student?name={studentName}&class_id={classId}";
            RestResponse response = this.generalRestCall(baseUrl, endpoint, Method.Post, token);

            return response;
        }

        public RestResponse AddUpdateStudentGrade(string baseUrl, string studentId, string subject, string grade, string token)
        {
            int.TryParse(grade, out int intGrade);
            string endpoint = $"grades/add?student_id={studentId}&subject={subject}&grade={intGrade}";
            RestResponse response = this.generalRestCall(baseUrl, endpoint, Method.Put, token);

            return response;
        }

        public RestResponse MoveStudentCall(string baseUrl, string student_id, string target_class_id, string token)
        {
            string endpoint = $"classes/move_student?student_id={student_id}&target_class_id={target_class_id}";
            RestResponse response = this.generalRestCall(baseUrl, endpoint, Method.Put, token);

            return response;
        }

        public RestResponse DeleteClasstCall(string baseUrl, string class_id, string token)
        {
            string endpoint = $"classes/delete_class_if_empty/{class_id}";
            RestResponse response = this.generalRestCall(baseUrl, endpoint, Method.Delete, token);

            return response;
        }

        public RestResponse ViewGradesCall(string baseUrl, string student_id, string token)
        {
            string endpoint = $"grades/student/{student_id}";
            RestResponse response = this.generalRestCall(baseUrl, endpoint, Method.Get, token);

            return response;
        }

        public RestResponse ConnectParentToStudentCall(string baseUrl, string parent_username,string student_id, string token)
        {
            string endpoint = $"users/connect_parent?parent_username={parent_username}&student_id={student_id}";
            RestResponse response = this.generalRestCall(baseUrl, endpoint, Method.Get, token);

            return response;
        }
    }
}
