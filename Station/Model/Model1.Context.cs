﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Station.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StationEntities : DbContext
    {
        public StationEntities()
            : base("name=StationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        private static StationEntities _context;

        public static StationEntities GetContext()
        {
            if (_context == null)
                _context = new StationEntities();
            return _context;
        }
        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Akt> Akt { get; set; }
        public virtual DbSet<Auto> Auto { get; set; }
        public virtual DbSet<Check> Check { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Graphic> Graphic { get; set; }
        public virtual DbSet<Mechanic> Mechanic { get; set; }
        public virtual DbSet<Passport> Passport { get; set; }
        public virtual DbSet<Schet> Schet { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Type_pay> Type_pay { get; set; }
        public virtual DbSet<Zakaz> Zakaz { get; set; }
        public virtual DbSet<Zapchast> Zapchast { get; set; }
        public virtual DbSet<Zapic> Zapic { get; set; }
    }
}