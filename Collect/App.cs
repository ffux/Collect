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
using Parse;

namespace Collect
{
    [Application]
    public class App : Application
    {

        public App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Initialize the Parse client with your Application ID and .NET Key found on
            // your Parse dashboard
            ParseClient.Initialize("mc8TqcwyvSSzrWYNqaeKfbMww9cOEgJE3bciWNlA", "9NhPpPz7Lj4AG0q8Mi4dIjvB5qUD5Hc9yfwmPmmG");           
        }
    }
}

