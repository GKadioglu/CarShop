using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace CarShop.Entity
{
    public class AdminMessageAnonimMessage: IEntity
    {
        public int AdminMessageId { get; set; }
        public AdminMessage AdminMessage { get; set; }
        public int MessageId { get; set; }
        public AnonimMessage AnonimMessage {get; set;}

    }
}