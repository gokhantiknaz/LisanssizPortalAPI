using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CariKart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adi = table.Column<string>(type: "text", nullable: false),
                    Soyadi = table.Column<string>(type: "text", nullable: false),
                    Unvan = table.Column<string>(type: "text", nullable: true),
                    Tckn = table.Column<long>(type: "bigint", nullable: true),
                    Vkn = table.Column<long>(type: "bigint", nullable: true),
                    Durum = table.Column<int>(type: "integer", nullable: false),
                    GercekTuzel = table.Column<int>(type: "integer", nullable: false),
                    OzelkodId1 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId2 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId3 = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CariKart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "letisim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CepTel = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Ilid = table.Column<int>(type: "integer", nullable: true),
                    Ilceid = table.Column<int>(type: "integer", nullable: true),
                    Adres = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_letisim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusteriOzelKod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RootId = table.Column<int>(type: "integer", nullable: false),
                    KodNo = table.Column<int>(type: "integer", nullable: false),
                    Kod = table.Column<string>(type: "text", nullable: false),
                    Ad = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriOzelKod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    EmailId = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musteri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adi = table.Column<string>(type: "text", nullable: false),
                    Soyadi = table.Column<string>(type: "text", nullable: false),
                    Unvan = table.Column<string>(type: "text", nullable: true),
                    CariKartId = table.Column<int>(type: "integer", nullable: false),
                    Tckn = table.Column<long>(type: "bigint", nullable: true),
                    Vkn = table.Column<long>(type: "bigint", nullable: true),
                    Durum = table.Column<int>(type: "integer", nullable: false),
                    GercekTuzel = table.Column<int>(type: "integer", nullable: false),
                    OzelkodId1 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId2 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId3 = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musteri_CariKart_CariKartId",
                        column: x => x.CariKartId,
                        principalTable: "CariKart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    Tarife = table.Column<int>(type: "integer", nullable: false),
                    EtsoKodu = table.Column<string>(type: "text", nullable: false),
                    DagitimFirmaId = table.Column<int>(type: "integer", nullable: false),
                    SeriNo = table.Column<long>(type: "bigint", nullable: false),
                    SozlesmeGucu = table.Column<double>(type: "double precision", nullable: false),
                    BaglantiGucu = table.Column<double>(type: "double precision", nullable: false),
                    KuruluGuc = table.Column<double>(type: "double precision", nullable: false),
                    SahisTip = table.Column<int>(type: "integer", nullable: false),
                    Terim = table.Column<int>(type: "integer", nullable: false),
                    Agog = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abone_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusteriIletisim",
                columns: table => new
                {
                    IletisimId = table.Column<int>(type: "integer", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriIletisim", x => new { x.MusteriId, x.IletisimId });
                    table.ForeignKey(
                        name: "FK_MusteriIletisim_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusteriIletisim_letisim_IletisimId",
                        column: x => x.IletisimId,
                        principalTable: "letisim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboneIletisim",
                columns: table => new
                {
                    IletisimId = table.Column<int>(type: "integer", nullable: false),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneIletisim", x => new { x.AboneId, x.IletisimId });
                    table.ForeignKey(
                        name: "FK_AboneIletisim_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AboneIletisim_letisim_IletisimId",
                        column: x => x.IletisimId,
                        principalTable: "letisim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboneSayac",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    SayacNo = table.Column<long>(type: "bigint", nullable: false),
                    Marka = table.Column<string>(type: "text", nullable: false),
                    FazAdedi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneSayac", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboneSayac_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboneTuketici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    UreticiAboneId = table.Column<int>(type: "integer", nullable: false),
                    BaslamaZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Durum = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneTuketici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboneTuketici_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AboneTuketici_Abone_UreticiAboneId",
                        column: x => x.UreticiAboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboneUretici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    UretimSekli = table.Column<int>(type: "integer", nullable: false),
                    LisansBilgisi = table.Column<int>(type: "integer", nullable: false),
                    UretimBaslama = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CagrimektupTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MahsupTipi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneUretici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboneUretici_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abone_MusteriId",
                table: "Abone",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneIletisim_IletisimId",
                table: "AboneIletisim",
                column: "IletisimId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneSayac_AboneId",
                table: "AboneSayac",
                column: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneTuketici_AboneId",
                table: "AboneTuketici",
                column: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneTuketici_UreticiAboneId",
                table: "AboneTuketici",
                column: "UreticiAboneId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneUretici_AboneId",
                table: "AboneUretici",
                column: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_Musteri_CariKartId",
                table: "Musteri",
                column: "CariKartId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriIletisim_IletisimId",
                table: "MusteriIletisim",
                column: "IletisimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboneIletisim");

            migrationBuilder.DropTable(
                name: "AboneSayac");

            migrationBuilder.DropTable(
                name: "AboneTuketici");

            migrationBuilder.DropTable(
                name: "AboneUretici");

            migrationBuilder.DropTable(
                name: "MusteriIletisim");

            migrationBuilder.DropTable(
                name: "MusteriOzelKod");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Abone");

            migrationBuilder.DropTable(
                name: "letisim");

            migrationBuilder.DropTable(
                name: "Musteri");

            migrationBuilder.DropTable(
                name: "CariKart");
        }
    }
}
