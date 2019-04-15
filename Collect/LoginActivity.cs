using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Parse;

namespace Collect
{
    [Activity(Label = "Collect", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : Activity
    {
        EditText txtUsername, txtPassword;
        TextView txtRegisterLink;
        Button btnLogin;
        ParseHandler objParse = ParseHandler.Default;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            if (ParseUser.CurrentUser != null)
            {
                StartActivity(typeof(CollectionsActivity));
            }
            SetContentView(Resource.Layout.Login);

            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            txtRegisterLink = FindViewById<TextView>(Resource.Id.txtRegisterLink);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            btnLogin.Click += btnLogin_Click;
            txtRegisterLink.Click += txtRegist_Click;

        }        

        private void txtRegist_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Register));
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            
            
                //If fields are populated
                if (txtUsername.Text != "" && txtPassword.Text != "")
                {
                    var result = await objParse.Login(txtUsername.Text, txtPassword.Text);
                    if (result == true)
                    {
                        //Set username global
                        User.Globals.username = txtUsername.Text;
                        //Start collections Activity
                        StartActivity(typeof(CollectionsActivity));
                    }
                    else
                    {
                        Toast.MakeText(this, "Login Failed", ToastLength.Short).Show();
                    }
                }
                                   
        }
        


    }
}

