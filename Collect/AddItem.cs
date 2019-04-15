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
    [Activity(Label = "AddItem")]
    public class AddItem : Activity
    {
        Button btnSave;
        EditText txtTitle, txtDescription, txtWebsite;
        ParseHandler objParse = ParseHandler.Default;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ItemEditLayout);
            txtTitle = FindViewById<EditText>(Resource.Id.txtItemEditTitle);
            txtDescription = FindViewById<EditText>(Resource.Id.txtItemEditDescription);
            txtWebsite = FindViewById<EditText>(Resource.Id.txtItemEditWebsite);
            btnSave = FindViewById <Button>(Resource.Id.btnSaveItem);
            btnSave.Text = "Add Item";
            btnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            await objParse.SaveItem(txtTitle.Text, txtDescription.Text, txtWebsite.Text);
            StartActivity(typeof(ListActivity));
        }
    }
}