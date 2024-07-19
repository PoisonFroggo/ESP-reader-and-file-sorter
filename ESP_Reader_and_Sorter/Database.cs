using System.Data.SQLite;
using System.IO;

public static class SQLiteHelper
{
    private static string connectionString = @"Data Source=..\..\Files\ModsToolkit.db;Version=3;";
    
    public static void InitializeDatabase()
    {
        if (!File.Exists(@"..\..\Files\ModsToolkit.db"))
        {
            SQLiteConnection.CreateFile(@"..\..\Files\ModsToolkit.db");

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                //Creating table if it isn't there
                string createAccountsTableQuery = @"
                CREATE TABLE IF NOT EXISTS accounts (
	                ID	INTEGER NOT NULL,
	                username	TEXT,
	                pw	TEXT,
	                gameName	TEXT,
	                PRIMARY KEY(ID AUTOINCREMENT)
                );   ";

                string createESPTableQuery = @"
                CREATE TABLE IF NOT EXISTS esp (
	                gameName	TEXT,
	                fileName	TEXT,
	                espPath	TEXT,
	                ID		INTEGER NOT NULL,
	                accountID INTEGER NOT NULL, 
	                PRIMARY KEY(ID AUTOINCREMENT),
	                CONSTRAINT FK_AccountEsp FOREIGN KEY(accountID) REFERENCES accounts(ID)
                );  ";

                string createFilesTableQuery = @"
                CREATE TABLE IF NOT EXISTS files (
	                fileName	TEXT,
	                filePath	TEXT,
	                prereq	TEXT,
	                espID		INTEGER NOT NULL,
	                PRIMARY KEY(fileName),
	                CONSTRAINT FK_EspFiles FOREIGN KEY(espID) REFERENCES esp(ID)
                );  ";

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = createAccountsTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createESPTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createFilesTableQuery;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
