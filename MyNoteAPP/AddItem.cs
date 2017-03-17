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
using MyNoteApp;

namespace MyNoteAPP.Resources.values
{
    [Activity(Label = "AddItem")]
    public class AddItem : Activity
    {
        Button btnAdd;
        Button btnNotes;
        EditText txtItemDescription;
        EditText txtItemTitle;
        TextView lblAddNote;
        

        DatabaseManager objdb = new DatabaseManager();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Window.RequestFeature(WindowFeatures.NoTitle); //This will Hide the title Bar

            SetContentView(Resource.Layout.AddItem);

            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnNotes = FindViewById<Button>(Resource.Id.btnNotes);
            txtItemTitle = FindViewById<EditText>(Resource.Id.txtItemTitle);
            txtItemDescription = FindViewById<EditText>(Resource.Id.txtItemDescription);
            lblAddNote= FindViewById<TextView>(Resource.Id.lblAddNote);

            lblAddNote.Text = "New Note";

            //Events
            btnAdd.Click += BtnAdd_Click;
            btnNotes.Click += BtnNotes_Click;


                
           
        }

        private void BtnNotes_Click(object sender, EventArgs e)
        {

            Add_Note();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Add_Note();
        }
        public override void OnBackPressed()
        {
            Add_Note();
        }

        public void Add_Note()
        {
            if (txtItemTitle.Text != "" || txtItemDescription.Text != "")
            {
                objdb.AddItem(txtItemTitle.Text, txtItemDescription.Text);
                Toast.MakeText(this, "Note Added", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
            else { StartActivity(typeof(MainActivity)); }
        }

    }

}