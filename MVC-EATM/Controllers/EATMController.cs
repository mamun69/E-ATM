using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_EATM.Models;
using System.Data.Entity;
using System.Globalization;

namespace MVC_EATM.Controllers
{
    public class EATMController : Controller
    {
        public ApplicationDbContext _context;

        public EATMController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult AdminOptions()
        {
            return View();
        }
        //
        // GET: /EATM/
        public ActionResult LoginInput(string errorMsg)
        {
            ViewBag.Message = errorMsg;
            return View();
        }

        public ActionResult Login(Account account)
        {
            var inputAccount = _context.Accounts.FirstOrDefault(a => a.accountNo == account.accountNo && a.pin == account.pin);
            if (inputAccount != null)
            {
                return RedirectToAction("Options", new{accountNo = account.accountNo});
            }
            else
            {
                
                return RedirectToAction("LoginInput", new {errorMsg = "Wrong login information!"});
            }
        }

        public ActionResult Options(int accountNo)
        {
            var account = GetAccount(accountNo);
            return View(account);
        }

        private Account GetAccount(int accountNo)
        {
            return _context.Accounts.FirstOrDefault(a => a.accountNo == accountNo);
        }

        public ActionResult BalanceWithdrawl(int accountNo)
        {
            var account = GetAccount(accountNo);
            return View(account);
        }

        public ActionResult Success(int amount, int accountNo)
        {
            var account = GetAccount(accountNo);
            if (NumberOfDailyTransiction(accountNo) < 3)
            {
                if (amount > 0)
                {
                    if (amount <= 2000)
                    {
                        if (account.balance > amount)
                        {
                            account.balance -= amount;
                            _context.SaveChanges();
                            UpdateTransictionHistory(accountNo, amount);
                            ViewBag.Message = amount;
                            return View(account);
                        }
                        else
                        {
                            return RedirectToAction("ErrorMassage",
                                new
                                {
                                    whthdrawError = "You dont have sufficient Balance",
                                    accountNo = account.accountNo
                                });
                        }
                    }
                    else
                    {
                        return RedirectToAction("ErrorMassage",
                            new
                            {
                                whthdrawError = "You can not withdraw more then 2000 BDT at a time",
                                accountNo = account.accountNo
                            });
                    }

                }
                else
                {
                    return RedirectToAction("ErrorMassage",
                        new {whthdrawError = "You entered negative amount", accountNo = account.accountNo});
                }
            }
            else
            {
                return RedirectToAction("ErrorMassage",
                    new { whthdrawError = "You have reached at daily maximun transaction limit", accountNo = account.accountNo });
            }
            
            
            
        }



        public ActionResult ErrorMassage(string whthdrawError, int accountNo)
        {
            Account account = GetAccount(accountNo);
            ViewBag.Message = whthdrawError;
            return View(account);
        }
        public ActionResult BalanceCheck(int accountNo)
        {
            var account = GetAccount(accountNo);
            return View(account);
        }

        public ActionResult AccountTransaction()
        {
            
            var history = _context.TransactionHistories.ToList();
            
            return View(history);
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        public ActionResult CreateSuccess(Account account)
        {
            var newAccount = _context.Accounts.FirstOrDefault(a => a.accountNo == account.accountNo);
            if (newAccount == null)
            {
                AddNewAccount(account);
                return RedirectToAction("ShowAccount");
            }
            else
            {
               return RedirectToAction("CreateAccount");
            }

        }

        public ActionResult ShowAccount()
        {
            var account = _context.Accounts.ToList();

            return View(account);
        }

        private void UpdateTransictionHistory(int accountNo, int amount)
        {
            TransactionHistory transactionHistory = new TransactionHistory();
            transactionHistory.accountNo= accountNo;
            transactionHistory.withdrawlAmount = amount;
            transactionHistory.transactionTime = DateTime.Now;
            _context.TransactionHistories.Add(transactionHistory);
            _context.SaveChanges();
        }

        private void AddNewAccount(Account account)
        {
            Account nAccount = new Account();
            nAccount.accountNo = account.accountNo;
            nAccount.balance = account.balance;
            nAccount.pin = account.pin;
            _context.Accounts.Add(nAccount);
            _context.SaveChanges();
        }

        private int NumberOfDailyTransiction(int accountNo)
        {
            int count = 0;
            foreach (var th in _context.TransactionHistories)
            {
                if (th.accountNo == accountNo &&
                    th.transactionTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) ==
                    DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture))
                {
                    count++;
                }
            }
            return count;
        }
	}
}