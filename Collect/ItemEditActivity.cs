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
    [Activity(Label = "Edit")]
    public class ItemEditActivity : Activity
    {
        EditText ItemTitle, ItemDescription, ItemWebsite;
        Button btnSave;
        ParseHandler objParse = ParseHandler.Default;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ItemEditLayout);
            ItemTitle = FindViewById<EditText>(Resource.Id.txtItemEditTitle);
            ItemDescription = FindViewById<EditText>(Resource.Id.txtItemEditDescription);
            ItemWebsite = FindViewById<EditText>(Resource.Id.txtItemEditWebsite);
            btnSave = FindViewById<Button>(Resource.Id.btnSaveItem);
            btnSave.Text = "Save Item";
            LoadData();
            btnSave.Click += BtnSave_Click;       
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add(0, 0, 0, "Delete Item");
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 0:
                    objParse.DeleteItem(SingletonB._instance.ItemID);
                    StartActivity(typeof(ListActivity));
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void LoadData()
        {
            ItemTitle.Text = SingletonB._instance.ItemName;
            ItemDescription.Text = SingletonB._instance.ItemDescription;
            ItemWebsite.Text = SingletonB._instance.ItemWebsite;
        }

        private async void SaveData()
        {
            await objParse.UpdateItem(ItemTitle.Text, ItemDescription.Text, ItemWebsite.Text);
            StartActivity(typeof(ListActivity));
        }

    }
}