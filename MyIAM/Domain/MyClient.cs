﻿using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIAM.Domain
{
    [Table("Client")]
    public class MyClient : Client, IAMDatabaseKey
    {
        public Guid Id { get; set; }
    }
}
