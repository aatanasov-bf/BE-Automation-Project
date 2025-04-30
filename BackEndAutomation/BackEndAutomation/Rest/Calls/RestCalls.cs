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
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };

            RestClient client = new RestClient(options);
            string endpoint = $"users/create?username={username}&password={password}&role={role}";
            RestRequest request = new RestRequest(baseUrl+endpoint, Method.Post);
            request.AddHeader("Authorization", token);
            RestResponse response = client.Execute(request);

            return response;
        }

        public RestResponse CreateClassCall(string baseUrl, string className, string subject1, string subject2, string subject3, string token)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };

            RestClient client = new RestClient(options);
            string endpoint = $"classes/create?class_name={className}&subject_1={subject1}&subject_2={subject2}&subject_3={subject3}";
            RestRequest request = new RestRequest(baseUrl + endpoint, Method.Post);
            request.AddHeader("Authorization", token);
            RestResponse response = client.Execute(request);

            return response;
        }

        public RestResponse AddStudentToClassCall(string baseUrl, string studentName, string classId, string token)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };

            RestClient client = new RestClient(options);
            string endpoint = $"classes/add_student?name={studentName}&class_id={classId}";
            RestRequest request = new RestRequest(baseUrl + endpoint, Method.Post);
            request.AddHeader("Authorization", token);
            RestResponse response = client.Execute(request);

            return response;
        }

        public RestResponse AddUpdateStudentGrade(string baseUrl, string studentId, string subject, string grade, string token)
        {
            int.TryParse(grade, out int intGrade);
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };

            RestClient client = new RestClient(options);
            string endpoint = $"grades/add?student_id={studentId}&subject={subject}&grade={intGrade}";
            RestRequest request = new RestRequest(baseUrl + endpoint, Method.Put);
            request.AddHeader("Authorization", token);
            RestResponse response = client.Execute(request);

            return response;
        }

        public RestResponse MoveStudentCall(string baseUrl, string student_id, string target_class_id, string token)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };

            RestClient client = new RestClient(options);
            string endpoint = $"classes/move_student?student_id={student_id}&target_class_id={target_class_id}";
            RestRequest request = new RestRequest(baseUrl + endpoint, Method.Put);
            request.AddHeader("Authorization", token);
            RestResponse response = client.Execute(request);

            return response;
        }

        public RestResponse DeleteClasstCall(string baseUrl, string class_id, string token)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };

            RestClient client = new RestClient(options);
            string endpoint = $"classes/delete_class_if_empty/{class_id}";
            RestRequest request = new RestRequest(baseUrl + endpoint, Method.Delete);
            request.AddHeader("Authorization", token);
            RestResponse response = client.Execute(request);

            return response;
        }


    }
}
