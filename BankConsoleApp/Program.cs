using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter the number of new customers' accounts you want create ");

            long num = Convert.ToInt64(Console.ReadLine());
            Account acct = new Account();

            for (int i = 1; i <= num; i++)
            {
                Console.WriteLine("Enter Customer {0}'s Firstname:",i);
                string firstname=Console.ReadLine();

                Console.WriteLine("Enter Customer {0}'s Lastname:", i);
                string lastname = Console.ReadLine();

                Console.WriteLine("Enter Customer {0}'s Account Number:", i);
                string accountnumber = Console.ReadLine();

                Console.WriteLine("Enter Customer {0}'s Initial Deposit:", i);
                decimal initialdeposit = Convert.ToDecimal(Console.ReadLine());

                acct.CreateAccount(firstname, lastname, accountnumber, initialdeposit);
            }


            if (acct.customer.Count > 0)
            {
                Console.WriteLine("Created Successfully");
            }

            acct.MakeTransfer();
            acct.currentBalance();

        }
    }


    public class Account
    {
        public List<Customer> customer = new List<Customer>();

        public List<Customer> CreateAccount(string firstname,string lastname, string accountnumber, decimal initialDeposit)
        {

            Customer cust = new Customer();
            cust.firstname = firstname;
            cust.lastname = lastname;
            cust.currentBalance = initialDeposit;
            cust.initialDeposit = initialDeposit;
            cust.accountNumber = accountnumber;
            customer.Add(cust);

            return customer;
        }

        public void MakeTransfer()
        {
            Console.WriteLine("Do you want to make a transfer (yes/no):");

            string option=Console.ReadLine().Trim().ToLower();

            if (option == "yes")
            {
                AccountToTaransferFrom:
                Console.WriteLine("Enter Account Number you want transfer from:");
                string fromacctno = Console.ReadLine().Trim().ToLower();

                int querycount = (from customer in customer where customer.accountNumber == fromacctno select customer).Count();

                if (querycount > 0)
                {
                    AccountToTaransferTo:
                    Console.WriteLine("Enter Account Number you want transfer to: ");
                    string toacctno = Console.ReadLine().Trim();

                    int querycount2 = (from customer in customer where customer.accountNumber == toacctno select customer).Count();
                    if (querycount2 > 0)
                    {
                        Console.WriteLine("Enter Amount you want to transfer:");
                        decimal amounttotrans = Convert.ToDecimal(Console.ReadLine().Trim());

                        
                        //transaction here

                        for (int i=0;i<=customer.Count-1;i++)
                        {

                            if (customer[i].accountNumber == fromacctno)
                            {
                                decimal curbal= customer[i].currentBalance;
                                
                                decimal trans=curbal - amounttotrans;

                                customer[i].currentBalance = trans;
                            }

                            if (customer[i].accountNumber == toacctno)
                            {

                                decimal curbal2 = customer[i].currentBalance;
                                
                                decimal trans2 = curbal2 + amounttotrans;
                                customer[i].currentBalance = trans2;
                                



                            }

                            

                        }
                        



                    }
                    else
                    {
                        Console.WriteLine("Customer does not exist");
                        goto AccountToTaransferTo;
                    }

                    }
                else
                {
                    Console.WriteLine("Customer does not exist");
                    goto AccountToTaransferFrom;
                }
                

            }
            else
            {
                Console.WriteLine("Bank application closes");
            }
        }

        public void currentBalance()
        {

            for (int j = 0; j <= customer.Count-1; j++)
            {
                Console.WriteLine("First Name: {0}, Last Name: {1}, Initial Deposit: {2}, Current Balance: {3}", customer[j].firstname, customer[j].lastname, customer[j].initialDeposit, customer[j].currentBalance);

            }



        }

       
    }




    public class Customer
    {
        public string firstname;
        public string lastname;
        public decimal initialDeposit;
        public decimal currentBalance;
        public string accountNumber;

    }




}
