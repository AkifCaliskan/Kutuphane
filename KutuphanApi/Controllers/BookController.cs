
using KutuphanApi.Models;
using KutuphanApi.Response;
using Kutuphane.Bussiness.Abstract;
using Kutuphane.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;
using System.Threading.Tasks;

namespace KutuphanApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
        private IOperationsService _operationsService;
        private IUserService _userService;

        public BookController(IBookService bookService, ICategoryService categoryService, IAuthorService authorService, IOperationsService operationsService, IUserService userService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _operationsService = operationsService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int pageSize=25, int pageIndex = 1)
        {
            //Geri Veri göndermediğin zaman yani model olarka bir şey görünmemesini istersen bu şekilde response yaparsın (HTTPDELETE)
            //var response2 = new Response.Response();
            var response = new ListResponse<BookListModel>();
            try
            {
                IQueryable<Book> bookList = _bookService.GetQueryable(true).Include(p => p.Category).Include(p => p.Author).Include(p => p.Operationss);
                //response.ItemsCount = await bookList.CountAsync();

                var books = bookList.ToList().Select(p => new BookListModel()
                {
                    AuthorName = _authorService.GetById(p.AuthorId).AuthorName,
                    BasimYili = p.PublicationYear,
                    CategoryName = _categoryService.GetById(p.CategoryId).CategoryName,
                    BookId = p.BookID,
                    BookName = p.BookName,
                    UserId = _operationsService.GetQueryable(true).Include(k => k.User).Include(k => k.Book).FirstOrDefault(k => k.BookId == p.BookID)?.UserId ?? 0,
                    SayfaSayisi = p.PageCount,
                    UserName = string.Join(" ", _operationsService.GetQueryable(true).Include(k => k.User).Include(k => k.Book).FirstOrDefault(k => k.BookId == p.BookID)?.User.FirstName ?? "", _operationsService.GetQueryable(false).Include(k => k.User).Include(k => k.Book).FirstOrDefault(k => k.BookId == p.BookID)?.User.LastName ?? ""),
                    SeriNo = p.SerialNumber,
                }).ToList();

                response.Model = books;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "ex";
                response.Success = false;
                return response.ToHttpResponse();
            }
            return response.ToHttpResponse();


            // public async Task <IActionResult> Add(int pageSize=25, int pageIndex=1)
            //{
            //    SetAddViewData();
            //    return Ok();
            //}

            //[HttpPost]
            //public async Task<IActionResult> Add(BookAddModel model)
            //{
            //    var response = new AddResponse<BookAddModel>();

               

            //    var addBook = _bookService.Add(new Book
            //    {
            //        AuthorId = model.AuthorId,
            //        YayinYili = model.YayinYili,
            //        BookName = model.BookName,
            //        CategoryId = model.CategoryId,
            //        PageCount = model.PageCount,
            //        PublicationYear = model.PublicationYear,
            //        Status = true,
            //        SerialNumber = model.SerialNumber,
            //        BookDescription = model.Description

            //    }); ;

            //    //_notifyService.Custom("Custom Notification - closes in 5 seconds.", 5, "whitesmoke", "fa fa-gear");
               
            //    return RedirectToAction("Index", "Book");
            //}
           

        }
    }
}
