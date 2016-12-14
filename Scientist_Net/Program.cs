using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHub;

namespace Scientist_Net
{
    class Program
    {
        static void Main(string[] args)
        {
            var smptSender = new SmtpMailSender();
            var cloudSender = new CloudMailSender();

            Scientist.ResultPublisher = new  SqlServerPublisher();

            string[] emailAddresses = new string[] { "wonderful@gmail.com","pissoff.net","killme@com","whoisthat.com"};

            foreach (var emailAddress in emailAddresses)
            {
                

                    bool isValidEmail = Scientist.Science<bool>("cloud-email-gateway", experiment=> {


                    experiment.AddContext("email address",emailAddress);

                    experiment.Use(()=>smptSender.isValidEmail(emailAddress));

                experiment.Try("Cloud sender",() => cloudSender.isValidEmail(emailAddress));


                });



            }

            Console.ReadLine();


        }
    }
}
