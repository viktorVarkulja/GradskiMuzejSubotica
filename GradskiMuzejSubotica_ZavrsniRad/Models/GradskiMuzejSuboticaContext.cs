using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GradskiMuzejSubotica_ZavrsniRad.Models;

public partial class GradskiMuzejSuboticaContext : DbContext
{
    public GradskiMuzejSuboticaContext()
    {
    }

    public GradskiMuzejSuboticaContext(DbContextOptions<GradskiMuzejSuboticaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Izlozba> Izlozbas { get; set; }

    public virtual DbSet<Izlozbeno> Izlozbenos { get; set; }

    public virtual DbSet<Korisnik> Korisniks { get; set; }

    public virtual DbSet<Odeljenje> Odeljenjes { get; set; }

    public virtual DbSet<Predmet> Predmets { get; set; }

    public virtual DbSet<Racun> Racuns { get; set; }

    public virtual DbSet<Restauracija> Restauracijas { get; set; }

    public virtual DbSet<Rola> Rolas { get; set; }

    public virtual DbSet<StatusZahtev> StatusZahtevs { get; set; }

    public virtual DbSet<Strucnjak> Strucnjaks { get; set; }

    public virtual DbSet<Usluga> Uslugas { get; set; }

    public virtual DbSet<ZahtevRestauracija> ZahtevRestauracijas { get; set; }

    public virtual DbSet<Zbirka> Zbirkas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HP-RYZER3-WIN10;Database=GradskiMuzejSubotica;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Izlozba>(entity =>
        {
            entity.HasKey(e => e.SifIzlozba);

            entity.ToTable("Izlozba");

            entity.Property(e => e.SifIzlozba)
                .ValueGeneratedNever()
                .HasColumnName("sifIzlozba");
            entity.Property(e => e.BrSale).HasColumnName("brSale");
            entity.Property(e => e.DatumKraja)
                .HasColumnType("date")
                .HasColumnName("datumKraja");
            entity.Property(e => e.DatumPocetak)
                .HasColumnType("date")
                .HasColumnName("datumPocetak");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("naziv");
        });

        modelBuilder.Entity<Izlozbeno>(entity =>
        {
            entity.HasKey(e => e.SifIzlozbeno).HasName("PK_Izlozbeno_1");

            entity.ToTable("Izlozbeno");

            entity.HasIndex(e => e.SifIzlozbeno, "IX_Izlozbeno").IsUnique();

            entity.Property(e => e.InvBr).HasColumnName("invBr");
            entity.Property(e => e.SifIzlozba).HasColumnName("sifIzlozba");

            entity.HasOne(d => d.InvBrNavigation).WithMany(p => p.Izlozbenos)
                .HasForeignKey(d => d.InvBr)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Izlozbeno_Predmet1");

            entity.HasOne(d => d.SifIzlozbaNavigation).WithMany(p => p.Izlozbenos)
                .HasForeignKey(d => d.SifIzlozba)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Izlozbeno_Izlozba");
        });

        modelBuilder.Entity<Korisnik>(entity =>
        {
            entity.HasKey(e => e.SifKorisnik).HasName("PK_Korisnik_1");

            entity.ToTable("Korisnik");

            entity.HasIndex(e => e.KorisnickoIme, "IX_Korisnik").IsUnique();

            entity.Property(e => e.SifKorisnik).HasColumnName("sifKorisnik");
            entity.Property(e => e.ImePrezime)
                .HasMaxLength(50)
                .HasColumnName("imePrezime");
            entity.Property(e => e.KorisnickoIme)
                .HasMaxLength(50)
                .HasColumnName("korisnickoIme");
            entity.Property(e => e.Lozinka)
                .HasMaxLength(50)
                .HasColumnName("lozinka");
            entity.Property(e => e.SifRola).HasColumnName("sifRola");

            entity.HasOne(d => d.SifRolaNavigation).WithMany(p => p.Korisniks)
                .HasForeignKey(d => d.SifRola)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Korisnik_Rola");
        });

        modelBuilder.Entity<Odeljenje>(entity =>
        {
            entity.HasKey(e => e.SifOdelj);

            entity.ToTable("Odeljenje");

            entity.Property(e => e.SifOdelj)
                .ValueGeneratedNever()
                .HasColumnName("sifOdelj");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("naziv");
        });

        modelBuilder.Entity<Predmet>(entity =>
        {
            entity.HasKey(e => e.InvBr);

            entity.ToTable("Predmet");

            entity.Property(e => e.InvBr)
                .ValueGeneratedNever()
                .HasColumnName("invBr");
            entity.Property(e => e.Autor)
                .HasMaxLength(50)
                .HasColumnName("autor");
            entity.Property(e => e.BrDelova)
                .HasMaxLength(50)
                .HasColumnName("brDelova");
            entity.Property(e => e.Dimenzije)
                .HasMaxLength(50)
                .HasColumnName("dimenzije");
            entity.Property(e => e.Materijal)
                .HasMaxLength(50)
                .HasColumnName("materijal");
            entity.Property(e => e.Mesto)
                .HasMaxLength(50)
                .HasColumnName("mesto");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("naziv");
            entity.Property(e => e.SifZbirka).HasColumnName("sifZbirka");
            entity.Property(e => e.Stil)
                .HasMaxLength(50)
                .HasColumnName("stil");
            entity.Property(e => e.Tehnika)
                .HasMaxLength(50)
                .HasColumnName("tehnika");
            entity.Property(e => e.Vreme)
                .HasColumnType("date")
                .HasColumnName("vreme");

            entity.HasOne(d => d.SifZbirkaNavigation).WithMany(p => p.Predmets)
                .HasForeignKey(d => d.SifZbirka)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Predmet_Zbirka");
        });

        modelBuilder.Entity<Racun>(entity =>
        {
            entity.HasKey(e => e.SifRacun);

            entity.ToTable("Racun");

            entity.HasIndex(e => e.SifRacun, "IX_Racun").IsUnique();

            entity.Property(e => e.SifRacun).HasColumnName("sifRacun");
            entity.Property(e => e.Datum)
                .HasColumnType("date")
                .HasColumnName("datum");
            entity.Property(e => e.Kolicina).HasColumnName("kolicina");
            entity.Property(e => e.SifKorisnik).HasColumnName("sifKorisnik");
            entity.Property(e => e.SifUsluga).HasColumnName("sifUsluga");
            entity.Property(e => e.Vrednost).HasColumnName("vrednost");
            entity.Property(e => e.Vreme).HasColumnName("vreme");

            entity.HasOne(d => d.SifKorisnikNavigation).WithMany(p => p.Racuns)
                .HasForeignKey(d => d.SifKorisnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Racun_Korisnik1");

            entity.HasOne(d => d.SifUslugaNavigation).WithMany(p => p.Racuns)
                .HasForeignKey(d => d.SifUsluga)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Racun_Usluga");
        });

        modelBuilder.Entity<Restauracija>(entity =>
        {
            entity.HasKey(e => e.SifRestauracija);

            entity.ToTable("Restauracija");

            entity.HasIndex(e => e.SifRestauracija, "IX_Restauracija").IsUnique();

            entity.Property(e => e.SifRestauracija).HasColumnName("sifRestauracija");
            entity.Property(e => e.SifKorisnik).HasColumnName("sifKorisnik");
            entity.Property(e => e.SifZahtev).HasColumnName("sifZahtev");

            entity.HasOne(d => d.SifKorisnikNavigation).WithMany(p => p.Restauracijas)
                .HasForeignKey(d => d.SifKorisnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Restauracija_Korisnik1");

            entity.HasOne(d => d.SifZahtevNavigation).WithMany(p => p.Restauracijas)
                .HasForeignKey(d => d.SifZahtev)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Restauracija_ZahtevRestauracija");
        });

        modelBuilder.Entity<Rola>(entity =>
        {
            entity.HasKey(e => e.SifRola);

            entity.ToTable("Rola");

            entity.Property(e => e.SifRola)
                .ValueGeneratedNever()
                .HasColumnName("sifRola");
            entity.Property(e => e.NazivRola)
                .HasMaxLength(50)
                .HasColumnName("nazivRola");
        });

        modelBuilder.Entity<StatusZahtev>(entity =>
        {
            entity.HasKey(e => e.SifStatus);

            entity.ToTable("StatusZahtev");

            entity.Property(e => e.SifStatus)
                .ValueGeneratedNever()
                .HasColumnName("sifStatus");
            entity.Property(e => e.OpisStatus)
                .HasMaxLength(50)
                .HasColumnName("opisStatus");
        });

        modelBuilder.Entity<Strucnjak>(entity =>
        {
            entity.HasKey(e => e.SifStrucnjak);

            entity.ToTable("Strucnjak");

            entity.Property(e => e.SifStrucnjak)
                .ValueGeneratedNever()
                .HasColumnName("sifStrucnjak");
            entity.Property(e => e.SifKorisnik).HasColumnName("sifKorisnik");
            entity.Property(e => e.SifOdelj).HasColumnName("sifOdelj");

            entity.HasOne(d => d.SifKorisnikNavigation).WithMany(p => p.Strucnjaks)
                .HasForeignKey(d => d.SifKorisnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Strucnjak_Korisnik1");

            entity.HasOne(d => d.SifOdeljNavigation).WithMany(p => p.Strucnjaks)
                .HasForeignKey(d => d.SifOdelj)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Strucnjak_Odeljenje");
        });

        modelBuilder.Entity<Usluga>(entity =>
        {
            entity.HasKey(e => e.SifUsluga);

            entity.ToTable("Usluga");

            entity.HasIndex(e => e.SifUsluga, "IX_Usluga").IsUnique();

            entity.Property(e => e.SifUsluga).HasColumnName("sifUsluga");
            entity.Property(e => e.BrSale).HasColumnName("brSale");
            entity.Property(e => e.Cena).HasColumnName("cena");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("naziv");
            entity.Property(e => e.SifIzlozba).HasColumnName("sifIzlozba");

            entity.HasOne(d => d.SifIzlozbaNavigation).WithMany(p => p.Uslugas)
                .HasForeignKey(d => d.SifIzlozba)
                .HasConstraintName("FK_Usluga_Izlozba");
        });

        modelBuilder.Entity<ZahtevRestauracija>(entity =>
        {
            entity.HasKey(e => e.SifZahtev);

            entity.ToTable("ZahtevRestauracija");

            entity.HasIndex(e => e.SifZahtev, "IX_ZahtevRestauracija").IsUnique();

            entity.Property(e => e.SifZahtev).HasColumnName("sifZahtev");
            entity.Property(e => e.InvBr).HasColumnName("invBr");
            entity.Property(e => e.Komentar)
                .HasMaxLength(100)
                .HasColumnName("komentar");
            entity.Property(e => e.SifStatus).HasColumnName("sifStatus");
            entity.Property(e => e.SifStrucnjak).HasColumnName("sifStrucnjak");

            entity.HasOne(d => d.InvBrNavigation).WithMany(p => p.ZahtevRestauracijas)
                .HasForeignKey(d => d.InvBr)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ZahtevRestauracija_Predmet");

            entity.HasOne(d => d.SifStatusNavigation).WithMany(p => p.ZahtevRestauracijas)
                .HasForeignKey(d => d.SifStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ZahtevRestauracija_StatusZahtev");

            entity.HasOne(d => d.SifStrucnjakNavigation).WithMany(p => p.ZahtevRestauracijas)
                .HasForeignKey(d => d.SifStrucnjak)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ZahtevRestauracija_Strucnjak");
        });

        modelBuilder.Entity<Zbirka>(entity =>
        {
            entity.HasKey(e => e.SifZbirka);

            entity.ToTable("Zbirka");

            entity.Property(e => e.SifZbirka)
                .ValueGeneratedNever()
                .HasColumnName("sifZbirka");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("naziv");
            entity.Property(e => e.SifOdelj).HasColumnName("sifOdelj");

            entity.HasOne(d => d.SifOdeljNavigation).WithMany(p => p.Zbirkas)
                .HasForeignKey(d => d.SifOdelj)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zbirka_Odeljenje");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
