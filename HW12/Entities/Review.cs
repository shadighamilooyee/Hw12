﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }     
        public Book Book { get; set; }
        public int BookId { get; set; }
        public string? Comment { get; set; }
        public float Rating { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
