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
    [Authorize]
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

                return Ok(account.Balance);

            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("users")]
        public ActionResult<List<User>> GetUsers()
        {
            List<User> users = userDAO.GetUsers();
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{userId}")]
        public ActionResult<Account> GetAccount(int userId)
        {
            Account account = accountDAO.GetAccount(userId);
            if(account != null)
            {
                return Ok(account);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpGet("transfers/{userId}")]
        public ActionResult<IList<Transfer>> GetTransfers(int userId)
        {
            IList<Transfer> transfers = transferDAO.GetTransfers(userId);
            if(transfers != null)
            {
                return Ok(transfers);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost("transfers")]
        public ActionResult<Transfer> TransferToUser(Transfer transfer)
        {
            Transfer output = transferDAO.CreateTransfer(transfer);
            if (output != null)
            {
                return Created($"/transfers/{output.TransferId}",output);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("users/{account.UserID}")]
        public ActionResult<bool> UpdateBalance(Account account)
        {
            bool worked = accountDAO.UpdateBalance(account);
            
            return Ok(worked);
        }

        [HttpGet("{userID}/users")]
        public ActionResult<User> GetSome(int userId)
        {
            User output = userDAO.GetUserFromID(userId);
            if (output != null)
            {
                return Ok(output);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public ActionResult<Account> CreateAccount(Account account)
        {
            bool accountCreated = accountDAO.CreateAccount(account);
            return Ok(accountCreated);
        }
        [HttpGet("transfer/{transferID}")]
        public ActionResult<Transfer> GetTransferByTransferID(int transferId)
        {
            Transfer output = transferDAO.GetTransfer(transferId);
            if (output != null)
            {
                return Ok(output);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
