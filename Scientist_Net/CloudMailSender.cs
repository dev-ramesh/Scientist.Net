using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scientist_Net
{
    class CloudMailSender
    {

        public bool isValidEmail(string email)
        {
            return email.Contains(".com");
        }


        public void SendSmtpMail(string sendermail, string receivermail, string subject, string emailText)
        {
            Console.WriteLine(string.Format("Email sent to : {0} ",receivermail));

            //simulating sending email.
            Thread.Sleep(200);


        }
    }
}
