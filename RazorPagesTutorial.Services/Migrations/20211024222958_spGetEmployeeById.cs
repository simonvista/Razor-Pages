using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPagesTutorial.Services.Migrations
{
    public partial class spGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"create procedure spGetEmployeeById
                                @Id int
                                as
                                begin
	                                select * from employees 
	                                where Id=@Id
                                end";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"drop procedure spGetEmployeeById";
            migrationBuilder.Sql(procedure);
        }
    }
}
