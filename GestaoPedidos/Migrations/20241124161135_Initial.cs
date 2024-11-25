using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestaoPedidos.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    cpf = table.Column<string>(type: "varchar(14)", nullable: false),
                    datedelete = table.Column<DateTime>(name: "date_delete", type: "timestamp with time zone", nullable: true),
                    persontype = table.Column<string>(name: "person_type", type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: true),
                    login = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    sku = table.Column<string>(type: "text", nullable: false),
                    datedelete = table.Column<DateTime>(name: "date_delete", type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    addresstype = table.Column<int>(name: "address_type", type: "integer", nullable: false),
                    logradouro = table.Column<string>(type: "text", nullable: false),
                    numberaddress = table.Column<string>(name: "number_address", type: "text", nullable: false),
                    complement = table.Column<string>(type: "text", nullable: false),
                    district = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    state = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cep = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false),
                    datedelete = table.Column<DateTime>(name: "date_delete", type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_people_client_id",
                        column: x => x.clientid,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false),
                    billingaddressid = table.Column<int>(name: "billing_address_id", type: "integer", nullable: false),
                    deliveryaddressid = table.Column<int>(name: "delivery_address_id", type: "integer", nullable: false),
                    orderdate = table.Column<DateTime>(name: "order_date", type: "timestamp with time zone", nullable: false),
                    datedelete = table.Column<DateTime>(name: "date_delete", type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_address_billing_address_id",
                        column: x => x.billingaddressid,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_address_delivery_address_id",
                        column: x => x.deliveryaddressid,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_people_client_id",
                        column: x => x.clientid,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    unitprice = table.Column<double>(name: "unit_price", type: "double precision", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    orderid = table.Column<int>(name: "order_id", type: "integer", nullable: false),
                    productid = table.Column<int>(name: "product_id", type: "integer", nullable: false),
                    datedelete = table.Column<DateTime>(name: "date_delete", type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_item_order_order_id",
                        column: x => x.orderid,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_item_product_product_id",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_client_id",
                table: "address",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_billing_address_id",
                table: "order",
                column: "billing_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_client_id",
                table: "order",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_delivery_address_id",
                table: "order",
                column: "delivery_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_order_id",
                table: "order_item",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_product_id",
                table: "order_item",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "people");
        }
    }
}
