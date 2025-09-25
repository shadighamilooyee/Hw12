﻿
namespace HW12.Entities
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowedDate { get; set; }
    }
}
