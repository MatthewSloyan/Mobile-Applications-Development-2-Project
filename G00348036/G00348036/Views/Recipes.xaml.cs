using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Recipes : ContentPage
	{
        public string url { get; set; }

        public Recipes (string URL)
		{
			InitializeComponent ();

            this.url = URL;
            setUpComponents();
		}

        private void setUpComponents()
        {
            System.Diagnostics.Debug.WriteLine(url);
            List<SearchByIngredientsData> results = Utils.GetApiData<SearchByIngredientsData>(url);

            System.Diagnostics.Debug.WriteLine(results[0].id);
        }
    }
}