using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meminisse.Application.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Memories",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Title = table.Column<string>(type: "TEXT", nullable: false),
                Description = table.Column<string>(type: "TEXT", nullable: false),
                Date = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Memories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "People",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                FullName = table.Column<string>(type: "TEXT", nullable: false),
                Bio = table.Column<string>(type: "TEXT", nullable: false),
                ProfilePictureUrl = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_People", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Tags",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tags", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MemoryItems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                MemoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                Content = table.Column<string>(type: "TEXT", nullable: false),
                DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                Type = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MemoryItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_MemoryItems_Memories_MemoryId",
                    column: x => x.MemoryId,
                    principalTable: "Memories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MemoryPerson",
            columns: table => new
            {
                MemoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                PersonId = table.Column<Guid>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MemoryPerson", x => new { x.MemoryId, x.PersonId });
                table.ForeignKey(
                    name: "FK_MemoryPerson_Memories_MemoryId",
                    column: x => x.MemoryId,
                    principalTable: "Memories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MemoryPerson_People_PersonId",
                    column: x => x.PersonId,
                    principalTable: "People",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MemoryTag",
            columns: table => new
            {
                MemoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                TagId = table.Column<Guid>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MemoryTag", x => new { x.MemoryId, x.TagId });
                table.ForeignKey(
                    name: "FK_MemoryTag_Memories_MemoryId",
                    column: x => x.MemoryId,
                    principalTable: "Memories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MemoryTag_Tags_TagId",
                    column: x => x.TagId,
                    principalTable: "Tags",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_MemoryItems_MemoryId",
            table: "MemoryItems",
            column: "MemoryId");

        migrationBuilder.CreateIndex(
            name: "IX_MemoryPerson_PersonId",
            table: "MemoryPerson",
            column: "PersonId");

        migrationBuilder.CreateIndex(
            name: "IX_MemoryTag_TagId",
            table: "MemoryTag",
            column: "TagId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MemoryItems");

        migrationBuilder.DropTable(
            name: "MemoryPerson");

        migrationBuilder.DropTable(
            name: "MemoryTag");

        migrationBuilder.DropTable(
            name: "People");

        migrationBuilder.DropTable(
            name: "Memories");

        migrationBuilder.DropTable(
            name: "Tags");
    }
}
