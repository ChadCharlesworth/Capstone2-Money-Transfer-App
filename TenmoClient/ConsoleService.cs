using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TenmoClient.Data;
using TenmoServer.Models;
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
            //UserAccount accountFrom = null;
            //UserAccount accountTo = null;
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
                //int accountToID = int.Parse(Console.ReadLine());
                //Console.WriteLine($"Enter amount: ");
                //decimal amount = decimal.Parse(Console.ReadLine());
                //TransferData transfer = new TransferData();
                //transfer.accountFrom = UserService.GetUserId();
                //transfer.accountTo = accountToID;
                //transfer.amount = amount;
                //accountFrom = accountService.GetAccount(transfer.accountFrom);
                //accountFrom.UserID = transfer.accountFrom;
                //accountFrom.Balance -= amount;
                //accountTo = accountService.GetAccount(transfer.accountTo);
                //accountTo.UserID = transfer.accountTo;
                //accountTo.Balance += amount;
                //accountService.CreateTransfer(transfer);
                //accountService.SubtractBalance(accountFrom);
                //accountService.SubtractBalance(accountTo);
            }
            catch (Exception)
            {

                throw;
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
                List<Transfer> transfers = accountService.AllTransfers(accountService.GetAccount(UserService.GetUserId()).AccountId);
                { 
                    foreach (Transfer transfer in transfers)
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
                Transfer transfers = accountService.GetTransferByTransferID(userSelection);
                {
                    {
                        Console.WriteLine($"ID: " + (transfers.TransferId.ToString())); 
                        Console.WriteLine($"From: " + (accountService.GetUsersFromID(transfers.AccountFrom).Username.ToString()));
                        Console.WriteLine($"To: " + (accountService.GetUsersFromID(transfers.AccountTo).Username.ToString()));
                        Console.WriteLine($"Type: " + (transfers.TransferTypeId.ToString()));
                        Console.WriteLine($"Status: " + (transfers.TransferStatusId.ToString()));
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
