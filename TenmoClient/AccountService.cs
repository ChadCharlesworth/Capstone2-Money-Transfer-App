using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TenmoClient.Data;


namespace TenmoClient
{
    public class AccountService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/Accounts";
        private readonly IRestClient client = new RestClient();
        
        public decimal GetBalance(int userId)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_BASE_URL + $"/{userId}/balance");
            IRestResponse<decimal> response = client.Get<decimal>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response); 
            }
            else
            {
                return response.Data;
            }
            
            return Convert.ToDecimal(null); 
        }

        public List<API_User> GetUsers()
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_BASE_URL + "/users");
            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            
            return null;
        }

        public TransferData CreateTransfer(TransferData transfer)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_BASE_URL + "/transfers");
            request.AddJsonBody(transfer); 
            IRestResponse<TransferData> response = client.Post<TransferData>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }

            return null;
        }

        public List<TransferData> GetTransfersOfUser(UserAccount account)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_BASE_URL + $"/transfers/{account.UserID}");
            IRestResponse<List<TransferData>> response = client.Get<List<TransferData>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }

            return null;
        }

        public decimal UpdateBalance(UserAccount account)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_BASE_URL + $"/users/{account.UserID}");
                request.AddJsonBody(account);
                IRestResponse<decimal> response = client.Put<decimal>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }

            return Convert.ToDecimal(null);
        }

        public UserAccount GetAccount(int userId)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
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

        public List<TransferData> AllTransfers(int userId)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_BASE_URL + $"/transfers/{userId}");
            IRestResponse<List<TransferData>> response = client.Get<List<TransferData>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response); 
            }
            else
            {
                return response.Data;
            }

            return null; 
        }

        public API_User GetUsersFromID(int userId)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_BASE_URL + $"/{userId}/users");
            IRestResponse<API_User> response = client.Get<API_User>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }

            return null;
        }

        public UserAccount CreateAccount(UserAccount account)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_BASE_URL);
            request.AddJsonBody(account);
            IRestResponse<UserAccount> response = client.Post<UserAccount>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }

            return null;
        }
        public TransferData GetTransferByTransferID(int transferId)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_BASE_URL + $"/transfer/{transferId}");
            IRestResponse<TransferData> response = client.Get<TransferData>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }

            return null;
        }

        private void ProcessErrorResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("Error occurred - unable to reach server."); 
            }
            else if (!response.IsSuccessful)
            {
                Console.WriteLine("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
        }
    }
}
