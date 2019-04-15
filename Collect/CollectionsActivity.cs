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
    [Activity(Label = "Collections")]
    public class CollectionsActivity : Activity
    {
        ListView CollectionsList;
        List<Collection> Collections;
        public static string CollectionSelectedName;
        Button btnNewCollection;

        ParseHandler objParse = ParseHandler.Default;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Collections);
            CollectionsList = FindViewById<ListView>(Resource.Id.CollectionsList);
            btnNewCollection = FindViewById<Button>(Resource.Id.btnNewCollection);
            GetCollections();

            CollectionsList.ItemClick += CollectionSelected;
            btnNewCollection.Click += BtnNewCollection_Click;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add(0, 0, 0, "Logout");
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 0: objParse.Logout();
                    StartActivity(typeof(LoginActivity));
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void BtnNewCollection_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AddCollection));
        }

        private void CollectionSelected(object sender, AdapterView.ItemClickEventArgs e)
        {
            StartActivity(typeof(ListActivity));
            objParse.CollectionSelected(Collections[e.Position].ObjectId);
            SingletonB._instance.SelectedCollection = Collections[e.Position].ObjectId;
            SingletonB._instance.SelectedCollectionName = Collections[e.Position].Name;

        }

        private async void GetCollections()
        {
            Collections = await objParse.GetAllCollections();
            CollectionsList.Adapter = new CollectionAdapter(this, Collections);
            
        }

        
    }
}