using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Solid
{
    interface IMessage
    {
        void Send();
    }

    class Sms : IMessage
    {
        public string Number { get; set; }
        public string Content { get; set; }

        public void Send()
        {
            SendSms();
        }

        public void SendSms()
        {
            Console.WriteLine("Sending sms...");
        }
    }
    class Mms : IMessage
    {
        public string Number { get; set; }
        public byte[] Content { get; set; }

        public void Send()
        {
            SendMms();
        }

        public void SendMms()
        {
            Console.WriteLine("Sending mms...");
        }
    }
    class Email : IMessage
    {
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public byte[] Attachment { get; set; }

        public void Send()
        {
            SendEmail();
        }

        public void SendEmail()
        {
            Console.WriteLine("Sending email...");
        }
    }



    class Messanger
    {
        private IMessage _message;

        //Wstrzyknięcie zależności przez konstruktor
        public Messanger(IMessage message)
        {
            _message = message;
        }

        //Wstrzyknięcie zależności przez właściwość
        private IMessage Message { get; set; }


        //Wstrzyknięcie zależności przez metodę
        public void Send(IMessage message)
        {
            message.Send();
        }

        public void Send()
        {
            _message.Send();
        }

    }
}
