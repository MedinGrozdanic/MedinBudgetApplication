using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetShellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BudgetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetShellId);
                    table.ForeignKey(
                        name: "FK_Budgets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryBudgetID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryBudgets",
                columns: table => new
                {
                    CategoryBudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BudgetShellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxAmount = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryBudgets", x => x.CategoryBudgetId);
                    table.ForeignKey(
                        name: "FK_CategoryBudgets_Budgets_BudgetShellId",
                        column: x => x.BudgetShellId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetShellId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryBudgets_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Food" },
                    { 2, "Shopping" },
                    { 3, "Entertainment" },
                    { 4, "Housing" },
                    { 5, "Transportation" },
                    { 6, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("2fa65294-5d70-4985-b15d-039c8f41743d"), "mathias@gmail.com", "Mathias", "Angelin", "abc", "mathias1" },
                    { new Guid("4c71645e-eb93-4dae-8586-70024ce28d89"), "medin@gmail.com", "Medin", "Grozdanic", "abc", "Medin1" }
                });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "BudgetShellId", "BudgetName", "EndDate", "StartDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("16723c21-2bb0-4c96-bf8a-3d083a331224"), "Budget 9", new DateTime(2022, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("1a38c791-fafc-436a-a03e-3586f355ea25"), "Budget 1", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("4faa2645-50cf-4358-88ae-aeb19036f225"), "Budget 4", new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("53575853-8605-4364-8e73-e7c92b0b6c47"), "Budget 6", new DateTime(2022, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("6ce2251f-233e-4521-9e14-580a7bd7d1a3"), "Budget 2", new DateTime(2022, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("9a97446e-022e-42eb-8a25-89a27bd950cd"), "Budget 3", new DateTime(2022, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("be0e300f-bec5-44cc-83dc-8e37472a339e"), "Budget 5", new DateTime(2022, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("c15cb38e-a7ce-4d67-825f-8e02ca1ce7ac"), "Budget 7", new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("e12ea337-db49-45ff-b3a6-57a157395646"), "Budget 10", new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("fee2835d-9ab9-4ea9-8a03-2b445a64df46"), "Budget 8", new DateTime(2022, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "ExpenseId", "Amount", "CategoryBudgetID", "CategoryId", "Comment", "DateStamp", "Receiver", "UserId" },
                values: new object[,]
                {
                    { new Guid("0219ca9b-e290-44e1-b6ee-425d38854161"), 1950m, null, 5, "Comment 109", new DateTime(2023, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver109", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("052f1b27-99e2-4b09-b415-c00e82c39059"), 1860m, null, 6, "Comment 112", new DateTime(2022, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver112", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("0536b405-4140-460d-a0cc-5a297a03a5cd"), 430m, null, 2, "Comment 189", new DateTime(2023, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver189", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("087b13ef-1139-406d-aa05-ea4c20f7eed3"), 1510m, null, 1, "Comment 111", new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver111", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("0a741004-1646-4717-bc88-858be6a140b0"), 500m, null, 4, "Comment 1", new DateTime(2022, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver1", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("0b029e37-4a04-4fec-ab06-b8aa09975df9"), 1430m, null, 5, "Comment 142", new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver142", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("0bc02227-2c4d-42e6-a5bb-a0f02d2314eb"), 1080m, null, 2, "Comment 176", new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver176", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("0c3fc17e-37f0-479e-ac22-ab55e2c67644"), 1810m, null, 6, "Comment 138", new DateTime(2023, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver138", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("11ca9e48-7d1b-45a3-a10c-1113f68264ef"), 660m, null, 1, "Comment 49", new DateTime(2023, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver49", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("13749f94-8cd6-43f4-8485-8f1c908c48cf"), 350m, null, 1, "Comment 184", new DateTime(2023, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver184", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("13bfd9d0-1dd2-4dab-8b89-868b7d462122"), 1260m, null, 3, "Comment 135", new DateTime(2022, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver135", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("13f7557c-2436-4d39-8aef-e5b9b4f8dc2c"), 1170m, null, 2, "Comment 182", new DateTime(2022, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver182", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("15e185b1-9906-4d7b-ae89-fd1b3133cb53"), 1240m, null, 5, "Comment 96", new DateTime(2022, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver96", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("1635f8fa-cf04-4d26-a6f0-4883a0176401"), 510m, null, 4, "Comment 39", new DateTime(2022, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver39", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("1751310a-803e-4d88-9766-d3f0650a2762"), 1760m, null, 6, "Comment 94", new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver94", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("19e2844e-ac56-495a-a2f5-3badae0adcd7"), 530m, null, 1, "Comment 65", new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver65", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("1a6c4450-4812-43d8-80d8-c83c5b4a0cc5"), 580m, null, 3, "Comment 114", new DateTime(2023, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver114", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("1aeadcb3-031f-47ba-a3a8-da613b236d4b"), 1350m, null, 1, "Comment 154", new DateTime(2023, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver154", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("1de23fa0-b856-41ac-a7df-da7e3c46c3df"), 750m, null, 5, "Comment 25", new DateTime(2022, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver25", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("1f7c963a-be51-4da9-b28f-9cf4b973d016"), 1880m, null, 1, "Comment 166", new DateTime(2022, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver166", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("214abde5-d4ad-47fe-b6b6-73730f56e9bd"), 680m, null, 6, "Comment 197", new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver197", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("220f62fe-3421-4121-9be7-17028eb162a6"), 470m, null, 5, "Comment 8", new DateTime(2023, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver8", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("233ce8e4-95ed-4cc0-9268-a80b411833c8"), 690m, null, 1, "Comment 127", new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver127", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("23f059e8-4339-4c8e-b061-94507215dd3a"), 550m, null, 1, "Comment 99", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver99", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("27a35345-e083-45da-acf8-a16bb7788310"), 200m, null, 3, "Comment 143", new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver143", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("283a3372-fdc9-4f89-a89b-c030160cc6ff"), 210m, null, 1, "Comment 130", new DateTime(2022, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver130", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("2a67b2d5-4ea4-48ed-8425-382b992931ba"), 1060m, null, 2, "Comment 193", new DateTime(2022, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver193", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("2cd9026f-c867-4ad1-955b-9f8a8209bafc"), 1860m, null, 6, "Comment 56", new DateTime(2023, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver56", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("2e36a3a6-a04f-4193-a6e3-37f1741f1330"), 660m, null, 3, "Comment 124", new DateTime(2023, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver124", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("315db64f-8a7e-4309-a434-e42bbd495f39"), 580m, null, 6, "Comment 113", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver113", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("31b69180-bac3-457a-8398-2a431edb1e21"), 1950m, null, 5, "Comment 6", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver6", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("32fcecf8-cc6a-4c41-974d-09ec30fee5a2"), 1520m, null, 1, "Comment 183", new DateTime(2023, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver183", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "ExpenseId", "Amount", "CategoryBudgetID", "CategoryId", "Comment", "DateStamp", "Receiver", "UserId" },
                values: new object[,]
                {
                    { new Guid("349be9a4-1b27-4cfb-bbcb-4640c112c6b0"), 1870m, null, 1, "Comment 101", new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver101", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("34e603e0-4384-491d-a530-f76095c6aada"), 690m, null, 5, "Comment 156", new DateTime(2022, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver156", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("364b85d6-6020-4173-b46d-f6d8230eb2b7"), 1460m, null, 4, "Comment 129", new DateTime(2022, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver129", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("37c4023b-21cd-40aa-b04e-55ad1abeddf3"), 1650m, null, 3, "Comment 152", new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver152", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("38a5e313-1444-4a71-ad82-100cb4b8b293"), 740m, null, 5, "Comment 71", new DateTime(2022, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver71", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("3a3ad5da-55ab-43d8-bc7e-b0422949b9a8"), 1700m, null, 2, "Comment 57", new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver57", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("3b6b9486-4cf0-4f11-8c78-4ad9e41f6b01"), 540m, null, 3, "Comment 155", new DateTime(2023, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver155", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("3cdbf9c6-fe20-45b7-995f-64045b54c5aa"), 870m, null, 3, "Comment 74", new DateTime(2023, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver74", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("3d51bea1-e9a8-4ea2-b1ff-5da960c813f2"), 580m, null, 4, "Comment 173", new DateTime(2022, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver173", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("402b7002-5a97-4044-a1fa-7d0bf573dfa9"), 1550m, null, 4, "Comment 122", new DateTime(2022, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver122", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("40b1ff91-fc13-4c49-9f7e-f7238acca4b4"), 1610m, null, 4, "Comment 190", new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver190", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("46ae822b-8756-452f-8d35-c5f6e3dc462c"), 1040m, null, 2, "Comment 137", new DateTime(2023, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver137", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("46ff0d7e-525f-4cbe-98d8-e9e756ad7171"), 1160m, null, 4, "Comment 41", new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver41", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("4806ba07-6d88-4ac4-8410-1b0bfaa93ac0"), 830m, null, 2, "Comment 17", new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver17", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("4bfa8188-61c5-4da0-91ea-bd2f4cd8ddd4"), 1880m, null, 3, "Comment 75", new DateTime(2022, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver75", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("4c7787e1-628c-4be9-8888-47b5d9244437"), 390m, null, 1, "Comment 115", new DateTime(2023, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver115", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("4cff32e8-eae1-4263-a450-0dcbccf9a1c7"), 600m, null, 3, "Comment 188", new DateTime(2022, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver188", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("4d3f6323-731b-408e-8502-f7a04fb2c0d8"), 1100m, null, 6, "Comment 159", new DateTime(2022, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver159", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("4eceddf0-6112-47ec-a321-f7a34d01b366"), 1250m, null, 4, "Comment 180", new DateTime(2023, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver180", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("50e7eac8-04eb-45f8-a144-3e7315797262"), 1090m, null, 5, "Comment 31", new DateTime(2022, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver31", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("50f51bac-e57b-45e6-a56b-5f2b63b0e339"), 1060m, null, 1, "Comment 72", new DateTime(2022, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver72", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("5421dc74-1f73-47d9-b6b3-048417aeaa5f"), 1400m, null, 1, "Comment 200", new DateTime(2022, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver200", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("54738250-891f-4261-853a-c826f6799c6b"), 400m, null, 2, "Comment 58", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver58", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("55f1f0e1-f71a-44a3-abb6-ce038551d007"), 1560m, null, 4, "Comment 133", new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver133", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("58aadd56-55fd-49ab-a942-f30a2b37515a"), 1980m, null, 1, "Comment 22", new DateTime(2022, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver22", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("5956925d-a398-424e-a1a9-c4f91c180c03"), 1930m, null, 4, "Comment 168", new DateTime(2022, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver168", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("5cb760db-b200-4a09-b495-f99768094e0e"), 1710m, null, 4, "Comment 169", new DateTime(2022, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver169", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("5f3224a0-b160-4883-af8f-3d94095cf40a"), 380m, null, 6, "Comment 32", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver32", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("6033cf49-887d-41a8-9d92-1dd133e108ad"), 460m, null, 2, "Comment 47", new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver47", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("60392934-2422-4e67-838b-f6a7981cb3b0"), 1220m, null, 3, "Comment 187", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver187", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("615455f5-95bd-411c-b2ce-efe163b8d495"), 730m, null, 5, "Comment 60", new DateTime(2022, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver60", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("620de005-56a9-48dc-a11d-b099b37d2571"), 800m, null, 6, "Comment 179", new DateTime(2023, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver179", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("6243932b-aa15-4c2c-9fe0-6ca99cd15c59"), 330m, null, 5, "Comment 153", new DateTime(2023, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver153", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("64aab2de-d455-4303-8a83-b3e2104b085e"), 620m, null, 4, "Comment 43", new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver43", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("654480cf-c054-4e33-90a4-4a16ed01e893"), 1090m, null, 6, "Comment 2", new DateTime(2023, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver2", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("6b3446cb-00bc-4ea1-87ec-41d5b9538ce1"), 1660m, null, 4, "Comment 186", new DateTime(2023, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver186", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("6bb05d5c-e525-4596-be51-2e7293ad7d61"), 1280m, null, 6, "Comment 121", new DateTime(2022, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver121", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("6c316a4c-e58d-4026-bb9c-0d2fb1655cc0"), 1880m, null, 3, "Comment 97", new DateTime(2022, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver97", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("6c7bd26d-99cc-416a-a161-acc7ed3dbef1"), 760m, null, 4, "Comment 5", new DateTime(2022, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver5", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("6d3c36b3-75d8-4cc7-8868-c7f868c6779c"), 1030m, null, 2, "Comment 126", new DateTime(2022, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver126", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("6dff4ad4-e245-4337-be82-0ead73fbb09b"), 1460m, null, 3, "Comment 27", new DateTime(2022, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver27", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("6e11a16a-ac60-4a57-95c2-47c42e5ade48"), 1900m, null, 6, "Comment 160", new DateTime(2023, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver160", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "ExpenseId", "Amount", "CategoryBudgetID", "CategoryId", "Comment", "DateStamp", "Receiver", "UserId" },
                values: new object[,]
                {
                    { new Guid("6e27ce4c-43b4-4cc9-8d65-a93c7b987c0b"), 690m, null, 4, "Comment 170", new DateTime(2022, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver170", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("6e49798a-7285-4a55-80d4-09587e74bb63"), 1940m, null, 5, "Comment 139", new DateTime(2022, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver139", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("6fe7465f-9d96-42a3-a31c-c0edcde54a8e"), 2000m, null, 1, "Comment 117", new DateTime(2022, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver117", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("70efffd1-de00-44b1-836b-52a434eee7ed"), 1520m, null, 5, "Comment 140", new DateTime(2022, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver140", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("71caf365-36e6-4db0-9b1f-f2346b0bd255"), 1430m, null, 3, "Comment 120", new DateTime(2022, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver120", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("729d9e45-38ed-4dbf-997f-b0458e11104f"), 890m, null, 2, "Comment 146", new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver146", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("72a6d68c-bbea-438b-a66d-d2be64b2a04a"), 730m, null, 4, "Comment 53", new DateTime(2023, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver53", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("74a6cae1-ae76-4267-bac4-3d666f92234c"), 1870m, null, 1, "Comment 148", new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver148", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("756d0458-f720-420c-a2ce-a42e579816ab"), 340m, null, 5, "Comment 149", new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver149", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("75721f76-fc10-408b-a839-e3c4a5760c6d"), 1520m, null, 1, "Comment 100", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver100", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("75ae312a-a11c-4e82-b02a-1f769bfce9a8"), 670m, null, 4, "Comment 29", new DateTime(2023, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver29", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("76b3e0d2-048f-499e-a401-217ddc18fcf2"), 590m, null, 4, "Comment 141", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver141", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("7a5c653f-6c9c-45fd-aac0-e4949c4b04ff"), 1130m, null, 4, "Comment 165", new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver165", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("7d3a2f0f-c9ee-4120-b1b1-6966e0e82ca3"), 1380m, null, 4, "Comment 95", new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver95", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("7dc6adb3-d50b-4cca-a813-6e873823d870"), 800m, null, 2, "Comment 83", new DateTime(2022, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver83", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("7e208850-cba9-40ad-9354-91b36fa0d4b8"), 1610m, null, 5, "Comment 13", new DateTime(2023, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver13", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("7f874944-0615-4823-8d95-99be96476c2b"), 970m, null, 3, "Comment 55", new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver55", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("829a500f-de37-473d-b989-8367df5071dd"), 1310m, null, 6, "Comment 86", new DateTime(2023, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver86", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("82bd3e09-acc7-422b-94bb-66a397051752"), 430m, null, 6, "Comment 196", new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver196", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("845153f4-c496-45fd-92e3-659ca49bb17b"), 1670m, null, 2, "Comment 46", new DateTime(2022, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver46", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("845c81e1-8c5c-4b47-9a9f-7ba225c952db"), 900m, null, 3, "Comment 157", new DateTime(2023, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver157", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("86955f0b-310f-446d-ad6d-5049ea3255bf"), 1560m, null, 4, "Comment 119", new DateTime(2022, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver119", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("876b2dd0-5ae9-4d29-bcd6-93e1a2c63714"), 1380m, null, 1, "Comment 106", new DateTime(2023, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver106", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("87963f85-6d6b-479e-b0ab-91244f6a1183"), 560m, null, 4, "Comment 18", new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver18", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("87e5902c-400d-448c-91af-3fb17f5c1f42"), 1370m, null, 1, "Comment 164", new DateTime(2023, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver164", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("880fef73-dcc6-4661-9e90-f367d43fad83"), 580m, null, 3, "Comment 136", new DateTime(2022, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver136", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("899114ed-af2a-4235-8452-dfe9ea675a0a"), 960m, null, 2, "Comment 104", new DateTime(2023, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver104", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("89d2cc1c-6d19-4ba1-bafa-12012d738032"), 1580m, null, 3, "Comment 198", new DateTime(2022, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver198", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("89e6d45d-1711-4ea1-aa80-f7ad06e1b58e"), 1320m, null, 3, "Comment 103", new DateTime(2023, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver103", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("8a3791c1-e7da-4cf1-8601-d60c89cece08"), 900m, null, 6, "Comment 28", new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver28", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("8a666c71-cff2-473e-9665-9fb032c4392f"), 1200m, null, 2, "Comment 38", new DateTime(2022, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver38", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("8b677264-6800-4e6f-8505-ba4950feec01"), 1700m, null, 3, "Comment 158", new DateTime(2022, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver158", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("8c9ee0c6-8967-4ab4-81dd-6f2addba522b"), 1460m, null, 3, "Comment 76", new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver76", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("8d2b2636-9caf-4bc6-ada6-5b392f16568b"), 940m, null, 4, "Comment 89", new DateTime(2023, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver89", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("8f4176fe-974a-4b22-af19-9cb3b55569f7"), 1350m, null, 4, "Comment 132", new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver132", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("909b7730-d7cb-4c71-b456-e20109e6b4fa"), 1280m, null, 4, "Comment 91", new DateTime(2023, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver91", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("91964066-403d-4ae0-bfc5-5859b502e80f"), 1200m, null, 1, "Comment 161", new DateTime(2022, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver161", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("92f26b82-404b-4260-b5f5-219d4c6e0fca"), 1280m, null, 2, "Comment 147", new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver147", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("95c171ab-bd96-4728-a766-19fb51084242"), 950m, null, 1, "Comment 77", new DateTime(2022, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver77", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("95d3fd0a-4c51-4771-ae9c-f0d44c29efc8"), 2000m, null, 4, "Comment 24", new DateTime(2022, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver24", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("96799159-995b-4df5-ac1c-8eaf4bcafa27"), 1780m, null, 5, "Comment 81", new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver81", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("981551cf-7996-4134-81d1-a4e0a5694762"), 710m, null, 5, "Comment 10", new DateTime(2022, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver10", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "ExpenseId", "Amount", "CategoryBudgetID", "CategoryId", "Comment", "DateStamp", "Receiver", "UserId" },
                values: new object[,]
                {
                    { new Guid("9c3b4d27-e7b9-4501-be56-02985581c7d7"), 1830m, null, 2, "Comment 167", new DateTime(2022, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver167", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("9d6acaac-3f8f-48e8-8b3e-6b8b3b734eeb"), 400m, null, 6, "Comment 171", new DateTime(2022, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver171", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("9da8369d-6b1a-4124-b366-ef8374150306"), 1770m, null, 3, "Comment 69", new DateTime(2022, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver69", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("9e471751-9aea-42f8-97d3-4bdf3866b1bd"), 1240m, null, 5, "Comment 102", new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver102", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("9e5f9060-685d-44a2-8446-b741155ed38b"), 260m, null, 4, "Comment 98", new DateTime(2022, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver98", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("9eb9c9a9-51e7-4381-aeb6-17bc5b84c0da"), 1370m, null, 4, "Comment 44", new DateTime(2022, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver44", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("9ee0ebbc-e03a-4b10-823b-6194e32ebee9"), 1380m, null, 3, "Comment 162", new DateTime(2023, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver162", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("9f9ec2e5-603f-4b95-890a-f6cc8bd3591a"), 880m, null, 6, "Comment 11", new DateTime(2023, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver11", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("a0fd31af-a353-44c8-9109-fb2b41b7531a"), 1820m, null, 2, "Comment 35", new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver35", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("a1d2bf51-6348-4085-8cef-c6e21df28019"), 1910m, null, 1, "Comment 145", new DateTime(2023, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver145", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("a35dfcca-e6a5-473b-a900-c198566b8338"), 290m, null, 5, "Comment 21", new DateTime(2022, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver21", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("a75ac68c-faed-470d-b2bb-554f11c07a57"), 240m, null, 3, "Comment 116", new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver116", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("a819b01f-55a7-456a-8a9f-1bda92874e46"), 1310m, null, 6, "Comment 19", new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver19", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("a848a816-34c1-4102-b9c0-131e6637f0be"), 800m, null, 2, "Comment 128", new DateTime(2022, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver128", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("a85cb566-c507-448d-a477-e1a9250c2b65"), 780m, null, 5, "Comment 163", new DateTime(2023, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver163", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("a8c8ed9e-7c2e-4f03-8ddf-79b0be95ac3c"), 820m, null, 6, "Comment 150", new DateTime(2022, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver150", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("a93cbff9-6e06-45f6-bd92-d61d98e95c4b"), 1840m, null, 6, "Comment 66", new DateTime(2022, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver66", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("aaeea6ee-e5f9-48e5-b9ca-a06d5a071da7"), 1960m, null, 2, "Comment 175", new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver175", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("ac91b753-af6c-4b94-aed0-4453d529bd1e"), 1900m, null, 2, "Comment 195", new DateTime(2022, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver195", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("ad3e5da9-90b1-4942-91f7-6213425194bb"), 1950m, null, 4, "Comment 194", new DateTime(2023, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver194", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("ae539a1c-ad12-45e5-b506-13081aa36d31"), 690m, null, 6, "Comment 63", new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver63", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("af0d2ed6-f211-48df-b55f-6a343f56df92"), 1660m, null, 2, "Comment 172", new DateTime(2022, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver172", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("b1221257-b31d-4f0d-a2cd-a26e0d0c92a8"), 1010m, null, 2, "Comment 42", new DateTime(2023, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver42", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("b2c227a1-2e8d-4fae-8a07-bc7df0db712d"), 980m, null, 6, "Comment 34", new DateTime(2023, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver34", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("b5c9d625-20dd-4715-8dd7-7d669aafc75b"), 480m, null, 3, "Comment 174", new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver174", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("b66ed80f-6dd8-4115-a1ce-3be8c2c8ecdf"), 450m, null, 2, "Comment 14", new DateTime(2022, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver14", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("b856b77b-991c-48b9-b093-029ed6f19266"), 900m, null, 3, "Comment 9", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver9", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("b87e5815-2e09-4b53-a77d-52c074393482"), 1190m, null, 3, "Comment 192", new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver192", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("b8ec05c2-9ed8-4622-adcd-6fc30a388a08"), 1450m, null, 5, "Comment 93", new DateTime(2023, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver93", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("b9017000-b890-437f-a068-230bc98c5ffc"), 1480m, null, 4, "Comment 70", new DateTime(2022, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver70", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("ba4282e3-98f2-4ebb-a5f7-ce6a6b8d6c75"), 1890m, null, 5, "Comment 4", new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver4", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("bc2ab9cb-dce2-4824-b378-b12428c1080c"), 1080m, null, 1, "Comment 144", new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver144", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("bc5fbdf9-ff3e-4369-a2bf-f09a4e2b4456"), 400m, null, 3, "Comment 151", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver151", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("bdfe6a07-bf4b-4063-bbf4-c69f0539be4f"), 640m, null, 4, "Comment 177", new DateTime(2023, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver177", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("be5d630b-d567-49b1-8a67-05a2ce7c2416"), 1180m, null, 6, "Comment 110", new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver110", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("bf3ecc3b-ea94-4a41-99b7-8e6272850d72"), 450m, null, 2, "Comment 15", new DateTime(2022, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver15", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("bfdcbd20-910a-46b9-9461-c7c24594b31c"), 550m, null, 1, "Comment 105", new DateTime(2023, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver105", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("bfffc068-86ca-439a-9576-f6a7cf04f92a"), 1890m, null, 6, "Comment 131", new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver131", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("c28d0b45-b710-4c17-baac-af4eb47d6d21"), 920m, null, 3, "Comment 85", new DateTime(2022, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver85", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("c3188b98-cd6d-413b-b347-822c01f96404"), 1410m, null, 2, "Comment 191", new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver191", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("c5d9d378-4447-476b-8c41-e74c92e202b8"), 480m, null, 6, "Comment 107", new DateTime(2022, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver107", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("c64b03f3-e925-4401-8754-696c64de0f66"), 1150m, null, 1, "Comment 68", new DateTime(2022, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver68", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "ExpenseId", "Amount", "CategoryBudgetID", "CategoryId", "Comment", "DateStamp", "Receiver", "UserId" },
                values: new object[,]
                {
                    { new Guid("c7930e59-f7b6-40a8-946c-1c21353522c1"), 250m, null, 3, "Comment 181", new DateTime(2022, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver181", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("c84d315f-61c3-41b9-a0d8-dcc6ae829492"), 640m, null, 5, "Comment 82", new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver82", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("c91d3689-d9e1-4ac5-9260-3eaeb3092779"), 1640m, null, 4, "Comment 185", new DateTime(2022, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver185", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("c9877776-2845-4614-b59f-649c2f347558"), 400m, null, 4, "Comment 108", new DateTime(2022, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver108", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("caa18d3f-e9ee-47a3-a7a4-8cd5a14d8b94"), 970m, null, 4, "Comment 12", new DateTime(2022, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver12", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("cbe67522-1f26-4a1c-9c82-287982aa13ab"), 1810m, null, 1, "Comment 80", new DateTime(2022, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver80", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("ccef1d2c-6fd2-49d4-9eee-97ab0baed48d"), 930m, null, 2, "Comment 26", new DateTime(2022, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver26", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("ccf033a6-fc09-4cb5-916d-e02c7eb40efb"), 1760m, null, 3, "Comment 51", new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver51", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("cd222431-1485-47e2-af7d-0c13cd9e7c89"), 630m, null, 3, "Comment 79", new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver79", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("cd93685e-d971-410a-b4e3-dcc9bb6b12f2"), 940m, null, 3, "Comment 87", new DateTime(2022, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver87", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("d18643ab-1872-46b0-83c3-d9a183c66d13"), 1610m, null, 2, "Comment 90", new DateTime(2022, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver90", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("d218fbe9-35f7-4f0f-a5f9-5cb8144c9e21"), 1360m, null, 4, "Comment 50", new DateTime(2022, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver50", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("d2a50f91-8fc9-4d3f-aa4e-0256d144a82e"), 960m, null, 1, "Comment 20", new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver20", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("d3ac3550-2f5f-49ca-ac40-2046e525f5f0"), 1020m, null, 6, "Comment 59", new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver59", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("d3f47042-f503-468c-8ef2-2b6fc0b3c4c8"), 970m, null, 6, "Comment 54", new DateTime(2022, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver54", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("d406124a-2e78-4dbf-9327-4cbb87f42965"), 900m, null, 2, "Comment 40", new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver40", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("d5609aae-6625-402d-be59-6dbdd80b3db2"), 860m, null, 5, "Comment 123", new DateTime(2023, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver123", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("d612a2cb-f4c2-4b99-8785-48e395c8c399"), 1810m, null, 6, "Comment 45", new DateTime(2022, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver45", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("d744ba97-16f9-470e-8b11-a01e5e5affd4"), 1510m, null, 3, "Comment 37", new DateTime(2022, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver37", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("dceb73f3-6867-4f12-af04-3564893c83eb"), 450m, null, 3, "Comment 134", new DateTime(2023, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver134", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("dd8f3393-bb4a-4655-8654-05492df0d5ea"), 620m, null, 3, "Comment 64", new DateTime(2023, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver64", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("dedabfc5-821f-476a-8438-f548ee6f66e4"), 1690m, null, 5, "Comment 92", new DateTime(2023, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver92", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("dee331bb-9f4e-4ebb-88e4-ad69aa5bc6aa"), 250m, null, 6, "Comment 199", new DateTime(2023, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver199", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("deec8e4d-905d-4bb3-bdaf-35c734f2ee20"), 320m, null, 1, "Comment 62", new DateTime(2023, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver62", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("df45952e-f2c4-48a5-aafe-0987ea63a76d"), 1260m, null, 4, "Comment 48", new DateTime(2023, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver48", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("e0920ebd-1b21-42f6-a0d6-ab09e6c135f0"), 420m, null, 4, "Comment 178", new DateTime(2023, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver178", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("e3910429-5830-4864-9479-5e67ea0baf2d"), 1720m, null, 3, "Comment 52", new DateTime(2023, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver52", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("e3c64dc1-a729-4193-8dbe-68063052b91f"), 1620m, null, 2, "Comment 125", new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver125", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("e416b433-ba5a-4912-b258-c83abcd3e837"), 1520m, null, 2, "Comment 73", new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver73", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("e43fdbbf-57b4-48aa-99ed-f09bba9d9d34"), 1010m, null, 2, "Comment 118", new DateTime(2023, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver118", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("e6bcfbd7-2c2f-4183-8b48-04c54b161211"), 380m, null, 5, "Comment 33", new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver33", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("e96e225c-ec63-4da5-a396-89ca57feb681"), 1020m, null, 1, "Comment 78", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver78", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("ed4c2cce-a96b-4aaf-82a8-2343c7238188"), 1110m, null, 6, "Comment 23", new DateTime(2023, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver23", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("f05dfa4b-bc1a-4120-b3bb-1740fcba189d"), 300m, null, 5, "Comment 36", new DateTime(2022, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver36", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("f0ffff92-1a56-4042-ab65-1ba59bc613f3"), 800m, null, 5, "Comment 3", new DateTime(2022, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver3", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("f5b8c04c-30d6-48b3-bd72-03e7a8932b7e"), 1710m, null, 5, "Comment 61", new DateTime(2023, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver61", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("f63f21db-d396-41b6-91b2-7ddb57e9e28c"), 330m, null, 2, "Comment 88", new DateTime(2022, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver88", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("f7d23524-4e2f-40d3-89c2-b0208184208e"), 510m, null, 4, "Comment 16", new DateTime(2023, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver16", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("f7e48fe2-6aec-448c-9862-3d1c5efc309c"), 1650m, null, 5, "Comment 84", new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver84", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") },
                    { new Guid("fcbe1694-74bb-448a-a534-60bed088f7c6"), 1780m, null, 2, "Comment 7", new DateTime(2022, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver7", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("fe6c3570-afc1-49bf-b969-d2cc39ac7dfd"), 1020m, null, 4, "Comment 30", new DateTime(2022, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver30", new Guid("4c71645e-eb93-4dae-8586-70024ce28d89") },
                    { new Guid("ff378cfc-dfe3-49b1-8f4a-a319203e162d"), 800m, null, 2, "Comment 67", new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receiver67", new Guid("2fa65294-5d70-4985-b15d-039c8f41743d") }
                });

            migrationBuilder.InsertData(
                table: "CategoryBudgets",
                columns: new[] { "CategoryBudgetId", "BudgetShellId", "CategoryId", "MaxAmount" },
                values: new object[,]
                {
                    { new Guid("183d3a84-ddff-458e-83de-033af49fc1b6"), new Guid("be0e300f-bec5-44cc-83dc-8e37472a339e"), 1, 9200.0 },
                    { new Guid("26332135-be29-4a9a-bdcd-f1d49a0f3c04"), new Guid("6ce2251f-233e-4521-9e14-580a7bd7d1a3"), 3, 7400.0 },
                    { new Guid("281a745c-8d07-4fbe-8b5a-2e4c248d458c"), new Guid("be0e300f-bec5-44cc-83dc-8e37472a339e"), 2, 1300.0 },
                    { new Guid("28b70d54-f5b5-4e2f-994b-cf0e455eb59b"), new Guid("53575853-8605-4364-8e73-e7c92b0b6c47"), 1, 7700.0 },
                    { new Guid("31e8e7d1-92d5-4a64-96d3-13ea1b368f7d"), new Guid("fee2835d-9ab9-4ea9-8a03-2b445a64df46"), 3, 4400.0 },
                    { new Guid("41dd5620-daaf-42a2-8ba2-2b9268c43dc6"), new Guid("9a97446e-022e-42eb-8a25-89a27bd950cd"), 4, 9700.0 },
                    { new Guid("42bf418d-cc5f-4251-aa8e-261b58560601"), new Guid("c15cb38e-a7ce-4d67-825f-8e02ca1ce7ac"), 1, 8000.0 },
                    { new Guid("484e7057-bb88-4412-a5ba-1ffb8a4706d8"), new Guid("fee2835d-9ab9-4ea9-8a03-2b445a64df46"), 4, 2400.0 },
                    { new Guid("4bf405d6-a31b-4988-9c88-7bee4fc6e3c7"), new Guid("c15cb38e-a7ce-4d67-825f-8e02ca1ce7ac"), 3, 5000.0 },
                    { new Guid("4f38e781-61d7-4957-a618-72117214f668"), new Guid("4faa2645-50cf-4358-88ae-aeb19036f225"), 2, 2200.0 },
                    { new Guid("51be7e2d-4b65-4ed1-b104-9993e6db4bc2"), new Guid("6ce2251f-233e-4521-9e14-580a7bd7d1a3"), 2, 3000.0 },
                    { new Guid("57bc26bc-bca5-4810-8fd4-b193e197c676"), new Guid("fee2835d-9ab9-4ea9-8a03-2b445a64df46"), 5, 2100.0 },
                    { new Guid("5aa3b7fa-7d6e-420a-a9c1-ba622e4b913d"), new Guid("16723c21-2bb0-4c96-bf8a-3d083a331224"), 4, 900.0 },
                    { new Guid("70bb4726-4695-4750-b34d-d4d2a1eabc71"), new Guid("1a38c791-fafc-436a-a03e-3586f355ea25"), 2, 0.0 },
                    { new Guid("724f92a2-3a3a-4bbc-aecd-3cbb07f881be"), new Guid("4faa2645-50cf-4358-88ae-aeb19036f225"), 3, 9300.0 },
                    { new Guid("82610bf0-e569-404d-a974-158f0b511c00"), new Guid("16723c21-2bb0-4c96-bf8a-3d083a331224"), 3, 8700.0 },
                    { new Guid("88069210-68af-41ee-a1ee-fe3ea93bd9f5"), new Guid("9a97446e-022e-42eb-8a25-89a27bd950cd"), 4, 4600.0 },
                    { new Guid("8e215cf2-c716-4e66-9297-3a50d4bc08dd"), new Guid("c15cb38e-a7ce-4d67-825f-8e02ca1ce7ac"), 1, 6800.0 },
                    { new Guid("985d23fd-bcfb-4864-a57b-af6d93831954"), new Guid("fee2835d-9ab9-4ea9-8a03-2b445a64df46"), 5, 3300.0 },
                    { new Guid("a2c0af87-ea30-4a50-ac4d-d7a7fbb0d493"), new Guid("c15cb38e-a7ce-4d67-825f-8e02ca1ce7ac"), 4, 4600.0 },
                    { new Guid("aee572cc-4f24-482b-8db6-947875f81a38"), new Guid("c15cb38e-a7ce-4d67-825f-8e02ca1ce7ac"), 1, 5600.0 },
                    { new Guid("c5c7278a-6d05-462d-8739-566a928a975b"), new Guid("6ce2251f-233e-4521-9e14-580a7bd7d1a3"), 6, 2600.0 },
                    { new Guid("ce2a968d-e665-4b6a-87eb-ae2f5a5f4d83"), new Guid("16723c21-2bb0-4c96-bf8a-3d083a331224"), 2, 7400.0 },
                    { new Guid("d602d5ad-2b26-4c4f-88af-4b557e7ae872"), new Guid("c15cb38e-a7ce-4d67-825f-8e02ca1ce7ac"), 6, 8500.0 },
                    { new Guid("d7cb26cc-4bc4-415d-9191-738bef14afcd"), new Guid("1a38c791-fafc-436a-a03e-3586f355ea25"), 5, 3800.0 },
                    { new Guid("e938ed5d-ddf2-4aa8-9bb7-493635362750"), new Guid("e12ea337-db49-45ff-b3a6-57a157395646"), 6, 300.0 },
                    { new Guid("ed75ae95-a1b5-4815-bbc8-13eb677179cb"), new Guid("9a97446e-022e-42eb-8a25-89a27bd950cd"), 3, 3200.0 },
                    { new Guid("f35df0d6-c0ca-4907-9f0b-c5047c937b4b"), new Guid("53575853-8605-4364-8e73-e7c92b0b6c47"), 3, 8900.0 },
                    { new Guid("fd03ad09-514c-4423-8b42-cc280b756f6d"), new Guid("4faa2645-50cf-4358-88ae-aeb19036f225"), 3, 8900.0 },
                    { new Guid("ffc729ac-43ca-45ec-b972-1114cfda308c"), new Guid("be0e300f-bec5-44cc-83dc-8e37472a339e"), 6, 8800.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBudgets_BudgetShellId",
                table: "CategoryBudgets",
                column: "BudgetShellId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBudgets_CategoryId",
                table: "CategoryBudgets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryBudgets");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
