using Xamarin.Forms;

namespace G00348036
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

           // NavigationPage.SetHasNavigationBar(this, false);
            //NavigationPage.BarTextColorProperty = Color.FromHex("e55050");
            //SetValue(NavigationPage.BarTextColorProperty, Color.White);
            //SetValue(NavigationPage.Na, Color.FromHex("e55050"));
        }

        private void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}
