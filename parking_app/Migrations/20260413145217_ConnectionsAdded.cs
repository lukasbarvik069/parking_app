using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace parking_app.Migrations
{
    /// <inheritdoc />
    public partial class ConnectionsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f9fdb71a-0915-4cbd-85b8-6c580bff797f", "AQAAAAIAAYagAAAAEKVpnlXUPcajsgF8dAyxLWOwV1KDDpDdk5PaFkU0xn+ttXRzX5dvFHWkScEcQqtBTg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a3f2e896-d0d6-44b2-b121-0194cbec3974", "AQAAAAIAAYagAAAAEOwAgdTvJchd/t8TewzwgR3gZ6eTGXUXgEqH1CmvF+7+dgPG5kRL+wAQjY+XTxSBPA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "233de932-b05f-419a-91f1-2b3feb25661f", "AQAAAAIAAYagAAAAEMycgwEaHdZyxbzMQpOAtfLUmKXpg47jj1F99ltzUENIyan7hyfkpUl6IzJUF4dy8A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b2478b44-76d6-4418-a245-3ff9b5f81f3a", "AQAAAAIAAYagAAAAENZDUlgxYUAtzOcM9HL0Rp0nqxUbRSTKxgHJyctMPRtmZx9aQIo+GimC3mLNbeKQFg==" });
        }
    }
}
