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

namespace lab3
{
    [Activity(Label = "ActivityRename")]
    public class ActivityRename : Activity
    {
        EditText name;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PageForText);
            name = FindViewById<EditText>(Resource.Id.editText);
            name.Text = Path.GetFileName(Intermediate.fileName);

            Button button = FindViewById<Button>(Resource.Id.Confirm);
            button.Click += ButtonClick;
            Button cancel = FindViewById<Button>(Resource.Id.Cancel);
            cancel.Click += delegate
            {
                Intermediate.fileName = null;
                StartActivity(typeof(MainActivity));
                Finish();
            };


        }

        private void ButtonClick(object sender, EventArgs e)
        {
            string input = name.Text;
            if (string.IsNullOrEmpty(name.Text) || string.IsNullOrWhiteSpace(name.Text))
            {
                Toast.MakeText(this, "This field shouldn't be empty", ToastLength.Short).Show();
            }
            else
            {
                string newFileName = Path.Combine(Path.GetDirectoryName(Intermediate.fileName), input);
                File.Move(Intermediate.fileName, newFileName);
                Intermediate.fileName = null;
                StartActivity(typeof(MainActivity));
                Finish();
            }
        }
    }
}