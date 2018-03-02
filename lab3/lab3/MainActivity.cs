using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Runtime;
using Android.Content;
using System;
using System.IO;
using System.Collections.Generic;

namespace lab3
{
    public static class Intermediate
    {
        public static string fileName;
        public static string newFileName; }
    [Activity(Label = "lab3", MainLauncher = true)]
    public class MainActivity : Activity
    {
        string path;
        private ListView fileList;
        private List<string> fileNames = new List<string>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var files = System.IO.Directory.GetFiles(path);
            if(files.Length <= 0)
            {
                File.WriteAllText(Path.Combine(path, "Res1"), "Res1 text");
                File.WriteAllText(Path.Combine(path, "Res2"), "Res2 text");
                File.WriteAllText(Path.Combine(path, "Res3"), "Res3 text");
            }

            foreach (string file in files)
            {
                fileNames.Add(Path.GetFileName(file));
            }

            fileList = FindViewById<ListView>(Resource.Id.listViewFiles);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, fileNames);
            fileList.Adapter = adapter;
            fileList.ItemClick += ListView_ItemClick;

            Button buttonEdit = FindViewById<Button>(Resource.Id.Edit);
            Button buttonRemove = FindViewById<Button>(Resource.Id.Remove);
            Button buttonRename = FindViewById<Button>(Resource.Id.Rename);
            Button buttonAdd = FindViewById<Button>(Resource.Id.Add);

            buttonEdit.Click += ButtonEditClick ;
            buttonRemove.Click += ButtonRemoveClick;
            buttonRename.Click += ButtonRenameClick;
            buttonAdd.Click += ButtonAddClick;
        }

        private void ButtonAddClick(object sender, EventArgs e)
        {
            StartActivity(typeof(ActivityAdd));
            Finish();
        }

        private void ButtonEditClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Intermediate.fileName))
            {
                Toast.MakeText(this, "Select a file",ToastLength.Short).Show();
            }
            else  StartActivity(typeof(Page2));
        } 

        private void ButtonRenameClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Intermediate.fileName))
            {
                Toast.MakeText(this, "Select a file", ToastLength.Short).Show();
            }
            else
            {
                StartActivity(typeof(ActivityRename));
                Finish();
            }
        }

        private void ButtonRemoveClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Intermediate.fileName))
            {
                Toast.MakeText(this, "Select a file", ToastLength.Short).Show();
            }
            else
            {
                File.Delete(Intermediate.fileName);
                Intermediate.fileName = null;
                StartActivity(typeof(MainActivity));
                Finish();
            }
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, Path.Combine(path, fileNames[e.Position]), ToastLength.Long).Show();
            Intermediate.fileName = Path.Combine(path, fileNames[e.Position]);
        }
    }
}

