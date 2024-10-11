
using KutuphanApi.Models;
using KutuphanApi.Response;
using Kutuphane.Bussiness.Abstract;
using Kutuphane.Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphanApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
        private IOperationsService _operationsService;
        private IUserService _userService;

        public GenericController(IBookService bookService, ICategoryService categoryService, IAuthorService authorService, IOperationsService operationsService, IUserService userService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _operationsService = operationsService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int pageSize = 25, int pageIndex = 1)
        {
            //Geri Veri göndermediğin zaman yani model olarka bir şey görünmemesini istersen bu şekilde response yaparsın (HTTPDELETE)
            //var response2 = new Response.Response();
            var bookresponse = new ListResponse<BookListModel>();
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

                bookresponse.Model = books;
                bookresponse.Success = true;
            }
            catch (Exception ex)
            {
                bookresponse.Message = "ex";
                bookresponse.Success = false;
                return bookresponse.ToHttpResponse();
            }
            return bookresponse.ToHttpResponse();
        }
        [HttpGet]
        public async Task<IActionResult> Get(/*int pageSize =25, int pageIndex = 1*/)
        {
            var authorresponse = new ListResponse<AuthorListModel>();
            try
            {
                IQueryable<Author> authorList = _authorService.GetQueryable(true);
                var authors = authorList.ToList().Select(p => new AuthorListModel()
                {
                    AuthorID = p.AuthorID,
                    AuthorName = p.AuthorName,
                    status = true

                }).ToList();
                authorresponse.Model = authors;
                authorresponse.Success = true;

            }
            catch (Exception)
            {
                authorresponse.Message = "Exception";
                authorresponse.Success = false;
                return authorresponse.ToHttpResponse();
            }
            return authorresponse.ToHttpResponse();

        }
    }
}