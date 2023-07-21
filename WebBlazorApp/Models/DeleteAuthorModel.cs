using Microsoft.AspNetCore.Components;

namespace WebBlazorApp.Models
{
    public class DeleteModel<T>
    {
        public T Model { get; set; }

        public RenderFragment PopupMessage { get; set; }

        public RenderFragment PopupHeader { get; set; }
    }
}
