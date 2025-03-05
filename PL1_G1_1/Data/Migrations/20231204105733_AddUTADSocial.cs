using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PL1_G1_1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUTADSocial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autenticado",
                columns: table => new
                {
                    id_autenticado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Nome_completo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autenticado", x => x.id_autenticado);
                });

            migrationBuilder.CreateTable(
                name: "Ficheiros",
                columns: table => new
                {
                    Id_ficheiro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    id_autenticado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ficheiros", x => x.Id_ficheiro);
                    table.ForeignKey(
                        name: "FK_Ficheiros_Autenticado_id_autenticado",
                        column: x => x.id_autenticado,
                        principalTable: "Autenticado",
                        principalColumn: "id_autenticado");
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    Id_grupo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_dono = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Acesso = table.Column<bool>(type: "bit", nullable: false),
                    Data_criacao = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.Id_grupo);
                    table.ForeignKey(
                        name: "FK_Grupo_Autenticado_Id_dono",
                        column: x => x.Id_dono,
                        principalTable: "Autenticado",
                        principalColumn: "id_autenticado"
                       );
                });

            migrationBuilder.CreateTable(
                name: "Publicacao",
                columns: table => new
                {
                    Id_publicacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_autor = table.Column<int>(type: "int", nullable: false),
                    Data_publicacao = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Tipo_publicacao = table.Column<string>(type: "varchar(7)", unicode: false, maxLength: 7, nullable: false),
                    Conteudo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicacao", x => x.Id_publicacao);
                    table.ForeignKey(
                        name: "FK_Publicacao_Autenticado_Id_autor",
                        column: x => x.Id_autor,
                        principalTable: "Autenticado",
                        principalColumn: "id_autenticado");
                });

            migrationBuilder.CreateTable(
                name: "Aderir",
                columns: table => new
                {
                    Id_autenticado = table.Column<int>(type: "int", nullable: false),
                    Id_grupo = table.Column<int>(type: "int", nullable: false),
                    Data_adesao = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aderir", x => new { x.Id_autenticado, x.Id_grupo });
                    table.ForeignKey(
                        name: "FK_Aderir_Autenticado_Id_autenticado",
                        column: x => x.Id_autenticado,
                        principalTable: "Autenticado",
                        principalColumn: "id_autenticado");
                    table.ForeignKey(
                        name: "FK_Aderir_Grupo_Id_grupo",
                        column: x => x.Id_grupo,
                        principalTable: "Grupo",
                        principalColumn: "Id_grupo");
                });

            migrationBuilder.CreateTable(
                name: "Partilha",
                columns: table => new
                {
                    Id_grupo = table.Column<int>(type: "int", nullable: false),
                    id_ficheiro = table.Column<int>(type: "int", nullable: false),
                    Data_partilha = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partilha", x => new { x.Id_grupo, x.id_ficheiro });
                    table.ForeignKey(
                        name: "FK_Partilha_Ficheiros_id_ficheiro",
                        column: x => x.id_ficheiro,
                        principalTable: "Ficheiros",
                        principalColumn: "Id_ficheiro");
                    table.ForeignKey(
                        name: "FK_Partilha_Grupo_Id_grupo",
                        column: x => x.Id_grupo,
                        principalTable: "Grupo",
                        principalColumn: "Id_grupo");
                });

            migrationBuilder.CreateTable(
                name: "Pedir_acesso",
                columns: table => new
                {
                    Id_autenticado = table.Column<int>(type: "int", nullable: false),
                    Id_grupo = table.Column<int>(type: "int", nullable: false),
                    Data_resposta = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Data_pedido = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    resultado = table.Column<bool>(type: "bit", nullable: true),
                    Mensagem = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedir_acesso", x => new { x.Id_autenticado, x.Id_grupo });
                    table.ForeignKey(
                        name: "FK_Pedir_acesso_Autenticado_Id_autenticado",
                        column: x => x.Id_autenticado,
                        principalTable: "Autenticado",
                        principalColumn: "id_autenticado");
                    table.ForeignKey(
                        name: "FK_Pedir_acesso_Grupo_Id_grupo",
                        column: x => x.Id_grupo,
                        principalTable: "Grupo",
                        principalColumn: "Id_grupo");
                });

            migrationBuilder.CreateTable(
                name: "Comentar",
                columns: table => new
                {
                    Id_autenticado = table.Column<int>(type: "int", nullable: false),
                    Id_publicacao = table.Column<int>(type: "int", nullable: false),
                    Data_comentario = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Conteudo = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentar", x => new { x.Id_autenticado, x.Id_publicacao, x.Data_comentario });
                    table.ForeignKey(
                        name: "FK_Comentar_Autenticado_Id_autenticado",
                        column: x => x.Id_autenticado,
                        principalTable: "Autenticado",
                        principalColumn: "id_autenticado");
                    table.ForeignKey(
                        name: "FK_Comentar_Publicacao_Id_publicacao",
                        column: x => x.Id_publicacao,
                        principalTable: "Publicacao",
                        principalColumn: "Id_publicacao");
                });

            migrationBuilder.CreateTable(
                name: "GrupoPublicacao",
                columns: table => new
                {
                    IdGrupo = table.Column<int>(type: "int", nullable: false),
                    IdPublicacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoPublicacao", x => new { x.IdGrupo, x.IdPublicacao });
                    table.ForeignKey(
                        name: "FK_GrupoPublicacao_Grupo_IdGrupo",
                        column: x => x.IdGrupo,
                        principalTable: "Grupo",
                        principalColumn: "Id_grupo");
                    table.ForeignKey(
                        name: "FK_GrupoPublicacao_Publicacao_IdPublicacao",
                        column: x => x.IdPublicacao,
                        principalTable: "Publicacao",
                        principalColumn: "Id_publicacao");
                });

            migrationBuilder.CreateTable(
                name: "Midia",
                columns: table => new
                {
                    Id_midia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    Id_publicaco = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Midia", x => x.Id_midia);
                    table.ForeignKey(
                        name: "FK_Midia_Publicacao_Id_publicaco",
                        column: x => x.Id_publicaco,
                        principalTable: "Publicacao",
                        principalColumn: "Id_publicacao");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aderir_Id_grupo",
                table: "Aderir",
                column: "Id_grupo");

            migrationBuilder.CreateIndex(
                name: "IX_Comentar_Id_publicacao",
                table: "Comentar",
                column: "Id_publicacao");

            migrationBuilder.CreateIndex(
                name: "IX_Ficheiros_id_autenticado",
                table: "Ficheiros",
                column: "id_autenticado");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_Id_dono",
                table: "Grupo",
                column: "Id_dono");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoPublicacao_IdPublicacao",
                table: "GrupoPublicacao",
                column: "IdPublicacao");

            migrationBuilder.CreateIndex(
                name: "IX_Midia_Id_publicaco",
                table: "Midia",
                column: "Id_publicaco");

            migrationBuilder.CreateIndex(
                name: "IX_Partilha_id_ficheiro",
                table: "Partilha",
                column: "id_ficheiro");

            migrationBuilder.CreateIndex(
                name: "IX_Pedir_acesso_Id_grupo",
                table: "Pedir_acesso",
                column: "Id_grupo");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacao_Id_autor",
                table: "Publicacao",
                column: "Id_autor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aderir");

            migrationBuilder.DropTable(
                name: "Comentar");

            migrationBuilder.DropTable(
                name: "GrupoPublicacao");

            migrationBuilder.DropTable(
                name: "Midia");

            migrationBuilder.DropTable(
                name: "Partilha");

            migrationBuilder.DropTable(
                name: "Pedir_acesso");

            migrationBuilder.DropTable(
                name: "Publicacao");

            migrationBuilder.DropTable(
                name: "Ficheiros");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Autenticado");
        }
    }
}
