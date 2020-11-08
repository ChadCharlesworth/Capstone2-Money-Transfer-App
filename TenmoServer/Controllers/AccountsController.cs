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
    [Route("[controller]")]
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

        [HttpGet("{userId}/balance")]
        public ActionResult<decimal> GetBalance(int userId)
        {
            Account account = accountDAO.GetAccount(userId);

            if (account.Balance != 0)
            {

                return account.Balance;

            }
            else return Forbid();

        }

        [HttpGet("users")]
        public List<User> GetUsers()
        {
            List<User> users = userDAO.GetUsers();
            return users;
        }
        [HttpGet("{userId}")]
        public Account GetAccount(int userId)
        {
            Account account = accountDAO.GetAccount(userId);
            return account;
        }

        [HttpGet("transfers/{userId}")]
        public IList<Transfer> GetTransfers(int userId)
        {
            IList<Transfer> transfers = transferDAO.GetTransfers(userId);
            return transfers;
        }

        [HttpPost("sendtransfers")]
        public ActionResult<Transfer> TransferToUser(Transfer transfer)
        {
            Transfer output = transferDAO.CreateTransfer(transfer);

            return output;
        }

        [HttpPut("users/{account.UserID}")]
        public ActionResult<bool> SubtractBalance(Account account)
        {

            bool worked = accountDAO.SubtractBalance(account);
            return worked;
        }

        [HttpGet("{userID}/users")]
        public ActionResult<User> GetSome(int userId)
        {
            User output = userDAO.GetUserFromID(userId);
            return output; 
        }

        [HttpPost]
        public ActionResult<Account> CreateAccount(Account account)
        {
            bool accountCreated = accountDAO.CreateAccount(account);
            return Ok(accountCreated);
        }
    }
}
