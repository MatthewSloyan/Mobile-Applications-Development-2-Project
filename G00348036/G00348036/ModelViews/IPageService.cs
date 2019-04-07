using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace G00348036
{
    public interface IPageService
    {
        // Method declarations 
        Task PushAsync(Page page);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
    }
}
