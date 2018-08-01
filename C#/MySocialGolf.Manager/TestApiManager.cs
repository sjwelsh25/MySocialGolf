using System.Linq;
using MySocialGolf.DataModel;
using MySocialGolf.DataManager;
using MySocialGolf.Manager.RESTful;
using MySocialGolf.Model;
using Newtonsoft.Json;
using MySocialGolf.Extensions;

namespace MySocialGolf.Manager
{
    public class TestApiManager
    {
        public TestApiDataModel TestApi(int testApiID)
        {
            TestApiDataManager taDtoMngr = new TestApiDataManager();
            TestApiDataModel taDto = taDtoMngr.ListTestApi(testApiID).FirstOrDefault<TestApiDataModel>();

            RestCallModel rcModel = new RestCallModel();
            rcModel.Url = taDto.TestUrl;
            rcModel.HttpVerb = taDto.HttpVerb;
            rcModel.InputJson = taDto.TestInputJson;
            RestAPIManager rMngr = new RestAPIManager();
            rMngr.TestRestApi(rcModel);

            // Log this response
            taDto.ResponseToLog = rcModel.ResponseMessage;
            taDto.LastStatusCode = rcModel.StatusCode.ToString();
            taDtoMngr.AddTestAPILog(taDto);
            taDto = taDtoMngr.ListTestApi(testApiID).First<TestApiDataModel>();

            // Return an appropriate test message to the client web page
            if (rcModel.StatusCode == "200")
            {
                // hack to return a list count. So we don't return huge response lists to test api page
                string message = $"{taDto.LastLogDateTime} Server Side {taDto.HttpVerb} Test {testApiID} Successful. Response: {rcModel.StatusCode} {rcModel.ResponseMessage}";
                if (taDto.ReturnType.IEqualsNullSafe("ListCount"))
                {
                    dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(rcModel.ResponseMessage);
                    message = $"{taDto.LastLogDateTime} Server Side {taDto.HttpVerb} Test {testApiID} Successful. Response: {rcModel.StatusCode} Returned {jsonData.Count} record(s)";
                }
                taDto.TestAPIMessage = message;
            }
            else
            {
                taDto.TestAPIMessage = $"{taDto.LastLogDateTime} Server Side {taDto.HttpVerb} Test {testApiID} Failed. Response: {rcModel.StatusCode} {rcModel.ResponseMessage}"; 
            }
            

            return taDto;

        }
    }
}
