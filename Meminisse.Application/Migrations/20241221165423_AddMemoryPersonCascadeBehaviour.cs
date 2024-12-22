using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meminisse.Application.Migrations;

/// <inheritdoc />
public partial class AddMemoryPersonCascadeBehaviour : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MemoryTag");

        migrationBuilder.AddColumn<DateTime>(
            name: "Birthday",
            table: "People",
            type: "datetime",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "Gender",
            table: "People",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DateAdded",
            table: "MemoryItems",
            type: "datetime",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<DateTime>(
            name: "Date",
            table: "Memories",
            type: "datetime",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "TEXT");

        migrationBuilder.AddColumn<int>(
            name: "Emotion",
            table: "Memories",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "MemoryTags",
            columns: table => new
            {
                MemoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                TagId = table.Column<Guid>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MemoryTags", x => new { x.MemoryId, x.TagId });
                table.ForeignKey(
                    name: "FK_MemoryTags_Memories_MemoryId",
                    column: x => x.MemoryId,
                    principalTable: "Memories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MemoryTags_Tags_TagId",
                    column: x => x.TagId,
                    principalTable: "Tags",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PersonTags",
            columns: table => new
            {
                PersonId = table.Column<Guid>(type: "TEXT", nullable: false),
                TagId = table.Column<Guid>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonTags", x => new { x.PersonId, x.TagId });
                table.ForeignKey(
                    name: "FK_PersonTags_People_PersonId",
                    column: x => x.PersonId,
                    principalTable: "People",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PersonTags_Tags_TagId",
                    column: x => x.TagId,
                    principalTable: "Tags",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_MemoryTags_TagId",
            table: "MemoryTags",
            column: "TagId");

        migrationBuilder.CreateIndex(
            name: "IX_PersonTags_TagId",
            table: "PersonTags",
            column: "TagId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MemoryTags");

        migrationBuilder.DropTable(
            name: "PersonTags");

        migrationBuilder.DropColumn(
            name: "Birthday",
            table: "People");

        migrationBuilder.DropColumn(
            name: "Gender",
            table: "People");

        migrationBuilder.DropColumn(
            name: "Emotion",
            table: "Memories");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DateAdded",
            table: "MemoryItems",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime");

        migrationBuilder.AlterColumn<DateTime>(
            name: "Date",
            table: "Memories",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime");

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
            name: "IX_MemoryTag_TagId",
            table: "MemoryTag",
            column: "TagId");
    }
}
