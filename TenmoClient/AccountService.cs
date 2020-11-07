using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;
using TenmoServer.Models;

namespace TenmoClient
{
    public class AccountService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/Accounts";
        private readonly IRestClient client = new RestClient();
        
        public decimal GetBalance(int userId)
        {
            RestRequest request = new RestRequest(API_BASE_URL + $"/{userId}");
            IRestResponse<decimal> response = client.Get<decimal>(request);

            return response.Data;
        }

        public List<API_User> GetUsers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "/users");
            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);
            return response.Data;
        }

        public Transfer CreateTransfer(TransferData transfer)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "/transfer");
            request.AddJsonBody(transfer); 
            IRestResponse<Transfer> response = client.Post<Transfer>(request);  //Respone String is not JSON formatted
            return response.Data;
        }

        public List<Transfer> GetTransfersOfUser()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "/transfers/{userId}");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);
            return response.Data;
        }

        public decimal UpdateBalance(int userId)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "/balance");
            request.AddJsonBody(userId); 
            IRestResponse<decimal> response = client.Put<decimal>(request);
            return response.Data; 
        }
    }
}
