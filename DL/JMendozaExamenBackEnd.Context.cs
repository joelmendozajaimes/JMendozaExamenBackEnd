﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JMendozaExamenBackEndEntities : DbContext
    {
        public JMendozaExamenBackEndEntities()
            : base("name=JMendozaExamenBackEndEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Banco> Bancoes { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Ocupacion> Ocupacions { get; set; }
        public virtual DbSet<Pai> Pais { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Sexo> Sexoes { get; set; }
        public virtual DbSet<TipoPersonaFiscal> TipoPersonaFiscals { get; set; }
    }
}
