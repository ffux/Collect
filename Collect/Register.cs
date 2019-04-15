using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Collect
{
    [Activity(Label = "Register")]
    public class Register : Activity
    {

        EditText txtUsername, txtPassword, txtEmail;
        Button btnRegister;
        ParseHandler objParse = ParseHandler.Default;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Register);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            // Create your application here

            btnRegister.Click += Register_Click;
        }

        private async void Register_Click(object sender, EventArgs e)
        {
            //Check if fields are empty
            if (txtEmail.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                //Check if email contains @
                bool b = txtEmail.Text.Contains("@");
                if (b)
                {
                    await objParse.CreateUserAsync(txtEmail.Text, txtUsername.Text, txtPassword.Text);
                    await objParse.Login(txtUsername.Text, txtPassword.Text);
                    StartActivity(typeof(CollectionsActivity));
                }
                else
                {

                }
            }
        }
    }
}