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
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();
        
        public decimal BalanceAmount()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "api/accounts");
            IRestResponse<decimal> response = client.Get<decimal>(request);

            return response.Data;
        }

        public List<API_User> GetUsers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "api/accounts/users");
            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);
            return response.Data;
        }

        public Transfer CreateTransfer(TransferData transfer)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "api/accounts/transfer");
            request.AddJsonBody(transfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);
            return response.Data;
        }
    }
}
