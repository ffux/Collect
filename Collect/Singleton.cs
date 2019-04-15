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
    class SingletonB
    {
        public static readonly SingletonB _instance = new SingletonB();

        public string ItemName;
        public string ItemDescription;
        public string ItemWebsite;
        public string ItemID;
        public string SelectedCollection;
        public string SelectedCollectionName;

        SingletonB()
        {
        }
    }
}