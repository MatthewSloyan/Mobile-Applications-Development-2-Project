
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchByRecipesListView : ContentPage
    {
        public SearchByRecipesListView(string URL, int selection)
        {
            InitializeComponent();

            this.BindingContext = new RecipesViewModel(URL, selection);
        }
    }
}