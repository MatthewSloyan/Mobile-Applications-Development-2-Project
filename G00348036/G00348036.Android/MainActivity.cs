
using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Media;

namespace G00348036.Droid
{
    [Activity(Label = "G00348036", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        // Set up camera functionality using Media Plugin, implemented using plugin documentation.
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //CrossCurrentActivity.Current.Init(this, bundle);
            CrossMedia.Current.Initialize();
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        // From research I have found there's a bug if a page is returned to either a tabbed or master details page it crashes on Android.
        // A fix for this is overriding the back button and moving the task to the back of the activity stack seems to solve the problem, 
        // which I found below at the link. It seems it's a common problem.
        // https://forums.xamarin.com/discussion/81793/back-button-from-causes-crash-on-android-when-page-is-masterdetail

        // From further testing I have found that this is not needed, as it has been fixed. 
        //public override void OnBackPressed()
        //{
        //    this.MoveTaskToBack(true);
        //    base.OnBackPressed();
        //}
    }
}