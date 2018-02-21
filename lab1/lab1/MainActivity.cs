using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace lab1
{
    [Activity(Label = "lab1", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button action;
        TextView result;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            result = FindViewById<TextView>(Resource.Id.textViewResult);
            action = FindViewById<Button>(Resource.Id.buttonCalculate);

            action.Click += findRoots;
        }

        private void findRoots(object sender, EventArgs e)
        {
            decimal a = string.IsNullOrEmpty(FindViewById<EditText>(Resource.Id.editTextA).Text)? 0 : decimal.Parse(FindViewById<EditText>(Resource.Id.editTextA).Text);
            decimal b = string.IsNullOrEmpty(FindViewById<EditText>(Resource.Id.editTextB).Text) ? 0 : decimal.Parse(FindViewById<EditText>(Resource.Id.editTextB).Text);
            decimal c = string.IsNullOrEmpty(FindViewById<EditText>(Resource.Id.editTextC).Text) ? 0 : decimal.Parse(FindViewById<EditText>(Resource.Id.editTextC).Text);

            decimal d = b * b - 4 * a * c;
            decimal x1r, x2r,xi;
            if (d > 0 && a != 0)
            {
                x1r = Math.Round(((-b + (decimal)Math.Sqrt((double)d)) / 2 * a), 2);
                x2r = Math.Round(((-b - (decimal)Math.Sqrt((double)d)) / 2 * a), 2);
                result.Text = string.Format("x1 = {0},  x2 = {1}", x1r, x2r);
            }
            else if (d == 0 && a != 0)
            {
                x1r = Math.Round((-b / 2 * a), 2);
                result.Text = string.Format("x1 = x2 = {0}", x1r);
            }
            else if (d < 0 && a != 0)
            {
                d = Math.Abs(d);
                xi = Math.Round(((decimal)Math.Sqrt((double)d)) / 2 * a, 2);
                x1r = Math.Round((-b / 2 * a), 2);
                result.Text = string.Format("x1 = {0} + {1}i,  x2 = {0} - {1}i", x1r, xi);
            }
            else if (a == 0 && b != 0)
            {
                x1r = Math.Round((-c / b), 2);
                result.Text = string.Format("x = {0}", x1r);
            }
            else if (a == 0 && b == 0)
                result.Text = string.Format("Error");
        }
    }
}

