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
using System.IO;
using System.Globalization;

namespace lab3
{
    [Activity(Label = "Page2")]
    public class Page2 : Activity
    {
        EditText editText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Page2);
            editText = FindViewById<EditText>(Resource.Id.editText);
            using (var streamReader = new StreamReader(Intermediate.fileName))
            {
                string content = streamReader.ReadToEnd();
                editText.Text = content;
            }
            Button buttonGo = FindViewById<Button>(Resource.Id.gotoMainPage);
            buttonGo.Click += MainPageClick;
            Button Save = FindViewById<Button>(Resource.Id.Save);
            Save.Click += SaveClick;
        }

        private void SaveClick(object sender, EventArgs e)
        {
            using (var streamWriter = new StreamWriter(Intermediate.fileName, false))
            {
                DateTime localDate = DateTime.Now;
                var culture = new CultureInfo("ru-RU");
                streamWriter.Write(editText.Text.ToCharArray());
                streamWriter.WriteLine("{0}: {1}", culture, localDate.ToString(culture));
            }
        }

        private void MainPageClick(object sender, EventArgs e)
        {
            Intermediate.fileName = null;
            Finish();
        }
    }
}