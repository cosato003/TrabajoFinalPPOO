using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class INITIAL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalAmmount = table.Column<float>(type: "REAL", nullable: false),
                    Discount = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Minibar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LiquorBottles = table.Column<int>(type: "INTEGER", nullable: false),
                    WaterBottles = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonalCareKits = table.Column<int>(type: "INTEGER", nullable: false),
                    Sodas = table.Column<int>(type: "INTEGER", nullable: false),
                    WineBottles = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minibar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    IdNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IdType = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    LoyaltyCode = table.Column<string>(type: "TEXT", nullable: true),
                    Discount = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.IdNumber);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomNumber = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Floor = table.Column<uint>(type: "INTEGER", nullable: false),
                    NightlyRate = table.Column<float>(type: "REAL", nullable: false),
                    Occupied = table.Column<bool>(type: "INTEGER", nullable: false),
                    Capacity = table.Column<uint>(type: "INTEGER", nullable: false),
                    GuestIdNumber = table.Column<string>(type: "TEXT", nullable: true),
                    OutDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CheckinDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    BedType = table.Column<int>(type: "INTEGER", nullable: true),
                    MinibarId = table.Column<int>(type: "INTEGER", nullable: true),
                    SimpleRoom_BedType = table.Column<int>(type: "INTEGER", nullable: true),
                    Suite_BedType = table.Column<int>(type: "INTEGER", nullable: true),
                    Bathrobes = table.Column<int>(type: "INTEGER", nullable: true),
                    Suite_MinibarId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomNumber);
                    table.ForeignKey(
                        name: "FK_Room_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Room_Minibar_MinibarId",
                        column: x => x.MinibarId,
                        principalTable: "Minibar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_Minibar_Suite_MinibarId",
                        column: x => x.Suite_MinibarId,
                        principalTable: "Minibar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_Person_GuestIdNumber",
                        column: x => x.GuestIdNumber,
                        principalTable: "Person",
                        principalColumn: "IdNumber");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestIdNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    UnitCost = table.Column<float>(type: "REAL", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalCost = table.Column<float>(type: "REAL", nullable: false),
                    When = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomNumber = table.Column<uint>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Person_GuestIdNumber",
                        column: x => x.GuestIdNumber,
                        principalTable: "Person",
                        principalColumn: "IdNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Room_RoomNumber",
                        column: x => x.RoomNumber,
                        principalTable: "Room",
                        principalColumn: "RoomNumber");
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationNumber = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReservedRoomRoomNumber = table.Column<uint>(type: "INTEGER", nullable: false),
                    ReservedGuestIdNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationNumber);
                    table.ForeignKey(
                        name: "FK_Reservation_Person_ReservedGuestIdNumber",
                        column: x => x.ReservedGuestIdNumber,
                        principalTable: "Person",
                        principalColumn: "IdNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Room_ReservedRoomRoomNumber",
                        column: x => x.ReservedRoomRoomNumber,
                        principalTable: "Room",
                        principalColumn: "RoomNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_GuestIdNumber",
                table: "InvoiceItem",
                column: "GuestIdNumber");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_RoomNumber",
                table: "InvoiceItem",
                column: "RoomNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ReservedGuestIdNumber",
                table: "Reservation",
                column: "ReservedGuestIdNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ReservedRoomRoomNumber",
                table: "Reservation",
                column: "ReservedRoomRoomNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Room_GuestIdNumber",
                table: "Room",
                column: "GuestIdNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Room_InvoiceId",
                table: "Room",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_MinibarId",
                table: "Room",
                column: "MinibarId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Suite_MinibarId",
                table: "Room",
                column: "Suite_MinibarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Minibar");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
