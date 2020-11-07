using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            RestRequest request = new RestRequest(API_BASE_URL + $"/{userId}/balance");
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
            RestRequest request = new RestRequest(API_BASE_URL + "/sendtransfers");
            request.AddJsonBody(transfer); 
            IRestResponse<Transfer> response = client.Post<Transfer>(request);  
            return response.Data;
        }

        public List<Transfer> GetTransfersOfUser(UserAccount account)
        {
            RestRequest request = new RestRequest(API_BASE_URL + $"/transfers/{account.UserID}");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);
            return response.Data;
        }

        public decimal SubtractBalance(UserAccount account)
        {
                RestRequest request = new RestRequest(API_BASE_URL + $"users/{account.UserID}");
                request.AddJsonBody(account);
                IRestResponse<decimal> response = client.Put<decimal>(request);
                return response.Data;
        }

        public UserAccount GetAccount(int userId)
        {
            RestRequest request = new RestRequest(API_BASE_URL + $"/{userId}");
            IRestResponse<UserAccount> response = client.Get<UserAccount>(request);
            if(response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new HttpRequestException();
            }
            else if (!response.IsSuccessful)
            {
                throw new HttpRequestException();
            }
            return response.Data;
        }
    }
}
