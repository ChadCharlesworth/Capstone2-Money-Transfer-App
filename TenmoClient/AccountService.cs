using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoServer.Models;

namespace TenmoClient
{
    public class AccountService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();
        
        public decimal BalanceAmount()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "api/accounts");
            IRestResponse<decimal> response = client.Get<decimal>(request);

            return response.Data;
        }

        public List<User> GetUsers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "api/accounts/users");
            IRestResponse<List<User>> respone = client.Get<List<User>>(request);
            return respone.Data;
        }

        public Transfer CreateTransfer()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "api/accounts/transfer");
            IRestResponse<Transfer> response = client.Post<Transfer>(request);
            return response.Data;
        }
    }
}
