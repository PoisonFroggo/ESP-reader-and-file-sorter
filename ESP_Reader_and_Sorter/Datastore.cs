using System.ComponentModel;
using Microsoft.Data.Sqlite;
using SqlKata;
using SqlKata.Compilers;

namespace DataStore;/*
public class DataStore
{

    private SqliteConnection Connection;
    private SqliteCompiler Compiler;

    public DataStore(string storePath)
    {
        Compiler = new SqliteCompiler();
        Connection = new($"Data Source={storePath}; Pooling=false");
        Connection.Open();
        SqliteCommand command = Connection.CreateCommand();
        command.CommandText = "PRAGMA table_info(init_status)";
        var reader = command.ExecuteReader();
        bool databaseInitialized = false;
        while (reader.Read())
        {
            databaseInitialized = true;
        }
        if (!databaseInitialized)
        {
            InitializeDatabase();
        }
    }

    ~DataStore()
    {
        Connection.Close();
    }

    public void Close()
    {
        Connection.Close();
        SqliteConnection.ClearAllPools();
    }

    private void InitializeDatabase()
    {
        string DatabaseInitString = @"CREATE TABLE files (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    file TEXT NOT NULL
);


CREATE TABLE tags (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    tag TEXT NOT NULL UNIQUE
);


CREATE TABLE file_tags (
    file_id INTEGER,
    tag_id INTEGER,
    PRIMARY KEY (file_id, tag_id),
    FOREIGN KEY (file_id) REFERENCES files (id),
    FOREIGN KEY (tag_id) REFERENCES tags (id)
);

CREATE TABLE init_status (
    id INTEGER PRIMARY KEY CHECK (id = 0),
    is_init INTEGER NOT NULL
);

INSERT INTO init_status VALUES (0, 1);";
        using (Connection)
        {
            Connection.Open();
            SqliteCommand initCommand = Connection.CreateCommand();
            initCommand.CommandText = DatabaseInitString;
            initCommand.ExecuteReader();
            initCommand.Dispose();
        }
    }

    /// <summary>
    /// Gets all the stored tagged files
    /// </summary>
    /// <returns>A list of file paths as strings</returns>
    public HashSet<string> GetFiles()
    {
        SqliteCommand command = Connection.CreateCommand();
        var query = new Query("files").Select("file");
        command.CommandText = Compiler.Compile(query).Sql;
        return Util.Read(0, command).ToHashSet();
    }

    /// <summary>
    /// Gets all files which each have all the <paramref name="tags"/> provided
    /// </summary>
    /// <param name="tags">A hashset corresponding to the unique tags to include in the search</param>
    /// <returns>A list of file paths as strings</returns>
    public HashSet<string> GetFilesByTagIntersection(HashSet<string> tags)
    {
        // Joins the vertex table to the other tables, searches for all files which have at least one of the tags, then filters the results for files with all the tags
        var query = new Query("files")
            .Select(new string[] { "files.file" }).SelectRaw("COUNT(files.id) as Count")
            .Join("file_tags", "file_tags.file_id", "files.id")
            .Join("tags", "tags.id", "file_tags.tag_id")
            .WhereIn("tags.tag", tags)
            .GroupBy("files.file")
            .Having("Count", "=", tags.Count);
        SqliteCommand command = Connection.CreateCommand();
        Util.InjectSqlIntoCommand(command, Compiler, query);
        return Util.Read(0, command).ToHashSet();
    }

    public HashSet<string> GetFilesByTagUnion(HashSet<string> tags)
    {
        var query = new Query("files")
            .Select("file").Distinct()
            .Join("file_tags", "file_tags.file_id", "files.id")
            .Join("tags", "tags.id", "file_tags.tag_id")
            .WhereIn("tags.tag", tags);
        SqliteCommand command = Connection.CreateCommand();
        Util.InjectSqlIntoCommand(command, Compiler, query);
        return Util.Read(0, command).ToHashSet();
    }

    /// <summary>
    /// Stores filepaths
    /// </summary>
    /// <param name="files">A list of file paths to store</param>
    public void AddFiles(HashSet<string> files)
    {
        var col = new[] { "file" };
        // Reformat the input as a collection of arrays for the insert query
        var filesAsRows = files.Select(file => new string[] { file });
        var query = new Query("files").AsInsert(col, filesAsRows);
        SqliteCommand command = Connection.CreateCommand();
        var compiledQuery = Compiler.Compile(query);
        command.CommandText = compiledQuery.Sql;
        command.Parameters.AddRange(compiledQuery.NamedBindings.Select(pair => new SqliteParameter(pair.Key, pair.Value)));
        Util.Create(command);
    }

    /// <summary>
    /// Deletes the given <paramref name="file"/> from the store
    /// </summary>
    /// <param name="file">The path of the file to delete. This must exist in the store.</param>
    /// <exception cref="Datastore.Exceptions.NotFoundException">Raised when the file is not found in the store.</exception>
    public void DeleteFile(string file)
    {
        var query = new Query("files").AsDelete().Where("file", file);
        var command = Connection.CreateCommand();
        Util.InjectSqlIntoCommand(command, Compiler, query);
        int rowsDeleted = Util.Delete(command);
        if (rowsDeleted == 0)
        {
            throw new Exceptions.NotFoundException($"No file named {file} was found in the store.");
        }
    }

    /// <summary>
    /// Gets all tags
    /// </summary>
    /// <returns>A list of tags as strings</returns>
    public HashSet<string> GetTags()
    {
        var query = new Query("tags").Select("tag");
        SqliteCommand command = Connection.CreateCommand();
        command.CommandText = Compiler.Compile(query).Sql;
        // This syntax is equivalent to `return Util.Read(command).ToHashset();`
        return [..Util.Read(0, command)];
    }

    /// <summary>
    /// Gets all tags for a given file
    /// </summary>
    /// <param name="file"> A string representing the path of the file</param>
    /// <returns></returns>
    public HashSet<string> GetTags(string file)
    {
        SqliteCommand command = Connection.CreateCommand();
        var query = new Query("tags")
            .Select("tag")
            .Join("file_tags", "tags.id", "file_tags.tag_id")
            .Join("files", "file_tags.file_id", "files.id")
            .Where("file", file);
        Util.InjectSqlIntoCommand(command, Compiler, query);
        // This syntax is equivalent to `return Util.Read(command).ToHashset();`
        return [..Util.Read(0, command)];
    }

    /// <summary>
    /// Adds tags
    /// </summary>
    /// <param name="tags">a unique collection of tags to store</param>
    public void AddTags(HashSet<string> tags)
    {
        var tagColumn = new string[] { "tag" };
        // Reformat the input as a collection of arrays for the insert query
        IEnumerable<string[]> tagsAsRows = tags.Select(tag => new string[] { tag });
        var query = new Query("tags").AsInsert(tagColumn, tagsAsRows);
        var compiledQuery = Compiler.Compile(query);
        SqliteCommand command = Connection.CreateCommand();
        command.CommandText = compiledQuery.Sql;
        command.Parameters.AddRange(compiledQuery.NamedBindings.Select(pair => new SqliteParameter(pair.Key, pair.Value)));
        Util.Create(command);
    }

    /// <summary>
    /// Deletes the given <paramref name="tag"/> from the store
    /// </summary>
    /// <param name="tag">The tag to delete. This must exist in the store.</param>
    /// <exception cref="Exceptions.NotFoundException">Raised when the tag is not found in the store.</exception>
    public void DeleteTag(string tag)
    {
        var query = new Query("tags").AsDelete().Where("tag", tag);
        var command = Connection.CreateCommand();
        Util.InjectSqlIntoCommand(command, Compiler, query);
        int rowsDeleted = Util.Delete(command);
        if (rowsDeleted == 0)
        {
            throw new Exceptions.NotFoundException($"No tag named {tag} was found in the store.");
        }
    }

    /// <summary>
    /// Associates a file with a tag.
    /// </summary>
    /// <param name="file">The file to tag. This must already be stored.</param>
    /// <param name="tag">The tag to associate with the file. This must already be stored.</param>
    /// <exception cref="Datastore.Exceptions.NotFoundException">Thrown when the provided file or tag is not found in the data store.</exception>
    public void TagFile(string file, string tag)
    {
        var queryFileId = new Query("files").Select("id").Where("file", file);
        SqliteCommand fileIdCommand = Connection.CreateCommand();
        Util.InjectSqlIntoCommand(fileIdCommand, Compiler, queryFileId);
        int fileId;
        try
        {
            fileId = Int32.Parse(Util.Read(0, fileIdCommand)[0]);
        }
        catch (SqliteException)
        {
            throw new Datastore.Exceptions.NotFoundException($"The file '{file}' was not found in the data store.");
        }
        var queryTagId = new Query("tags").Select("id").Where("tag", tag);
        SqliteCommand tagIdCommand = Connection.CreateCommand();
        Util.InjectSqlIntoCommand(tagIdCommand, Compiler, queryTagId);
        int tagId;
        try
        {
            tagId = Int32.Parse(Util.Read(0, tagIdCommand)[0]);
        }
        catch (SqliteException)
        {
            throw new Datastore.Exceptions.NotFoundException($"The tag '{tag}' was not found in the data store.");
        }
        var insertQuery = new Query("file_tags").AsInsert(new { file_id = fileId, tag_id = tagId });
        var insertCommand = Connection.CreateCommand();
        Util.InjectSqlIntoCommand(insertCommand, Compiler, insertQuery);
        Util.Create(insertCommand);
    }

    /// <summary>
    /// Dissociates a tag from a file.
    /// </summary>
    /// <param name="file">The path of the file. This must exist in the store.</param>
    /// <param name="tag">The tag to remove. This must exist in the store.</param>
    /// <exception cref="Exceptions.NotFoundException">Thrown when the file or tag was not found in the store.</exception>
    public void UntagFile(string file, string tag)
    {
        var queryFileId = new Query("files").Select("id").Where("file", file);
        SqliteCommand fileIdCommand = Connection.CreateCommand();
        Util.InjectSqlIntoCommand(fileIdCommand, Compiler, queryFileId);
        int fileId;
        try
        {
            fileId = Int32.Parse(Util.Read(0, fileIdCommand)[0]);
        }
        catch (SqliteException)
        {
            throw new Datastore.Exceptions.NotFoundException($"The file '{file}' was not found in the data store.");
        }
        var queryTagId = new Query("tags").Select("id").Where("tag", tag);
        SqliteCommand tagIdCommand = Connection.CreateCommand();
        Util.InjectSqlIntoCommand(tagIdCommand, Compiler, queryTagId);
        int tagId;
        try
        {
            tagId = Int32.Parse(Util.Read(0, tagIdCommand)[0]);
        }
        catch (SqliteException)
        {
            throw new Datastore.Exceptions.NotFoundException($"The tag '{tag}' was not found in the data store.");
        }
        var query = new Query("file_tags").AsDelete()
            .Where("file_id", fileId)
            .Where("tag_id", tagId);
        var command = Connection.CreateCommand();
        Util.InjectSqlIntoCommand(command, Compiler, query);
        Util.Delete(command);
    }
}
*/