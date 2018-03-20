using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml;
using System.Text.RegularExpressions;
using System;
using System.Net;
using System.IO;

namespace lab5
{
    [Activity(Label = "lab5", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button httpRequest;
        private ListView list;
        private List<string> listFiller = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            httpRequest = FindViewById<Button>(Resource.Id.Button);
            httpRequest.Click += HttpRequest_Click;
        }

        private void HttpRequest_Click(object sender, EventArgs e)
        {
            listFiller.Clear();
            var rxcui = "198440";
            //Формируем HTTP запрос
            var request = HttpWebRequest.Create(string.Format(@"http://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/{0}/allinfo", rxcui));
            request.ContentType = "application/json";
            request.Method = "GET";
            HttpWebResponse response;

            //отсылаем запрос
            try
            {
                using (response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string error = string.Format("Error status code: {0}", response.StatusCode);
                        Toast.MakeText(this, error, ToastLength.Short).Show();
                    }
                    else
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            var content = reader.ReadToEnd();
                            string name = Regex.Match(content, "<displayName>(.*?)</displayName>").Groups[1].Value.ToString();
                            string synonym = Regex.Match(content, "<synonym>(.*?)</synonym>").Groups[1].Value.ToString();
                            string route = Regex.Match(content, "<route>(.*?)</route>").Groups[1].Value.ToString();
                            string strength = Regex.Match(content, "<strength>(.*?)</strength>").Groups[1].Value.ToString();
                            listFiller.Add(string.Format("Название: {0}", name));
                            listFiller.Add(string.Format("Cиноним: {0}", synonym));
                            listFiller.Add(string.Format("Cпособ применения: {0}", route));
                            listFiller.Add(string.Format("Дозировка: {0}", strength));
                        }
                }
                list = FindViewById<ListView>(Resource.Id.listView);
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listFiller);
                list.Adapter = adapter;
            }
            catch (Exception exception)
            {
                string error = string.Format("Error status code: {0}", exception.ToString());
                Toast.MakeText(this, error, ToastLength.Long).Show();
            }
        }
    }
}

