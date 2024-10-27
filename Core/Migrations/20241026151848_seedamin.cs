using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class seedamin : Migration
	{
        /// <inheritdoc />
         protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])\r\n      VALUES" +
				" ('6341cd82-0384-4758-863c-867cf3ed516e',0,'4c052e37-41c8-4226-a606-18bf45e9b3e6', 'admin@admin.com',0,1,NULL, 'ADMIN@ADMIN.COM','ADMIN@ADMIN.COM', 'AQAAAAIAAYagAAAAEJp++2CicIO5WP1kDUc3DOH0LvY+pWqfyvfkcrP6F+JUw672UEgkWG42BseIQu0Fgw==','01018903943',0,'PW3QVZ7IUISHU54AHIKVQMCPYNFLFMRO', 0, 'admin@admin.com');\r\n  "
				+ "    INSERT INTO [Admin] ([Id], [FirstName], [LastName])\r\n      VALUES ('6341cd82-0384-4758-863c-867cf3ed516e', 'Mohamed', 'Elsayed'); "
				+ "INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])\r\n      VALUES ('a8cc6873-26a4-4ab7-9ccf-0db3de4c4471', '6341cd82-0384-4758-863c-867cf3ed516e'); ")
				;
		}
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DELETE FROM [AspNetUserRoles] WHERE [UserId] = '6341cd82-0384-4758-863c-867cf3ed516e';");
			migrationBuilder.Sql("DELETE FROM [Admin] WHERE [Id] = '6341cd82-0384-4758-863c-867cf3ed516e';");
			migrationBuilder.Sql("DELETE FROM [AspNetUsers] WHERE [Id] = '6341cd82-0384-4758-863c-867cf3ed516e';");
		}
	}
}

