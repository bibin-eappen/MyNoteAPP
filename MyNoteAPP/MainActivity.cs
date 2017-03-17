using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using MyNoteApp;
using Java.IO;
using System.IO;
using Android.Views;
using Android.Runtime;
using Android.Content;
using System;
using MyNoteAPP.Resources.values;

namespace MyNoteAPP
{
    [Activity(Label = "MyNoteAPP")]
    public class MainActivity : Activity
    {
        ListView lstNoteList;
        EditText txtSearch;
        List<MyNote> myList;
        Toolbar toolbar1;
        TextView lblNoteNo;
        int Note_Count;
        static string dbName = "MyNote_Bibin.sqlite";
        string dbpath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        DatabaseManager objDB;
        Button btnAdd;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
           Window.RequestFeature(WindowFeatures.NoTitle); //This will Hide the title Bar
            SetContentView(Resource.Layout.Main);
            toolbar1 = FindViewById<Toolbar>(Resource.Id.toolbar1);
            //toolbar1.Title = "Hello";
            
            lblNoteNo= FindViewById<TextView>(Resource.Id.lblNoteNo);
            lstNoteList = FindViewById<ListView>(Resource.Id.listView1);
            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            txtSearch = FindViewById<EditText>(Resource.Id.txtSearch);

            //copies files to phone
            CopyDatabase();
            objDB = new DatabaseManager();
            myList = objDB.ViewAll();
            Note_Count = myList.Count;
            lblNoteNo.Text = "Notes :"+Note_Count.ToString();

            lstNoteList.Adapter = new DataAdapter(this, myList);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            lstNoteList.ItemClick += lstNoteList_ItemClick;
            btnAdd.Click += BtnAdd_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;

        


        }

        private void TxtSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            string SearchQuery = e.Text.ToString();

            myList = objDB.SearchAll(SearchQuery);

            if (myList != null)

            { lstNoteList.Adapter = new DataAdapter(this, myList); }
        }

        private void lstNoteList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView1 = sender as ListView;
            var MyNoteItem = myList[e.Position];
           // var editItem = new Intent(this, typeof(EditItem));
            var edit = new Intent(this, typeof(EditItem));

            edit.PutExtra("Title", MyNoteItem.Title);
            edit.PutExtra("Details", MyNoteItem.Details);
            edit.PutExtra("NoteID", MyNoteItem.NoteID);

            StartActivity(edit);
           
        }

      

        private void BtnAdd_Click(object sender, EventArgs e)
        {

            var addItem = new Intent(this, typeof(AddItem));           
            StartActivity(addItem);
        }


        public void CopyDatabase()
        {
            if (!System.IO.File.Exists(dbpath))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbpath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);

                        }
                    }
                }
            }
        }

       public override bool OnCreateOptionsMenu(IMenu menu)
          {
              menu.Add("Add");
          
              return base.OnCreateOptionsMenu(menu);
           
          }
      


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var itemTitle = item.TitleFormatted.ToString();
            switch (itemTitle)
            {
                case "Add":
                    break;
               

            }
            return base.OnOptionsItemSelected(item);

        }

        protected override void OnResume()
        {
            base.OnResume();
            objDB = new DatabaseManager();
            myList = objDB.ViewAll();
            lstNoteList.Adapter = new DataAdapter(this, myList);
        }

        protected override void OnStart()
        {
            base.OnStart();
            base.OnResume();
            objDB = new DatabaseManager();
            myList = objDB.ViewAll();
            lstNoteList.Adapter = new DataAdapter(this, myList);
        }

   
           
    }

    
}

