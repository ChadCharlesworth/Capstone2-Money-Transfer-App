﻿using RestSharp;
using System;
using System.Collections.Generic;
using TenmoClient.Data;


namespace TenmoClient
{
    public class ConsoleService
    {
        private static readonly AccountService accountService = new AccountService();

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
            List<API_User> users = accountService.GetUsers();
            Console.WriteLine($"----------------------");
            Console.WriteLine($"Users");
            Console.WriteLine($"ID           Name");
            Console.WriteLine($"----------------------");
            foreach (API_User user in users)
            {
                Console.WriteLine($"{user.UserId}           {user.Username}");
            }
            Console.WriteLine($"-----------------");
            Console.WriteLine($"Enter ID of user you are sending to");
            int accountTo = int.Parse(Console.ReadLine());
            Console.WriteLine($"Enter amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            TransferData transfer = new TransferData();
            transfer.accountFrom = 
            transfer.accountTo = accountTo;
            transfer.amount = amount;
            accountService.CreateTransfer(transfer);

        }


    }
}