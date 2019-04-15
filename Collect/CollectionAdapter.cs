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
    public class CollectionAdapter : BaseAdapter<Collection>
    {
        List<Collection> items;

        Activity context;

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override Collection this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public CollectionAdapter(Activity context, List<Collection> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null)
            {
                view = this.context.LayoutInflater.Inflate(Resource.Layout.CollectionLayout, null);
                view.FindViewById<TextView>(Resource.Id.txtCollectionTitle).Text = item.Name;
            }
            return view;
        }
    }
}