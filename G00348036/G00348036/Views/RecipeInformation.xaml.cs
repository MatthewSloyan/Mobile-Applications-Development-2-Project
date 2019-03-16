﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipeInformation : ContentPage
	{
		public RecipeInformation ()
		{
			InitializeComponent ();
            this.BindingContext = new RecipeInformationViewModel("1234");
        }
	}
}