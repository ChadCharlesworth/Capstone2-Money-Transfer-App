using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TenmoClient.Data;

using LoginUser = TenmoClient.Data.LoginUser;

namespace TenmoClient
{
    public class ConsoleService
    {
        private static readonly AccountService accountService = new AccountService();
        public class NotEnoughMoneyException { }

        /// <summary>
        /// Prompts for transfer ID to view, approve, or reject
        /// </summary>
        /// <param name="action">String to print in prompt. Expected values are "Approve" or "Reject" or "View"</param>
        /// <returns>ID of transfers to view, approve, or reject</returns>
        public int PromptForTransferID(string action)
        {
            Console.WriteLine("");
            Console.Write("Please enter transfer ID to " + action + " (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int auctionId))
            {
                Console.WriteLine("Invalid input. Only input a number.");
                return 0;
            }
            else
            {
                return auctionId;

            }
        }

        public LoginUser PromptForLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            string password = GetPasswordFromConsole("Password: ");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }

        public void PrintOutAllUsers()
        {
            
            try
            {
                List<API_User> users = accountService.GetUsers();
                Console.WriteLine($"----------------------");
                Console.WriteLine($"Users");
                Console.WriteLine($"ID          Name");
                Console.WriteLine($"----------------------");
                foreach (API_User user in users)
                {
                    Console.WriteLine($"{user.UserId}           {user.Username}");
                }
                Console.WriteLine($"----------------------");
                Console.WriteLine($"Enter ID of user you are sending to");
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SendTEBucks(int accountToID, decimal amount)
        {
            try
            {
                TransferData transfer = new TransferData();
                transfer.AccountFrom = UserService.GetUserId();
                transfer.AccountTo = accountToID;
                transfer.Amount = amount;
                UserAccount accountFrom = accountService.GetAccount(transfer.AccountFrom);
                accountFrom.UserID = transfer.AccountFrom;
                UserAccount accountTo = accountService.GetAccount(transfer.AccountTo);
                accountTo.UserID = transfer.AccountTo;
                if (transfer.Amount <= accountFrom.Balance)
                {
                    accountTo.Balance += amount;
                    accountFrom.Balance -= amount;
                    accountService.CreateTransfer(transfer);
                    accountService.UpdateBalance(accountFrom);
                    accountService.UpdateBalance(accountTo);
                }
                else throw new Exception("There are not enough funds in your account.");
            }
            catch (Exception e)
            {
                
            }
            
        }
        public void ShowTransfers()
        {
            try
            {
                Console.WriteLine($"-------------------------------------------------------------");
                Console.WriteLine($"Transfers");
                Console.WriteLine($"ID".PadRight(10) + "From/To".PadRight(25) + "Amount".PadRight(10));
                Console.WriteLine($"-------------------------------------------------------------");
                List<TransferData> transfers = accountService.AllTransfers(accountService.GetAccount(UserService.GetUserId()).AccountId);
                { 
                    foreach (TransferData transfer in transfers)
                    {
                        if (transfer.AccountTo == accountService.GetAccount(UserService.GetUserId()).AccountId)
                        {
                            Console.WriteLine((transfer.TransferId.ToString().PadRight(10)) + "From: " + (accountService.GetUsersFromID(transfer.AccountFrom).Username.ToString().PadRight(20)) + transfer.Amount.ToString("C").PadRight(10));
                        }
                        else if (transfer.AccountFrom == UserService.GetUserId())
                        {
                            Console.WriteLine((transfer.TransferId.ToString().PadRight(10)) + "To:   " + (accountService.GetUsersFromID(transfer.AccountTo).Username.ToString().PadRight(20)) + transfer.Amount.ToString("C").PadRight(10));
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void TransferDetails(int userSelection)
        {
            try
            {
                Console.WriteLine($"-------------------------------------------------------------");
                Console.WriteLine($"Transfer Details ");
                Console.WriteLine($"-------------------------------------------------------------");
                TransferData transfers = accountService.GetTransferByTransferID(userSelection);
                {
                    {
                        Console.WriteLine($"ID: " + (transfers.TransferId.ToString())); 
                        Console.WriteLine($"From: " + (accountService.GetUsersFromID(transfers.AccountFrom).Username.ToString()));
                        Console.WriteLine($"To: " + (accountService.GetUsersFromID(transfers.AccountTo).Username.ToString()));
                        if(transfers.TransferTypeId == 1)
                        {
                            Console.WriteLine($"Type: Request");
                        } else if(transfers.TransferTypeId == 2)
                        {
                            Console.WriteLine($"Type: Send");
                        }
                        if(transfers.TransferStatusId == 1)
                        {
                            Console.WriteLine($"Status: Pending");
                        }
                        else if (transfers.TransferStatusId == 2)
                        {
                            Console.WriteLine($"Status: Approved");
                        }
                        else if(transfers.TransferStatusId == 3)
                        {
                            Console.WriteLine($"Status: Rejected");
                        }
                        Console.WriteLine($"Amount: " + (transfers.Amount.ToString("C"))); 
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
