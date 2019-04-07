
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipeInformation : ContentPage
	{
		public RecipeInformation (string id)
		{
			InitializeComponent ();
            this.BindingContext = new RecipeInformationViewModel(new PageService(), id);
        }
	}
}