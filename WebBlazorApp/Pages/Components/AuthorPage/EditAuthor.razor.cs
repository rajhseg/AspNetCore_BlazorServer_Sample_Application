using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using WebBlazorApp.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using Abc.UnitOfWorkLibrary;
using Abc.BusinessService;

namespace WebBlazorApp.Pages.Components.AuthorPage
{
    [Authorize(Roles ="User")]
	public partial class EditAuthor
	{        
        public AuthorModel AuthorData { get; set; }

        private string PhotoPath { get; set; }

        private IBrowserFile Photo { get; set; }

        private string? ImgUri { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var data = (await this._authorService.GetAuthors(x => x.Id == int.Parse(Id))).FirstOrDefault();

            this.AuthorData = new AuthorModel { Id = data.Id, Name = data.Name, PhotoName = data.PhotoName };

            this.PhotoPath = "/images/" + (string.IsNullOrEmpty(AuthorData.PhotoName) ? "noimage.jpg" : AuthorData.PhotoName);

            StateHasChanged();

            base.OnInitializedAsync();
        }

        private async Task BackToList()
        {
            _navigationManager.NavigateTo("/listauthor");
        }

        private async Task LoadImage(InputFileChangeEventArgs e)
        {
            this.Photo = e.File;

            var image = await e.File.RequestImageFileAsync(e.File.ContentType, 600, 600);

            using Stream imageStream = image.OpenReadStream(1024 * 1024 * 10);

            using MemoryStream ms = new();
           
            await imageStream.CopyToAsync(ms);
   
            ImgUri = $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";

            StateHasChanged();
        }

        private async Task UpdateAuthor()
        {

            using (var trans = await this._unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var photo_name = await processUploadFile();
                    await this._authorService.UpdateAuthor(new ABC.Models.Author { Id = AuthorData.Id, Name = AuthorData.Name, PhotoName = photo_name });
                    await this._unitOfWork.CommitTransactionAsync(trans);
                    _navigationManager.NavigateTo("/listauthor");
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

        private async Task<string> processUploadFile()
        {
            string filename = "noimage.jpg";

            if (Photo != null)
            {
                var folder = Path.Combine(_environment.WebRootPath, "images");
                var fileGuid = Guid.NewGuid().ToString() + "_" + Photo.Name;
                var fullpath = Path.Combine(folder, fileGuid);

                using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                {
                    var content = Photo.OpenReadStream();
                    await content.CopyToAsync(fs);
                }

                filename = fileGuid;
            }

            return filename;
        }
    }
}
