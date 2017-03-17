using System;
using SQLite;



namespace MyNoteAPP
{
    public class MyNote
    {
        [PrimaryKey, AutoIncrement]
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }

        public MyNote()
        {

        }
    }
    
}