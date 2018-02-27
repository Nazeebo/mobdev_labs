using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using System;
using System.Globalization;

namespace lab2
{
    [Activity(Label = "lab2", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button load,save,clear;
        EditText editText;
        String filename1 = "res1.txt";
        string path,filename;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            editText = FindViewById<EditText>(Resource.Id.editText);
            load = FindViewById<Button>(Resource.Id.Load);
            save = FindViewById<Button>(Resource.Id.Save);
            clear = FindViewById<Button>(Resource.Id.Clear);
            path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            filename = Path.Combine(path, filename1);

            load.Click += LoadClick;
            save.Click += SaveClick;
            clear.Click += ClearClick;

            if (!File.Exists(filename))
            {
                using (var streamWriter = new StreamWriter(filename, false))
                {
                    streamWriter.WriteLine("Hi, world!");
                }
            }
            LoadClick(null, null);
        }

        private void SaveClick(object sender, EventArgs e)
        {
            using (var streamWriter = new StreamWriter(filename, false))
            {
                DateTime localDate = DateTime.Now;
                var culture = new CultureInfo("ru-RU");
                streamWriter.Write(editText.Text.ToCharArray());
                streamWriter.WriteLine("{0}: {1}", culture, localDate.ToString(culture));
            }
        }

        private void LoadClick(object sender, EventArgs e)
        {
            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                editText.Text = content;
            }
        }

        private void ClearClick(object sender, EventArgs e)
        {
            editText.Text = "";
        }
    }
}

