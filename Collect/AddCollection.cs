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

namespace Collect
{
    [Activity(Label = "New Collection")]
    public class AddCollection : Activity
    {
        Button btnSaveCollection;
        TextView txtTitle;
        ParseHandler objParse = ParseHandler.Default;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ItemEditLayout);
            FindViewById<TextView>(Resource.Id.txtItemEditDescription).Visibility = ViewStates.Gone;
            FindViewById<TextView>(Resource.Id.lblItemEditDescr).Visibility = ViewStates.Gone;
            FindViewById<TextView>(Resource.Id.txtItemEditWebsite).Visibility = ViewStates.Gone;
            FindViewById<TextView>(Resource.Id.lblItemEditWeb).Visibility = ViewStates.Gone;
            btnSaveCollection = FindViewById<Button>(Resource.Id.btnSaveItem);
            txtTitle = FindViewById<TextView>(Resource.Id.txtItemEditTitle);
            btnSaveCollection.Text = "Add Collection";

            btnSaveCollection.Click += BtnSaveCollection_Click;
        }

        private async void BtnSaveCollection_Click(object sender, EventArgs e)
        {
            await objParse.CreateCollection(txtTitle.Text);
            StartActivity(typeof(CollectionsActivity));
        }
    }
}