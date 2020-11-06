using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserDAO userDAO;
        private readonly ITransferDAO transferDAO;
        private readonly IAccountDAO accountDAO;

        public AccountsController(IUserDAO _userDAO, ITransferDAO _transferDAO, IAccountDAO _accountDAO)
        {
            userDAO = _userDAO;
            transferDAO = _transferDAO;
            accountDAO = _accountDAO;
        }

        [HttpGet]
        public ActionResult<decimal> GetBalance(ReturnUser user)
        {

            User user = userDAO.GetUser(user.UserId));
            if (user.UserId > 0)
            {
                decimal balance = accountDAO.GetBalance(user.UserId);
                return balance;

            }
            else return Forbid();

        }

        [HttpGet("users")]
        public List<User> GetUsers()
        {
            List<User> users = userDAO.GetUsers();
            return users; 
        }  
        
        [HttpPost("transfer")]
        public ActionResult<Transfer> TransferToUser(int accountFrom,int accountTo, decimal amount)
        {
            Transfer output = transferDAO.CreateTransfer(accountFrom,accountTo,amount);
            
            return output;
        }
    }
}
