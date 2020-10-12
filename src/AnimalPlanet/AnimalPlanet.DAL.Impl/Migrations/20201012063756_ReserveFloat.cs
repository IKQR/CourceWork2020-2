using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalPlanet.DAL.Impl.Migrations
{
    public partial class ReserveFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Longitude",
                table: "Reserves",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                table: "Reserves",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Reserves",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Reserves",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
