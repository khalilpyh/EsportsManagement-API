/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using Microsoft.EntityFrameworkCore.Migrations;

namespace EsportsManagementAPI.Data
{
	public static class ExtraMigration
	{
		public static void Steps(MigrationBuilder migrationBuilder)
		{
			//Player Table Triggers for Concurrency
			migrationBuilder.Sql(
				@"
                    CREATE TRIGGER SetPlayerTimestampOnUpdate
                    AFTER UPDATE ON Players
                    BEGIN
                        UPDATE Players
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
			migrationBuilder.Sql(
				@"
                    CREATE TRIGGER SetPlayerTimestampOnInsert
                    AFTER INSERT ON Players
                    BEGIN
                        UPDATE Players
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
		}
	}
}
