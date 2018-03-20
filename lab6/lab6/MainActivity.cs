using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace lab6
{
    public static class Intermediate
    {
        public static string URLNews = "http://www.bbc.com/";
    }

    [Activity(Label = "lab6", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button news;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            news = FindViewById<Button>(Resource.Id.news);
            news.Click += newsClick;
        }

        private void newsClick(object sender, EventArgs e)
        {
            StartActivity(typeof(WebViewPage));
        }
    }
}

