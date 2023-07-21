using Abc.BusinessService;
using Microsoft.AspNetCore.Components;
using System;
using WebBlazorApp.Models;

namespace WebBlazorApp.Pages.Components.AuthorPage
{
	public partial class Author
	{
        [Inject] 
        IAuthorService AuthorServiceObj { get; set; }

        ModelPopup modelObj { get; set; }

        private bool IsNotified { set; get; }

        private IEnumerable<AuthorModel> Authors { get; set; }

        protected override Task OnInitializedAsync()
        {
            LoadData();
            return base.OnInitializedAsync();
        }

        private void EditAuthorData(int id)
        {
            this._navigationManager.NavigateTo($"/author/edit/{id}");
        }

        private void AddAuthorPage()
        {
            _navigationManager.NavigateTo("/addauthor");
        }

        private async Task ModalCallbackMethod(ModalObj obj)
        {
            if (obj.IsConfirm)
            {
                AuthorModel author = obj.Data as AuthorModel;
                if (author != null)
                {
                    using (var trans = await this._unitOfWork.BeginTransactionAsync())
                    {
                        try
                        {
                            await this.AuthorServiceObj.DeleteAuthor(author.Id);
                            await this._unitOfWork.CommitTransactionAsync(trans);
                            await LoadData();
                        }
                        catch (Exception)
                        {
                            await this._unitOfWork.RollbackTransactionAsync(trans);
                            throw;
                        }
                        finally
                        {
                            await trans.DisposeAsync();
                        }
                    }
                }
            }
        }

        private void DeleteData<T>(DeleteModel<T> authorModel)
        {            
            this.modelObj.ShowModal(authorModel.Model, authorModel.PopupHeader, authorModel.PopupMessage);
        }

        private async Task LoadData()
        {
            var listData = await this.AuthorServiceObj.GetAllAuthors();
            List<AuthorModel> authors = new List<AuthorModel>();

            foreach (var item in listData)
            {
                var model = new AuthorModel { Id = item.Id, Name = item.Name };
                List<BookModel> books = new List<BookModel>();

                if (item.Books != null)
                {
                    foreach (var book in item.Books)
                    {
                        var _book = new BookModel
                        {
                            Id = book.Id,
                            Title = book.Title,
                            Description = book.Description,
                            AuthorName = item.Name
                        };

                        books.Add(_book);
                    }
                }

                model.Books = books;
                authors.Add(model);
            }

            this.Authors = authors;

            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
    }
}
