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
    [Activity(Label = "ListActivity")]
    public class ListActivity : Activity
    {
        ParseHandler objParse = ParseHandler.Default;
        ListView List;
        List<CollectionItems> Items;
        Button btnAddItem;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ListLayout);
            this.Title = SingletonB._instance.SelectedCollectionName;
            List = FindViewById<ListView>(Resource.Id.listView);
            btnAddItem = FindViewById<Button>(Resource.Id.btnAddItem);
            
            GetItems();

            List.ItemClick += List_ItemClick;
            btnAddItem.Click += BtnAddItem_Click;
            
        }

        public override void OnBackPressed()
        {
            StartActivity(typeof(CollectionsActivity));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add(0, 0, 0, "Delete Collection");
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 0:
                    objParse.DeleteCollection("collection");
                    StartActivity(typeof(CollectionsActivity));
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AddItem));
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            SingletonB._instance.ItemName = Items[e.Position].Name;
            SingletonB._instance.ItemDescription = Items[e.Position].Description;
            SingletonB._instance.ItemWebsite = Items[e.Position].Website;
            SingletonB._instance.ItemID = Items[e.Position].ObjectId;
            StartActivity(typeof(ItemEditActivity));
        }

        private async void GetItems()
        {
            Items = await objParse.GetItems();
            List.Adapter = new ListAdapter(this, Items);
        }
    }
}