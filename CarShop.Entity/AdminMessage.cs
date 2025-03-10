using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class AdminMessage: IEntity
    {
        public int AdminMessageId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public List<AdminMessageAnonimMessage> AdminMessageAnonimMessages {get; set;}


    }
}