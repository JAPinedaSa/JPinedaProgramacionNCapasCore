using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class UsuarioContext : DbContext
{
    public UsuarioContext()
    {
    }

    public UsuarioContext(DbContextOptions<UsuarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aseguradora> Aseguradoras { get; set; }

    public virtual DbSet<Colonium> Colonia { get; set; }

    public virtual DbSet<Dependiente> Dependientes { get; set; }

    public virtual DbSet<DependienteTipo> DependienteTipos { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Poliza> Polizas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<SubPoliza> SubPolizas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= Usuario; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aseguradora>(entity =>
        {
            entity.HasKey(e => e.IdAseguradora).HasName("PK__Asegurad__8FA1C5971468E629");

            entity.ToTable("Aseguradora");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Aseguradoras)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asegurado__IdUsu__1ED998B2");
        });

        modelBuilder.Entity<Colonium>(entity =>
        {
            entity.HasKey(e => e.IdColonia).HasName("PK__Colonia__A1580F6645BFD0BF");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Colonia)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Colonia__IdMunic__3F466844");
        });

        modelBuilder.Entity<Dependiente>(entity =>
        {
            entity.HasKey(e => e.IdDependiente).HasName("PK__Dependie__366D07718B3C6874");

            entity.ToTable("Dependiente");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Genero)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IdEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rfc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDependienteTipoNavigation).WithMany(p => p.Dependientes)
                .HasForeignKey(d => d.IdDependienteTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dependien__IdDep__6FE99F9F");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Dependientes)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dependien__IdDep__6EF57B66");
        });

        modelBuilder.Entity<DependienteTipo>(entity =>
        {
            entity.HasKey(e => e.IdDependienteTipo).HasName("PK__Dependie__2C220C6263786E97");

            entity.ToTable("DependienteTipo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__Direccio__FAD4F1FCFB183681");

            entity.ToTable("Direccion");

            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdColonia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Direccion__IdCol__4222D4EF");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Direccion__IdUsu__4316F928");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.NumeroEmpleado).HasName("PK__Empleado__44F848FC7A982A00");

            entity.ToTable("Empleado");

            entity.HasIndex(e => e.Correo, "UQ__Empleado__60695A19FDD924CC").IsUnique();

            entity.Property(e => e.NumeroEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.FechaRegistro).HasColumnType("date");
            entity.Property(e => e.Foto).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nss)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NSS");
            entity.Property(e => e.Rfc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK__Empleado__IdEmpr__4F7CD00D");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__5EF4033E80D6F222");

            entity.ToTable("Empresa");

            entity.Property(e => e.Correo)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.DireccionWeb)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Logo).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC140D1902B");

            entity.ToTable("Estado");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Estados)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estado__IdPais__398D8EEE");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__Municipi__6100597894C3B05D");

            entity.ToTable("Municipio");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Municipio__IdEst__3C69FB99");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PK__Pais__FC850A7BF73A9B4B");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.HasKey(e => e.IdPoliza).HasName("PK__Poliza__8E3943B33D6DEAF6");

            entity.ToTable("Poliza");

            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroPoliza)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSubPolizaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdSubPoliza)
                .HasConstraintName("FK__Poliza__IdSubPol__5EBF139D");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Poliza__IdUsuari__5FB337D6");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C83F3F0F0");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubPoliza>(entity =>
        {
            entity.HasKey(e => e.IdSubPoliza).HasName("PK__SubPoliz__91F56A98F09850F9");

            entity.ToTable("SubPoliza");

            entity.Property(e => e.IdSubPoliza).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9710255D08");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.UserName, "UserNameUnico").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Imagen).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdRol__173876EA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
