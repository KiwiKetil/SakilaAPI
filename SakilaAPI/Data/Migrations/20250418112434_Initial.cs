using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SakilaAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "actor",
                columns: table => new
                {
                    actor_id = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.actor_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    category_id = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.category_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    country_id = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    country = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.country_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "film_text",
                columns: table => new
                {
                    film_id = table.Column<short>(type: "smallint", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.film_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "language",
                columns: table => new
                {
                    language_id = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "char(20)", fixedLength: true, maxLength: 20, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.language_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    city_id = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    city = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.city_id);
                    table.ForeignKey(
                        name: "fk_city_country",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "country_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "film",
                columns: table => new
                {
                    film_id = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    release_year = table.Column<short>(type: "year", nullable: true),
                    language_id = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    original_language_id = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    rental_duration = table.Column<byte>(type: "tinyint unsigned", nullable: false, defaultValueSql: "'3'"),
                    rental_rate = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false, defaultValueSql: "'4.99'"),
                    length = table.Column<ushort>(type: "smallint unsigned", nullable: true),
                    replacement_cost = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, defaultValueSql: "'19.99'"),
                    rating = table.Column<string>(type: "enum('G','PG','PG-13','R','NC-17')", nullable: true, defaultValueSql: "'G'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.film_id);
                    table.ForeignKey(
                        name: "fk_film_language",
                        column: x => x.language_id,
                        principalTable: "language",
                        principalColumn: "language_id");
                    table.ForeignKey(
                        name: "fk_film_language_original",
                        column: x => x.original_language_id,
                        principalTable: "language",
                        principalColumn: "language_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    address_id = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    address = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address2 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    district = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    postal_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.address_id);
                    table.ForeignKey(
                        name: "fk_address_city",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "city_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "film_actor",
                columns: table => new
                {
                    actor_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    film_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.actor_id, x.film_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_film_actor_actor",
                        column: x => x.actor_id,
                        principalTable: "actor",
                        principalColumn: "actor_id");
                    table.ForeignKey(
                        name: "fk_film_actor_film",
                        column: x => x.film_id,
                        principalTable: "film",
                        principalColumn: "film_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "film_category",
                columns: table => new
                {
                    film_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    category_id = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.film_id, x.category_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_film_category_category",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "fk_film_category_film",
                        column: x => x.film_id,
                        principalTable: "film",
                        principalColumn: "film_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customer_id = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    store_id = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    first_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.customer_id);
                    table.ForeignKey(
                        name: "fk_customer_address",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "address_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "inventory",
                columns: table => new
                {
                    inventory_id = table.Column<uint>(type: "mediumint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    film_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    store_id = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.inventory_id);
                    table.ForeignKey(
                        name: "fk_inventory_film",
                        column: x => x.film_id,
                        principalTable: "film",
                        principalColumn: "film_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    payment_id = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    staff_id = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    rental_id = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.payment_id);
                    table.ForeignKey(
                        name: "fk_payment_customer",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customer_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "rental",
                columns: table => new
                {
                    rental_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    rental_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    inventory_id = table.Column<uint>(type: "mediumint unsigned", nullable: false),
                    customer_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    return_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    staff_id = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.rental_id);
                    table.ForeignKey(
                        name: "fk_rental_customer",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "fk_rental_inventory",
                        column: x => x.inventory_id,
                        principalTable: "inventory",
                        principalColumn: "inventory_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    staff_id = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    picture = table.Column<byte[]>(type: "blob", nullable: true),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    store_id = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    username = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, collation: "utf8mb4_bin")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.staff_id);
                    table.ForeignKey(
                        name: "fk_staff_address",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "address_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "store",
                columns: table => new
                {
                    store_id = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    manager_staff_id = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    address_id = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.store_id);
                    table.ForeignKey(
                        name: "fk_store_address",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "address_id");
                    table.ForeignKey(
                        name: "fk_store_staff",
                        column: x => x.manager_staff_id,
                        principalTable: "staff",
                        principalColumn: "staff_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "idx_actor_last_name",
                table: "actor",
                column: "last_name");

            migrationBuilder.CreateIndex(
                name: "idx_fk_city_id",
                table: "address",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_country_id",
                table: "city",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_address_id",
                table: "customer",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_store_id",
                table: "customer",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "idx_last_name",
                table: "customer",
                column: "last_name");

            migrationBuilder.CreateIndex(
                name: "idx_fk_language_id",
                table: "film",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_original_language_id",
                table: "film",
                column: "original_language_id");

            migrationBuilder.CreateIndex(
                name: "idx_title",
                table: "film",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "idx_fk_film_id",
                table: "film_actor",
                column: "film_id");

            migrationBuilder.CreateIndex(
                name: "fk_film_category_category",
                table: "film_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "idx_title_description",
                table: "film_text",
                columns: new[] { "title", "description" })
                .Annotation("MySql:FullTextIndex", true);

            migrationBuilder.CreateIndex(
                name: "idx_fk_film_id1",
                table: "inventory",
                column: "film_id");

            migrationBuilder.CreateIndex(
                name: "idx_store_id_film_id",
                table: "inventory",
                columns: new[] { "store_id", "film_id" });

            migrationBuilder.CreateIndex(
                name: "fk_payment_rental",
                table: "payment",
                column: "rental_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_customer_id",
                table: "payment",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_staff_id",
                table: "payment",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_customer_id1",
                table: "rental",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_inventory_id",
                table: "rental",
                column: "inventory_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_staff_id1",
                table: "rental",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "rental_date",
                table: "rental",
                columns: new[] { "rental_date", "inventory_id", "customer_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_fk_address_id1",
                table: "staff",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_store_id1",
                table: "staff",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "idx_fk_address_id2",
                table: "store",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "idx_unique_manager",
                table: "store",
                column: "manager_staff_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_customer_store",
                table: "customer",
                column: "store_id",
                principalTable: "store",
                principalColumn: "store_id");

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_store",
                table: "inventory",
                column: "store_id",
                principalTable: "store",
                principalColumn: "store_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_rental",
                table: "payment",
                column: "rental_id",
                principalTable: "rental",
                principalColumn: "rental_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_payment_staff",
                table: "payment",
                column: "staff_id",
                principalTable: "staff",
                principalColumn: "staff_id");

            migrationBuilder.AddForeignKey(
                name: "fk_rental_staff",
                table: "rental",
                column: "staff_id",
                principalTable: "staff",
                principalColumn: "staff_id");

            migrationBuilder.AddForeignKey(
                name: "fk_staff_store",
                table: "staff",
                column: "store_id",
                principalTable: "store",
                principalColumn: "store_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_address_city",
                table: "address");

            migrationBuilder.DropForeignKey(
                name: "fk_staff_address",
                table: "staff");

            migrationBuilder.DropForeignKey(
                name: "fk_store_address",
                table: "store");

            migrationBuilder.DropForeignKey(
                name: "fk_staff_store",
                table: "staff");

            migrationBuilder.DropTable(
                name: "film_actor");

            migrationBuilder.DropTable(
                name: "film_category");

            migrationBuilder.DropTable(
                name: "film_text");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "actor");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "rental");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "film");

            migrationBuilder.DropTable(
                name: "language");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "store");

            migrationBuilder.DropTable(
                name: "staff");
        }
    }
}
