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

        [HttpGet("{userId}")]
        public ActionResult<decimal> GetBalance(int userId)
        {
            decimal? balance = accountDAO.GetBalance(userId);

            if (balance != null)
            {

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

        [HttpPut("users/{userId}")]
        public ActionResult<decimal> UpdateBalance(int userId, decimal amountChanged)
        {
            decimal output = accountDAO.UpdateBalance(userId, amountChanged);
            return output;
        }
    }
}
