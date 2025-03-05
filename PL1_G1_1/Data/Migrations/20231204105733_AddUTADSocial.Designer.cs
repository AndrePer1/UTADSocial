﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PL1_G1_1.Data;

#nullable disable

namespace PL1_G1_1.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231204105733_AddUTADSocial")]
    partial class AddUTADSocial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GrupoPublicacao", b =>
                {
                    b.Property<int>("IdGrupo")
                        .HasColumnType("int");

                    b.Property<int>("IdPublicacao")
                        .HasColumnType("int");

                    b.HasKey("IdGrupo", "IdPublicacao");

                    b.HasIndex("IdPublicacao");

                    b.ToTable("GrupoPublicacao");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PL1_G1_1.Models.Aderir", b =>
                {
                    b.Property<int>("IdAutenticado")
                        .HasColumnType("int")
                        .HasColumnName("Id_autenticado");

                    b.Property<int>("IdGrupo")
                        .HasColumnType("int")
                        .HasColumnName("Id_grupo");

                    b.Property<DateTime>("DataAdesao")
                        .HasColumnType("smalldatetime")
                        .HasColumnName("Data_adesao");

                    b.HasKey("IdAutenticado", "IdGrupo");

                    b.HasIndex("IdGrupo");

                    b.ToTable("Aderir");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Autenticado", b =>
                {
                    b.Property<int>("IdAutenticado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_autenticado");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAutenticado"));

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Nome_completo");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("IdAutenticado");

                    b.ToTable("Autenticado");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Comentar", b =>
                {
                    b.Property<int>("IdAutenticado")
                        .HasColumnType("int")
                        .HasColumnName("Id_autenticado");

                    b.Property<int>("IdPublicacao")
                        .HasColumnType("int")
                        .HasColumnName("Id_publicacao");

                    b.Property<DateTime>("DataComentario")
                        .HasColumnType("smalldatetime")
                        .HasColumnName("Data_comentario");

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)");

                    b.HasKey("IdAutenticado", "IdPublicacao", "DataComentario");

                    b.HasIndex("IdPublicacao");

                    b.ToTable("Comentar");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Ficheiro", b =>
                {
                    b.Property<int>("IdFicheiro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_ficheiro");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFicheiro"));

                    b.Property<int?>("IdAutenticado")
                        .HasColumnType("int")
                        .HasColumnName("id_autenticado");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.HasKey("IdFicheiro");

                    b.HasIndex("IdAutenticado");

                    b.ToTable("Ficheiros");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Grupo", b =>
                {
                    b.Property<int>("IdGrupo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_grupo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdGrupo"));

                    b.Property<bool>("Acesso")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("smalldatetime")
                        .HasColumnName("Data_criacao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("IdDono")
                        .HasColumnType("int")
                        .HasColumnName("Id_dono");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("IdGrupo");

                    b.HasIndex("IdDono");

                    b.ToTable("Grupo");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Midium", b =>
                {
                    b.Property<int>("IdMidia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_midia");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMidia"));

                    b.Property<int?>("IdPublicaco")
                        .HasColumnType("int")
                        .HasColumnName("Id_publicaco");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.HasKey("IdMidia");

                    b.HasIndex("IdPublicaco");

                    b.ToTable("Midia");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Partilha", b =>
                {
                    b.Property<int>("IdGrupo")
                        .HasColumnType("int")
                        .HasColumnName("Id_grupo");

                    b.Property<int>("IdFicheiro")
                        .HasColumnType("int")
                        .HasColumnName("id_ficheiro");

                    b.Property<DateTime>("DataPartilha")
                        .HasColumnType("date")
                        .HasColumnName("Data_partilha");

                    b.HasKey("IdGrupo", "IdFicheiro");

                    b.HasIndex("IdFicheiro");

                    b.ToTable("Partilha");
                });

            modelBuilder.Entity("PL1_G1_1.Models.PedirAcesso", b =>
                {
                    b.Property<int>("IdAutenticado")
                        .HasColumnType("int")
                        .HasColumnName("Id_autenticado");

                    b.Property<int>("IdGrupo")
                        .HasColumnType("int")
                        .HasColumnName("Id_grupo");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("smalldatetime")
                        .HasColumnName("Data_pedido");

                    b.Property<DateTime?>("DataResposta")
                        .HasColumnType("smalldatetime")
                        .HasColumnName("Data_resposta");

                    b.Property<string>("Mensagem")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<bool?>("Resultado")
                        .HasColumnType("bit")
                        .HasColumnName("resultado");

                    b.HasKey("IdAutenticado", "IdGrupo");

                    b.HasIndex("IdGrupo");

                    b.ToTable("Pedir_acesso");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Publicacao", b =>
                {
                    b.Property<int>("IdPublicacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_publicacao");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPublicacao"));

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DataPublicacao")
                        .HasColumnType("smalldatetime")
                        .HasColumnName("Data_publicacao");

                    b.Property<int>("IdAutor")
                        .HasColumnType("int")
                        .HasColumnName("Id_autor");

                    b.Property<string>("TipoPublicacao")
                        .IsRequired()
                        .HasMaxLength(7)
                        .IsUnicode(false)
                        .HasColumnType("varchar(7)")
                        .HasColumnName("Tipo_publicacao");

                    b.HasKey("IdPublicacao");

                    b.HasIndex("IdAutor");

                    b.ToTable("Publicacao");
                });

            modelBuilder.Entity("GrupoPublicacao", b =>
                {
                    b.HasOne("PL1_G1_1.Models.Grupo", null)
                        .WithMany()
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PL1_G1_1.Models.Publicacao", null)
                        .WithMany()
                        .HasForeignKey("IdPublicacao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PL1_G1_1.Models.Aderir", b =>
                {
                    b.HasOne("PL1_G1_1.Models.Autenticado", "IdAutenticadoNavigation")
                        .WithMany("Aderirs")
                        .HasForeignKey("IdAutenticado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PL1_G1_1.Models.Grupo", "IdGrupoNavigation")
                        .WithMany("Aderirs")
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdAutenticadoNavigation");

                    b.Navigation("IdGrupoNavigation");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Comentar", b =>
                {
                    b.HasOne("PL1_G1_1.Models.Autenticado", "IdAutenticadoNavigation")
                        .WithMany("Comentars")
                        .HasForeignKey("IdAutenticado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PL1_G1_1.Models.Publicacao", "IdPublicacaoNavigation")
                        .WithMany("Comentars")
                        .HasForeignKey("IdPublicacao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdAutenticadoNavigation");

                    b.Navigation("IdPublicacaoNavigation");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Ficheiro", b =>
                {
                    b.HasOne("PL1_G1_1.Models.Autenticado", "IdAutenticadoNavigation")
                        .WithMany("Ficheiros")
                        .HasForeignKey("IdAutenticado");

                    b.Navigation("IdAutenticadoNavigation");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Grupo", b =>
                {
                    b.HasOne("PL1_G1_1.Models.Autenticado", "IdDonoNavigation")
                        .WithMany("Grupos")
                        .HasForeignKey("IdDono")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdDonoNavigation");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Midium", b =>
                {
                    b.HasOne("PL1_G1_1.Models.Publicacao", "IdPublicacoNavigation")
                        .WithMany("Midia")
                        .HasForeignKey("IdPublicaco");

                    b.Navigation("IdPublicacoNavigation");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Partilha", b =>
                {
                    b.HasOne("PL1_G1_1.Models.Ficheiro", "IdFicheiroNavigation")
                        .WithMany("Partilhas")
                        .HasForeignKey("IdFicheiro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PL1_G1_1.Models.Grupo", "IdGrupoNavigation")
                        .WithMany("Partilhas")
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdFicheiroNavigation");

                    b.Navigation("IdGrupoNavigation");
                });

            modelBuilder.Entity("PL1_G1_1.Models.PedirAcesso", b =>
                {
                    b.HasOne("PL1_G1_1.Models.Autenticado", "IdAutenticadoNavigation")
                        .WithMany("PedirAcessos")
                        .HasForeignKey("IdAutenticado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PL1_G1_1.Models.Grupo", "IdGrupoNavigation")
                        .WithMany("PedirAcessos")
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdAutenticadoNavigation");

                    b.Navigation("IdGrupoNavigation");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Publicacao", b =>
                {
                    b.HasOne("PL1_G1_1.Models.Autenticado", "IdAutorNavigation")
                        .WithMany("Publicacaos")
                        .HasForeignKey("IdAutor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdAutorNavigation");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Autenticado", b =>
                {
                    b.Navigation("Aderirs");

                    b.Navigation("Comentars");

                    b.Navigation("Ficheiros");

                    b.Navigation("Grupos");

                    b.Navigation("PedirAcessos");

                    b.Navigation("Publicacaos");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Ficheiro", b =>
                {
                    b.Navigation("Partilhas");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Grupo", b =>
                {
                    b.Navigation("Aderirs");

                    b.Navigation("Partilhas");

                    b.Navigation("PedirAcessos");
                });

            modelBuilder.Entity("PL1_G1_1.Models.Publicacao", b =>
                {
                    b.Navigation("Comentars");

                    b.Navigation("Midia");
                });
#pragma warning restore 612, 618
        }
    }
}
