namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EFContext : DbContext
    {
        // Your context has been configured to use a 'DataBase' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // '_03___DataLayer.Model.DataBase' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DataBase' 
        // connection string in the application configuration file.
        public EFContext(string connStrring)
            : base(connStrring)
        {

        }
        //"name=DataBaseHome"
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Tariff> Tariffs { get; set; }
        public virtual DbSet<RentValue> RentValues { get; set; }
    }

}