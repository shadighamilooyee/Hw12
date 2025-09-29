
using HW12.Enums;
using HW12.LocalDb;
using HW12.Services;
using HW12.Tools;

Authentication authentication = new Authentication();
AdminService adminService = new AdminService();
RegularUserService regularUserService = new RegularUserService();

while (true)
{
    try
    {
        MyAuthentication();
        MyLibrary();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        MyLibrary();
    }
}
void MyAuthentication()
{
    int Choose = 0;
    bool isLoggedIn = false;
    do
    {
        Console.Clear();

        Console.WriteLine("1.Login");
        Console.WriteLine("2.Register");
        Console.Write("Please Choose One:");
        Choose = int.Parse(Console.ReadLine());
        switch (Choose)
        {
            case 1:
                Console.Write("Please Enter Your Username: ");
                string username1 = Console.ReadLine();
                Console.Write("Please Enter Your Password: ");
                string password1 = Console.ReadLine();
                isLoggedIn = authentication.Login(username1, password1);
                break;

            case 2:
                Console.Write("Please Enter A Username: ");
                string username2 = Console.ReadLine();
                Console.Write("Please Enter A Password: ");
                string password2 = Console.ReadLine();
                Console.Write("Please Choose Your Role (1 For Being Admin and 2 for Regular User): ");
                int role = int.Parse(Console.ReadLine());
                RoleEnum rolename;
                if (role == 1)
                    rolename = RoleEnum.Admin;
                else if (role == 2)
                    rolename = RoleEnum.RegularUser;
                else
                    throw new Exception("Wrong Role Number");
                authentication.Register(username2, password2, rolename);
                break;
        }
    } while (!isLoggedIn && Choose > 0);
}
void MyLibrary()
{
    if (LocalStorage.CurrentUser != null)
    {
        switch (LocalStorage.CurrentUser.Role)
        {
            case RoleEnum.Admin:
                AdminMenu();
                break;

            case RoleEnum.RegularUser:
                RegularUserMenu();
                break;
        }
    }
    else
    {
        MyAuthentication();
    }
}
void RegularUserMenu()
{
    int choice = 0;

    do
    {
        Console.Clear();
        Console.WriteLine("-".PadRight(20, '-'));
        Console.WriteLine("1.List Of Books");
        Console.WriteLine("2.Borrow Book");
        Console.WriteLine("3.My Books");
        Console.WriteLine("4.Add Review");
        Console.WriteLine("5.My Reviews");
        Console.WriteLine("6.Change My Comment");
        Console.WriteLine("7.Change My Rating");
        Console.WriteLine("8.Delete My Review");
        Console.WriteLine("9.See Book Reviews");
        Console.WriteLine("10.My Wishlists");
        Console.WriteLine("11.Add Wishlist");
        Console.WriteLine("12.Delete Wishlist");
        Console.WriteLine("13.Return Book");
        Console.WriteLine("14.My PenaltyAmount");
        Console.WriteLine("0.Exit");
        Console.WriteLine("-".PadRight(20, '-'));

        Console.Write("please choice a item : ");
        choice = int.Parse(Console.ReadLine());

        int userid = LocalStorage.CurrentUser.Id;

        switch (choice)
        {
            case 1:
                var categories1 = regularUserService.GetCategories();
                var mycategories1 = categories1.Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Books.Count
                });
                ConsolePainter.WriteTable(mycategories1);
                Console.Write("Please Enter Category Id: ");
                int categoryid = int.Parse(Console.ReadLine());
                var books1 = regularUserService.GetBooksByCategory(categoryid);
                var mybooks1 = books1.Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Author,
                    CategoryName = p.Category != null ? p.Category.Name : "No Category",
                    BookWishListCount = regularUserService.BookWishlistCount(p.Id),
                    p.IsBorrowed
                });
                ConsolePainter.WriteTable(mybooks1);
                Console.ReadKey();
                break;
            case 2:
                var books2 = regularUserService.GetBooks();
                var mybooks2 = books2.Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Author,
                    CategoryName = p.Category.Name,
                    BookWishListCount = regularUserService.BookWishlistCount(p.Id),
                    p.IsBorrowed
                });
                ConsolePainter.WriteTable(mybooks2);
                Console.Write("Please Enter Book Id: ");
                int bookid2 = int.Parse(Console.ReadLine());
                regularUserService.BorrowBook(bookid2, userid);
                Console.WriteLine("Book Borrowed");
                Console.ReadKey();
                break;
            case 3:
                var mybooks = regularUserService.UserBorrowedBooks(userid);
                var myborrowedbooks = mybooks.Select(p => new
                {
                    p.Id,
                    p.BookId,
                    BookTitle = p.Book.Title,
                    p.BorrowedDate
                });
                ConsolePainter.WriteTable(myborrowedbooks);
                Console.ReadKey();
                break;
            case 4:
                var books = adminService.GetBooks();
                var mybooks4 = books.Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Author,
                    CategoryName = p.Category.Name,
                    BookWishListCount = regularUserService.BookWishlistCount(p.Id),
                    p.IsBorrowed
                });
                ConsolePainter.WriteTable(mybooks4);
                Console.Write("Please Enter Book Id: ");
                int bookid4 = int.Parse(Console.ReadLine());
                Console.Write("Please Enter Your Comment: ");
                string comment = Console.ReadLine();
                Console.Write("Please Enter Your Rating (1 To 5): ");
                float rating = float.Parse(Console.ReadLine());
                regularUserService.AddUserReview(userid, bookid4, comment, rating);
                Console.WriteLine("Review Added");
                Console.ReadKey();
                break;
            case 5:
                var userreviews5 = regularUserService.GetUserReviews(userid);
                var myuserreviews5 = userreviews5.Select(p => new
                {
                    p.Id,
                    p.BookId,
                    BookTitle = p.Book.Title,
                    p.Rating,
                    p.Comment,
                    p.IsApproved
                });
                ConsolePainter.WriteTable(myuserreviews5);
                Console.ReadKey();
                break;
            case 6:
                var userreviews6 = regularUserService.GetUserReviews(userid);
                var myuserreviews6 = userreviews6.Select(p => new
                {
                    p.Id,
                    p.BookId,
                    BookTitle = p.Book.Title,
                    p.Rating,
                    p.Comment,
                    p.IsApproved
                });
                ConsolePainter.WriteTable(myuserreviews6);
                Console.Write("Please Enter Review Id: ");
                int reviewid6 = int.Parse(Console.ReadLine());
                Console.Write("Please Enter Your New Comment: ");
                string newcomment = Console.ReadLine();
                regularUserService.ChangeUserComment(newcomment, reviewid6);
                break;
            case 7:
                var userreviews7 = regularUserService.GetUserReviews(userid);
                var myuserreviews7 = userreviews7.Select(p => new
                {
                    p.Id,
                    p.BookId,
                    BookTitle = p.Book.Title,
                    p.Rating,
                    p.Comment,
                    p.IsApproved
                });
                ConsolePainter.WriteTable(myuserreviews7);
                Console.Write("Please Enter Review Id: ");
                int reviewid7 = int.Parse(Console.ReadLine());
                Console.Write("Please Enter Your New Rating: ");
                float newrating = float.Parse(Console.ReadLine());
                regularUserService.ChangeUserRating(newrating, reviewid7);
                break;
            case 8:
                var userreviews8 = regularUserService.GetUserReviews(userid);
                var myuserreviews8 = userreviews8.Select(p => new
                {
                    p.Id,
                    p.BookId,
                    BookTitle = p.Book.Title,
                    p.Rating,
                    p.Comment,
                    p.IsApproved
                });
                ConsolePainter.WriteTable(myuserreviews8);
                Console.Write("Please Enter Review Id: ");
                int reviewid8 = int.Parse(Console.ReadLine());
                regularUserService.DeleteUserReview(reviewid8, userid);
                Console.WriteLine("Review Deleted");
                Console.ReadKey();
                break;
            case 9:
                var books9 = adminService.GetBooks();
                var mybooks9 = books9.Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Author,
                    CategoryName = p.Category.Name,
                    BookWishListCount = regularUserService.BookWishlistCount(p.Id),
                    p.IsBorrowed
                });
                ConsolePainter.WriteTable(mybooks9);
                Console.Write("Please Enter Book Id: ");
                int bookid9 = int.Parse(Console.ReadLine());
                var booksreviews = regularUserService.ShowBooksReviews(bookid9);
                var avgrating = regularUserService.BookAvgRating(bookid9);
                var mybooksreviews = booksreviews.Select(p => new
                {
                    p.Comment,
                    p.Rating,
                    User = p.User.Username,

                });
                ConsolePainter.WriteTable(mybooksreviews);
                Console.WriteLine($"The Avg Rating Is {avgrating}");
                Console.ReadKey();
                break;
            case 10:
                var wishlist10 = regularUserService.GetUserWishlists(userid);
                var mywishlist10 = wishlist10.Select(p => new
                {
                    p.Id,
                    BookTitle = p.Book.Title,
                    p.CreatedAt
                });
                ConsolePainter.WriteTable(mywishlist10);
                Console.ReadKey();
                break;
            case 11:
                var books11 = adminService.GetBooks();
                var mybooks11 = books11.Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Author,
                    CategoryName = p.Category.Name,
                    BookWishListCount = regularUserService.BookWishlistCount(p.Id),
                    p.IsBorrowed
                });
                ConsolePainter.WriteTable(mybooks11);
                Console.Write("Please Enter Book Id: ");
                int bookid11 = int.Parse(Console.ReadLine());
                regularUserService.AddWishlist(userid, bookid11);
                Console.WriteLine("Wishlist Added");
                Console.ReadKey();
                break;
            case 12:
                var wishlist12 = regularUserService.GetUserWishlists(userid);
                var mywishlist12 = wishlist12.Select(p => new
                {
                    p.Id,
                    BookTitle = p.Book.Title,
                    p.CreatedAt
                });
                ConsolePainter.WriteTable(mywishlist12);
                Console.Write("Please Enter Wishlist Id: ");
                int wishlistid12 = int.Parse(Console.ReadLine());
                regularUserService.DeleteWishlist(wishlistid12, userid);
                Console.WriteLine("Wishlist Deleted");
                Console.ReadKey();
                break;
            case 13:
                var mybooks13 = regularUserService.UserBorrowedBooks(userid);
                var myborrowedbooks13 = mybooks13.Select(p => new
                {
                    p.Id,
                    p.BookId,
                    BookTitle = p.Book.Title,
                    p.BorrowedDate
                });
                ConsolePainter.WriteTable(myborrowedbooks13);
                Console.Write("Please Enter Book Id: ");
                int myborrowedbookid13 = int.Parse(Console.ReadLine());
                regularUserService.ReturnBook(myborrowedbookid13, userid);
                Console.WriteLine("Book Returned");
                Console.ReadKey();
                break;
            case 14:
                decimal penaltyamount = regularUserService.GetUserPenaltyAmount(userid);
                Console.WriteLine($"Your PenaltyAmount Is: {penaltyamount}");
                Console.ReadKey();
                break;
        }

    } while (choice > 0);
}
void AdminMenu()
{
    int choice = 0;

    do
    {
        Console.Clear();
        Console.WriteLine("-".PadRight(20, '-'));
        Console.WriteLine("1.List Of Books");
        Console.WriteLine("2.List Of Categories");
        Console.WriteLine("3.Add Category");
        Console.WriteLine("4.Add Book");
        Console.WriteLine("5.See Book Reviews");
        Console.WriteLine("6.Change IsApproved For Reviews");
        Console.WriteLine("7.See User PenaltyAmount");
        Console.WriteLine("0.Exit");
        Console.WriteLine("-".PadRight(20, '-'));

        Console.Write("please choice a item : ");
        choice = int.Parse(Console.ReadLine());

        int userid = LocalStorage.CurrentUser.Id;

        switch (choice)
        {
            case 1:
                var books = adminService.GetBooks();
                var mybooks = books.Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Author,
                    CategoryName = p.Category.Name,
                    BookWishListCount = regularUserService.BookWishlistCount(p.Id),
                    p.IsBorrowed
                });
                ConsolePainter.WriteTable(mybooks);
                Console.ReadKey();
                break;
            case 2:
                var categories = adminService.GetCategories();
                var mycategories = categories.Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Books.Count
                });
                ConsolePainter.WriteTable(mycategories);
                Console.ReadKey();
                break;
            case 3:
                Console.Write("Please Enter The Category Name: ");
                string categoryname = Console.ReadLine();
                adminService.AddCategory(categoryname);
                Console.WriteLine("Category Added");
                Console.ReadKey();
                break;
            case 4:
                var categories4 = adminService.GetCategories();
                ConsolePainter.WriteTable(categories4);
                Console.Write("Please Enter Category Id: ");
                int categoryid = int.Parse(Console.ReadLine());
                Console.Write("Please Enter The Book Title: ");
                string title = Console.ReadLine();
                Console.Write("Please Enter The Book Author: ");
                string author = Console.ReadLine();
                adminService.AddBook(title, author, categoryid);
                Console.WriteLine("Book Added");
                Console.ReadKey();
                break;
            case 5:
                var books5 = adminService.GetBooks();
                var mybooks5 = books5.Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Author,
                    CategoryName = p.Category.Name,
                    BookWishListCount = regularUserService.BookWishlistCount(p.Id),
                    p.IsBorrowed
                });
                ConsolePainter.WriteTable(mybooks5);
                Console.Write("Please Enter Book Id: ");
                int bookid5 = int.Parse(Console.ReadLine());
                var booksreviews = regularUserService.ShowBooksReviews(bookid5);
                var avgrating = regularUserService.BookAvgRating(bookid5);
                var mybooksreviews = booksreviews.Select(p => new
                {
                    p.Comment,
                    p.Rating,
                    User = p.User.Username
                });
                ConsolePainter.WriteTable(mybooksreviews);
                Console.WriteLine($"The Avg Rating Is {avgrating}");
                Console.ReadKey();
                break;
            case 6:
                var alluserreviews = adminService.GetAllReviews();
                var userreviews = alluserreviews.Select(p => new
                {
                    p.Id,
                    p.BookId,
                    BookTitle = p.Book.Title,
                    p.Rating,
                    p.Comment,
                    p.IsApproved
                });
                ConsolePainter.WriteTable(userreviews);
                Console.Write("Please Enter Review Id: ");
                int reviewid6 = int.Parse(Console.ReadLine());
                Console.Write("Please Enter 1 To Approved The Review and 2 For Not Approved: ");
                int isapproved = int.Parse(Console.ReadLine());
                bool isapproved6 = false;
                if (isapproved == 1)
                    isapproved6 = true;
                else if (isapproved > 2)
                    throw new Exception("Wrong Input");
                adminService.ChangeIsApproved(reviewid6, isapproved6);
                break;
            case 7:
                Console.Write("Please Enter User Id: ");
                int myuserid = int.Parse(Console.ReadLine());
                decimal penaltyamount = regularUserService.GetUserPenaltyAmount(myuserid);
                Console.WriteLine($"Your PenaltyAmount Is: {penaltyamount}");
                Console.ReadKey();
                break;
        }

    } while (choice > 0);
}