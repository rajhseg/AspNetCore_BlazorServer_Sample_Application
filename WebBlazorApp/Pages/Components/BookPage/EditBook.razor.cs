using Abc.BusinessService;
using Abc.UnitOfWorkLibrary;
using ABC.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBlazorApp.Models;

namespace WebBlazorApp.Pages.Components.BookPage
{
	public partial class EditBook
	{
		[Inject]
		NavigationManager navigationManager { get; set; }

		[Inject]
		IAuthorService authorService { get; set; }

		[Inject]
		IBookService bookService { get; set; } 

        [Inject]
        IUnitOfWork unitOfWork { get; set; }

		private BookModel Book { get;set; }

        private IEnumerable<SelectListItem> Authors { get; set; }


		protected override Task OnInitializedAsync()
		{
			LoadData();
			return base.OnInitializedAsync();
		}

		private async Task LoadData()
		{
            var listData = await this.authorService.GetAllAuthors();
            this.Authors = listData.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            var data = (await this.bookService.GetBooks(x => x.Id == int.Parse(Id))).FirstOrDefault();

			if (data == null)
				navigationManager.NavigateTo("/book");

            this.Book = new BookModel { Id = data.Id, Title = data.Title, Description = data.Description, AuthorId = data.AuthorId, AuthorName = data.Author.Name };

			StateHasChanged();
        }

        private async Task UpdateBook()
		{
            using (var transaction = await this.unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await this.bookService.UpdateBook(new ABC.Models.Book { Id = int.Parse(Id), Title = Book.Title, Description = Book.Description, AuthorId = Book.AuthorId });
                    await this.unitOfWork.CommitTransactionAsync(transaction);
                    navigationManager.NavigateTo("/book");
                }
                catch (Exception)
                {
                    await this.unitOfWork.RollbackTransactionAsync(transaction);
                    throw;
                }
                finally
                {
                    await transaction.DisposeAsync();
                }
            }
        }

		private void BackToList()
		{
			this.navigationManager.NavigateTo("/book");
		}
    }
}
