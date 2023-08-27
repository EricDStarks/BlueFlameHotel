using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlueFlameHotel.Migrations
{
    /// <inheritdoc />
    public partial class byggpoppa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_Hotel_HotelID",
                table: "HotelRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hotel",
                table: "Hotel");

            migrationBuilder.RenameTable(
                name: "Hotel",
                newName: "BlueFlameHotel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlueFlameHotel",
                table: "BlueFlameHotel",
                column: "ID");

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "A/C" });

            migrationBuilder.InsertData(
                table: "BlueFlameHotel",
                columns: new[] { "ID", "Address", "City", "Name", "Phone", "State" },
                values: new object[] { 1, "123 Sesame Street", "Houston", "Blue Flame Hotel", "281-330-8004", "TX" });

            migrationBuilder.InsertData(
                table: "RoomAmenities",
                columns: new[] { "ID", "AmenityID", "RoomsID" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "ID", "Layout", "Name", "RoomAmenitiesID" },
                values: new object[,]
                {
                    { 1, 0, "Basic Room", null },
                    { 2, 1, "Basic Single Room", null },
                    { 3, 2, "Basic Double Room", null }
                });

            migrationBuilder.InsertData(
                table: "HotelRooms",
                columns: new[] { "ID", "HotelID", "Price", "RoomID" },
                values: new object[] { 1, 1, 189.99000000000001, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_BlueFlameHotel_HotelID",
                table: "HotelRooms",
                column: "HotelID",
                principalTable: "BlueFlameHotel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_BlueFlameHotel_HotelID",
                table: "HotelRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlueFlameHotel",
                table: "BlueFlameHotel");

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HotelRooms",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomAmenities",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BlueFlameHotel",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "BlueFlameHotel",
                newName: "Hotel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hotel",
                table: "Hotel",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_Hotel_HotelID",
                table: "HotelRooms",
                column: "HotelID",
                principalTable: "Hotel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
