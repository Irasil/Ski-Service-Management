﻿using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Ski_Service_Management.Models
{
    /// <summary>
    /// Inhalt der Tabelle Mitarbeiter
    /// </summary>
    public class Registration
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Pickup_Date { get; set; }
        
        public int PriorityId { get; set; }
        public Priority? Priority { get; set; } = new Priority();   
        public int StatusId { get; set; }
        public Status? Status { get; set; } = new Status();
        public int ServiceId { get; set; }
        public Service Service { get; set; } = new Service();   




    }
}

