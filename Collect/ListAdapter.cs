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
using Java.Net;
using Android.Graphics;
using Java.IO;
using Android.Graphics.Drawables;
using Android.Util;
using System.Net;
using System.IO;
using Parse;

namespace Collect
{
    public class ListAdapter : BaseAdapter<CollectionItems>
    {
        List<CollectionItems> items;

        Activity context;
        public override CollectionItems this[int position]
        {            
            get
            {
                return items[position];
            }
            
        }

        public override int Count
        {
            get
            {
                return items.Count; 
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public ListAdapter(Activity context, List<CollectionItems> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.ItemLayout, null);                
            }
            view.FindViewById<TextView>(Resource.Id.txtTitle).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.txtDescription).Text = item.Description;
            if (item.Website != "")
            {
                view.FindViewById<TextView>(Resource.Id.txtWebsite).Text = item.Website;
            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.txtWebsite).Visibility = Android.Views.ViewStates.Gone;
            }

            return view;
        }
    }
}