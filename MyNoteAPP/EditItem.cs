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
    [Activity(Label = "EditItem")]
    public class EditItem : Activity
    {

        int NoteID;
        string Title;
        string Details;

        TextView txtTitle;
        TextView txtDetails;
        
       

        Button btnEdit;
        Button btnDelete;
        Button btnBackEdit;
        

        DatabaseManager objDB;
      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
         

            // Create your application here
            Window.RequestFeature(WindowFeatures.NoTitle); //This will Hide the title Bar

            SetContentView(Resource.Layout.EditItems);
                        txtTitle = FindViewById<TextView>(Resource.Id.txtEditTitle);
                        txtDetails = FindViewById<TextView>(Resource.Id.txtEditDetails);


            btnBackEdit = FindViewById<Button>(Resource.Id.btnBackEdit);
                        btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
                        btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
                        
                        btnEdit.Click += BtnEdit_Click;
                        btnDelete.Click += BtnDelete_Click;
            btnBackEdit.Click += BtnBackEdit_Click;
           

                        NoteID = Intent.GetIntExtra("NoteID", 0);
                        Details = Intent.GetStringExtra("Details");
                        Title = Intent.GetStringExtra("Title");

                        txtTitle.Text = Title;
                        txtDetails.Text = Details;
                        objDB = new DatabaseManager();


        }

        private void BtnBackEdit_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //Toast.MakeText(this, "Note Deleted. Under construction", ToastLength.Long).Show();
            try
            {
                objDB.DeleteItem(NoteID);
                Toast.MakeText(this, "Note Deleted", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));

            }
            catch(Exception ex) { Console.WriteLine("Error  Occured" + ex.Message); }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            Update_Note();
        }
        //Function update
        public void Update_Note()
        {
            try
            {
                if (txtTitle.Text != "" || txtDetails.Text != "")
                {
                    DateTime now = DateTime.Now.ToLocalTime();
                    objDB.EditItem(txtTitle.Text, txtDetails.Text,now, NoteID);
                    Toast.MakeText(this, "Note Updated", ToastLength.Long).Show();
                    this.Finish();
                    StartActivity(typeof(MainActivity));
                }
                else
                {
                    StartActivity(typeof(MainActivity));

                }


            }
            catch (Exception ex) { Console.WriteLine("Error Edit Occured" + ex.Message); }
        }
        public override void OnBackPressed()
        {
           Update_Note();
        }

    }
}