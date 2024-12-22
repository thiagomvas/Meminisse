using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meminisse.Application.Migrations;

/// <inheritdoc />
public partial class AddMemoryPersonTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MemoryPerson");

        migrationBuilder.AddColumn<Guid>(
            name: "PersonId",
            table: "Memories",
            type: "TEXT",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "MemoryPeople",
            columns: table => new
            {
                MemoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                PersonId = table.Column<Guid>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MemoryPeople", x => new { x.MemoryId, x.PersonId });
                table.ForeignKey(
                    name: "FK_MemoryPeople_Memories_MemoryId",
                    column: x => x.MemoryId,
                    principalTable: "Memories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MemoryPeople_People_PersonId",
                    column: x => x.PersonId,
                    principalTable: "People",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Memories_PersonId",
            table: "Memories",
            column: "PersonId");

        migrationBuilder.CreateIndex(
            name: "IX_MemoryPeople_PersonId",
            table: "MemoryPeople",
            column: "PersonId");

        migrationBuilder.AddForeignKey(
            name: "FK_Memories_People_PersonId",
            table: "Memories",
            column: "PersonId",
            principalTable: "People",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Memories_People_PersonId",
            table: "Memories");

        migrationBuilder.DropTable(
            name: "MemoryPeople");

        migrationBuilder.DropIndex(
            name: "IX_Memories_PersonId",
            table: "Memories");

        migrationBuilder.DropColumn(
            name: "PersonId",
            table: "Memories");

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

        migrationBuilder.CreateIndex(
            name: "IX_MemoryPerson_PersonId",
            table: "MemoryPerson",
            column: "PersonId");
    }
}
