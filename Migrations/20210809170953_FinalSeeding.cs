using Microsoft.EntityFrameworkCore.Migrations;

namespace net.core.api.Migrations
{
  public partial class FinalSeeding : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Skills",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(type: "TEXT", nullable: true),
            Damage = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Skills", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Username = table.Column<string>(type: "TEXT", nullable: true),
            PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: true),
            PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: true),
            Role = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Player")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Users", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Characters",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(type: "TEXT", nullable: true),
            HitPoints = table.Column<int>(type: "INTEGER", nullable: false),
            Strength = table.Column<int>(type: "INTEGER", nullable: false),
            Defense = table.Column<int>(type: "INTEGER", nullable: false),
            Intelligence = table.Column<int>(type: "INTEGER", nullable: false),
            Class = table.Column<int>(type: "INTEGER", nullable: false),
            UserId = table.Column<int>(type: "INTEGER", nullable: false),
            Fights = table.Column<int>(type: "INTEGER", nullable: false),
            Victories = table.Column<int>(type: "INTEGER", nullable: false),
            Defeats = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Characters", x => x.Id);
            table.ForeignKey(
                      name: "FK_Characters_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "CharacterSkill",
          columns: table => new
          {
            CharactersId = table.Column<int>(type: "INTEGER", nullable: false),
            SkillsId = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_CharacterSkill", x => new { x.CharactersId, x.SkillsId });
            table.ForeignKey(
                      name: "FK_CharacterSkill_Characters_CharactersId",
                      column: x => x.CharactersId,
                      principalTable: "Characters",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_CharacterSkill_Skills_SkillsId",
                      column: x => x.SkillsId,
                      principalTable: "Skills",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Weapons",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(type: "TEXT", nullable: true),
            Damage = table.Column<int>(type: "INTEGER", nullable: false),
            CharacterId = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Weapons", x => x.Id);
            table.ForeignKey(
                      name: "FK_Weapons_Characters_CharacterId",
                      column: x => x.CharacterId,
                      principalTable: "Characters",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.InsertData(
          table: "Skills",
          columns: new[] { "Id", "Damage", "Name" },
          values: new object[] { 1, 100, "Force" });

      migrationBuilder.InsertData(
          table: "Skills",
          columns: new[] { "Id", "Damage", "Name" },
          values: new object[] { 2, 110, "Dark Rays" });

      migrationBuilder.InsertData(
          table: "Skills",
          columns: new[] { "Id", "Damage", "Name" },
          values: new object[] { 3, 80, "Push" });

      migrationBuilder.InsertData(
          table: "Users",
          columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Username" },
          values: new object[] { 1, new byte[] { 54, 0, 226, 182, 224, 42, 83, 174, 112, 24, 213, 234, 142, 28, 100, 154, 55, 247, 19, 218, 203, 250, 100, 69, 223, 176, 180, 195, 183, 32, 232, 219, 51, 163, 57, 6, 227, 3, 187, 214, 220, 76, 218, 185, 120, 31, 180, 57, 130, 212, 199, 123, 73, 155, 163, 238, 241, 129, 176, 160, 158, 30, 149, 98 }, new byte[] { 100, 11, 88, 176, 142, 103, 22, 142, 148, 164, 53, 246, 193, 134, 37, 20, 125, 51, 11, 127, 215, 6, 16, 128, 121, 180, 209, 17, 68, 114, 203, 99, 146, 86, 94, 148, 131, 135, 226, 102, 209, 83, 205, 119, 218, 214, 212, 50, 224, 12, 164, 152, 134, 250, 135, 238, 183, 249, 41, 168, 151, 100, 82, 177, 196, 132, 45, 2, 31, 118, 206, 199, 141, 183, 180, 72, 163, 45, 73, 178, 182, 248, 228, 225, 112, 142, 50, 67, 28, 126, 61, 219, 141, 232, 178, 7, 68, 124, 148, 150, 16, 139, 168, 40, 5, 89, 185, 80, 249, 87, 68, 202, 102, 210, 198, 96, 252, 76, 84, 169, 103, 243, 175, 242, 174, 160, 40, 105 }, "User1" });

      migrationBuilder.InsertData(
          table: "Users",
          columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Username" },
          values: new object[] { 2, new byte[] { 54, 0, 226, 182, 224, 42, 83, 174, 112, 24, 213, 234, 142, 28, 100, 154, 55, 247, 19, 218, 203, 250, 100, 69, 223, 176, 180, 195, 183, 32, 232, 219, 51, 163, 57, 6, 227, 3, 187, 214, 220, 76, 218, 185, 120, 31, 180, 57, 130, 212, 199, 123, 73, 155, 163, 238, 241, 129, 176, 160, 158, 30, 149, 98 }, new byte[] { 100, 11, 88, 176, 142, 103, 22, 142, 148, 164, 53, 246, 193, 134, 37, 20, 125, 51, 11, 127, 215, 6, 16, 128, 121, 180, 209, 17, 68, 114, 203, 99, 146, 86, 94, 148, 131, 135, 226, 102, 209, 83, 205, 119, 218, 214, 212, 50, 224, 12, 164, 152, 134, 250, 135, 238, 183, 249, 41, 168, 151, 100, 82, 177, 196, 132, 45, 2, 31, 118, 206, 199, 141, 183, 180, 72, 163, 45, 73, 178, 182, 248, 228, 225, 112, 142, 50, 67, 28, 126, 61, 219, 141, 232, 178, 7, 68, 124, 148, 150, 16, 139, 168, 40, 5, 89, 185, 80, 249, 87, 68, 202, 102, 210, 198, 96, 252, 76, 84, 169, 103, 243, 175, 242, 174, 160, 40, 105 }, "User2" });

      migrationBuilder.InsertData(
          table: "Characters",
          columns: new[] { "Id", "Class", "Defeats", "Defense", "Fights", "HitPoints", "Intelligence", "Name", "Strength", "UserId", "Victories" },
          values: new object[] { 1, 0, 0, 120, 0, 1250, 125, "Obi wan", 90, 1, 0 });

      migrationBuilder.InsertData(
          table: "Characters",
          columns: new[] { "Id", "Class", "Defeats", "Defense", "Fights", "HitPoints", "Intelligence", "Name", "Strength", "UserId", "Victories" },
          values: new object[] { 2, 1, 0, 100, 0, 1100, 112, "Darth Vader", 110, 2, 0 });

      migrationBuilder.InsertData(
          table: "Weapons",
          columns: new[] { "Id", "CharacterId", "Damage", "Name" },
          values: new object[] { 1, 1, 105, "Dark Saber" });

      migrationBuilder.InsertData(
          table: "Weapons",
          columns: new[] { "Id", "CharacterId", "Damage", "Name" },
          values: new object[] { 2, 2, 100, "Red lightsaber" });

      migrationBuilder.CreateIndex(
          name: "IX_Characters_UserId",
          table: "Characters",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_CharacterSkill_SkillsId",
          table: "CharacterSkill",
          column: "SkillsId");

      migrationBuilder.CreateIndex(
          name: "IX_Weapons_CharacterId",
          table: "Weapons",
          column: "CharacterId",
          unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "CharacterSkill");

      migrationBuilder.DropTable(
          name: "Weapons");

      migrationBuilder.DropTable(
          name: "Skills");

      migrationBuilder.DropTable(
          name: "Characters");

      migrationBuilder.DropTable(
          name: "Users");
    }
  }
}
