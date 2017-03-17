using System;
using System.IO;
using System.Collections.Generic;
using MyNoteAPP;



namespace MyNoteApp
{
    public class DatabaseManager
    {
        static string dbName = "MyNote_Bibin.sqlite";
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        public DatabaseManager()
        {

        }
        public List<MyNote> ViewAll()
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = "select * from tblMyNote";
                    var NoteList = cmd.ExecuteQuery<MyNote>();
                   
                    return NoteList;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
                return null;
            }

        }

        public void EditItem(string title,string details,DateTime now,int listid)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = "update tblMyNote set Title='" + title + "',Details='" + details + "',Date='" + now + "'where NoteID=" + listid;
                    cmd.ExecuteNonQuery();

                }
            }catch(Exception e) { Console.WriteLine("Error:" + e.Message); }

        }

       public void AddItem(String title,string details)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = "insert into tblMyNote(Title,Details)values('"+title+"','"+details+"')";
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception e) { Console.WriteLine("Error:" + e.Message); }

        }

        public void DeleteItem(int noteid)
        {
            try
            {
                using (var conn=new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = "delete from tblMyNote where NoteId= " + noteid;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Delete " + ex.Message);
            }
        }

        public List<MyNote> SearchAll(string query)
        {

            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = "select * from tblMyNote where Title LIKE '%" + query + "%' or Details LIKE '%" + query + "%'";
                    var NoteList = cmd.ExecuteQuery<MyNote>();

                    return NoteList;


                }
            }
            catch (Exception e)
            { Console.WriteLine("Error:" + e.Message); return null; }
        }

     
    }
}



