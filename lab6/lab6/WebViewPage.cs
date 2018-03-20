using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

namespace lab6
{
    [Activity(Label = "WebViewPage")]
    public class WebViewPage : Activity
    {
        WebView wv;
        Button home, back, forward;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WebViewPage);
            // Create your application here

            home = FindViewById<Button>(Resource.Id.home);
            back = FindViewById<Button>(Resource.Id.back);
            forward = FindViewById<Button>(Resource.Id.forward);

            home.Click += homeClick;
            back.Click += backClick;
            forward.Click += forwardClick;
            //WebView init
            wv = FindViewById<WebView>(Resource.Id.webView1);
            wv.SetWebViewClient(new WebViewClient());
            wv.Settings.JavaScriptEnabled = true;
            wv.LoadUrl(Intermediate.URLNews);
        }

        private void forwardClick(object sender, EventArgs e)
        {
            if (wv.CanGoForward())
                wv.GoForward();
        }

        private void homeClick(object sender, EventArgs e)
        {
            Finish();
        }

        private void backClick(object sender, EventArgs e)
        {
            if (wv.CanGoBack()) wv.GoBack();
        }
    }
}