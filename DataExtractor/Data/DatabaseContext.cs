using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;


namespace DataExtractor.Data
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext() :
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = "DataBase/database.sqlite3", ForeignKeys = true }.ConnectionString
            }, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Lead> Leads { get; set; }

        public DbSet<Note> NotesContacts { get; set; }
        public DbSet<Note> NotesTasks { get; set; }
        public DbSet<Note> NotesLeads { get; set; }

        public DbSet<CustomField> CustomFields { get; set; }
        
        

    }
}


           