using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using WebBlazorApp.Models;
using Abc.BusinessService;
using Abc.UnitOfWorkLibrary;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;
using ABC.Models;

namespace WebBlazorApp.Pages.Components.BookPage
{
    [Authorize(Roles ="User, Admin")]
	public partial class AddBook
	{
        [Inject]
        IUnitOfWork unitOfWork { get; set; }

        [Inject]
        IBookService bookService { get; set; }

        [Inject]
        IAuthorService authorService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }


        public BookModel Book { get; set; }
        
        public IEnumerable<SelectListItem> Authors { get; set; }

        protected override Task OnInitializedAsync()
        {
            LoadData();
            return base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            var listData = await this.authorService.GetAllAuthors();
            this.Authors = listData.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            this.Book = new BookModel { };
            StateHasChanged();
        }

        private async Task AddAuthor()
        {
            if (Book.Title != "" && Book.Description != "" && Book.AuthorId > 0)
            {
                using (var trans = await this.unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        await this.bookService.AddBook(new ABC.Models.Book { Title = Book.Title, Description = Book.Description, AuthorId = Book.AuthorId });
                        await this.unitOfWork.CommitTransactionAsync(trans);
                        navigationManager.NavigateTo("/book");
                    }
                    catch (Exception)
                    {
                        await this.unitOfWork.RollbackTransactionAsync(trans);
                        throw;
                    }
                    finally
                    {
                        await trans.DisposeAsync();
                    }
                }
            }
        }

        private void Back()
        {
            navigationManager.NavigateTo("/book");
        }
    }
}
