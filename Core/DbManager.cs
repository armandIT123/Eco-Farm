using Data;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Core;

public class DbManager
{
    public static List<List<object>> Select(string? connctionString, string table, string[] columns, string sqlFilter = "", string orderBy = "")
    {
        try
        {
            if (string.IsNullOrEmpty(connctionString))
                return null;

            List<List<object>> retVal = new List<List<object>>();

            string commandString = $"SELECT * From {table}";
            if (!string.IsNullOrEmpty(sqlFilter))
                commandString += $" WHERE {sqlFilter}";
            if (!string.IsNullOrEmpty(orderBy))
                commandString += $" ORDER BY {orderBy}";

            using (SqlConnection connection = new SqlConnection(connctionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(commandString, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (null != reader && reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<object> values = new List<object>();
                            for (int i = 0; i < columns.Length; i++)
                            {
                                values.Add(reader[columns[i]]);
                            }
                            retVal.Add(values);
                        }
                    }
                }
            }

            return retVal;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public static List<T> Select<T>(string connectionString, string table, string[] columns, string sqlFilter = "", string orderBy = "") where T : new()
    {
        try
        {
            if (string.IsNullOrEmpty(connectionString))
                return null;

            List<T> retVal = new List<T>();
            string commandString = $"SELECT {string.Join(", ", columns)} FROM {table}";
            if (!string.IsNullOrEmpty(sqlFilter))
                commandString += $" WHERE {sqlFilter}";
            if (!string.IsNullOrEmpty(orderBy))
                commandString += $" ORDER BY {orderBy}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(commandString, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            T item = new T();
                            foreach (PropertyInfo prop in typeof(T).GetProperties())
                            {
                                if (Attribute.IsDefined(prop, typeof(IgnorePropertyAttribute)))
                                    continue;

                                if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                                {
                                    prop.SetValue(item, reader[prop.Name]);
                                }
                            }
                            retVal.Add(item);
                        }
                    }
                }
            }

            return retVal;
        }
        catch (Exception ex)
        {
            // Consider logging the exception
            return null;
        }
    }

    public static int? InsertAndReturnId(string? connectionString, string table, string[] columns, object[] values)
    {
        if (columns == null || columns.Length == 0 || values == null || values.Length == 0)
            return null;
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO {table} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", columns.Select(c => "@" + c))})";

                    for (int i = 1; i < columns.Length; i++) // jump over ID column
                    {
                        command.Parameters.AddWithValue("@" + columns[i], values[i]);
                    }
                    var result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))                   
                        return id;                    
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static bool Insert(string? connectionString, string table, string[] columns, List<object[]> valuesList) // to be tested
    {
        if (columns == null || columns.Length == 0 || valuesList == null || valuesList.Count == 0 || valuesList.Any(v => v.Length != columns.Length))
            return false;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;

                    var valueParameters = new List<string>();
                    for (int i = 0; i < valuesList.Count; i++)
                    {
                        var valueParams = columns.Select((c, j) => $"@{c}{i}{j}").ToArray();
                        valueParameters.Add($"({string.Join(", ", valueParams)})");

                        for (int j = 1; j < columns.Length; j++) // jump over ID column
                        {
                            command.Parameters.AddWithValue($"@{columns[j]}{i}{j}", valuesList[i][j]);
                        }
                    }
                    command.CommandText = $"INSERT INTO {table} ({string.Join(", ", columns)}) VALUES {string.Join(", ", valueParameters)}";
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            // Consider logging the exception details
            return false;
        }
    }

    public static bool Update(string? connectionString, string table, string columnToUpdate, object newValue, string filterColumn, object filterValue)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"UPDATE {table} SET {columnToUpdate} = @newValue WHERE {filterColumn} = @filterValue";
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@filterValue", filterValue);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool? EmailExists(string connectionString, string table, string email)
    {
        string query = $"SELECT COUNT(1) FROM {table} WHERE (Email) = @Email";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static bool Delete(string connectionString, string table)
    {
        try
        {
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
