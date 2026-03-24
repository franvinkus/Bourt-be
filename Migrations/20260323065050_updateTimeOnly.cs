using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bourt.Migrations
{
    /// <inheritdoc />
    public partial class updateTimeOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Places\" ALTER COLUMN \"OpenHour\" TYPE time without time zone USING \"OpenHour\"::time without time zone;");

            migrationBuilder.Sql("ALTER TABLE \"Places\" ALTER COLUMN \"CloseHour\" TYPE time without time zone USING \"CloseHour\"::time without time zone;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Places\" ALTER COLUMN \"OpenHour\" TYPE text USING \"OpenHour\"::text;");

            migrationBuilder.Sql("ALTER TABLE \"Places\" ALTER COLUMN \"CloseHour\" TYPE text USING \"CloseHour\"::text;");
        }
    }
}
