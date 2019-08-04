using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;

namespace OdbcSchemaFilesGenerator
{
   public class ODBCHelpers
   {
      //-------------------------------------------------------------------------------------------------------//

      /// <summary>
      /// 
      /// </summary>
      /// <param name="connectionString"></param>
      /// <param name="tableName"></param>
      /// <returns></returns>
      public static List<string> GetColumnNames(string connectionString, string tableName)
      {
         List<string> table = new List<string>();


         using (OdbcConnection connection = new OdbcConnection(connectionString))
         {
            using (OdbcDataAdapter adapter = new OdbcDataAdapter("", connection))
            {
               connection.Open();

               var dtCols = connection.GetSchema("Columns", new[] { connection.DataSource, null, tableName })
                              .AsEnumerable()
                              .Select(r => r.ItemArray[3])
                              .OrderBy(r => r.ToString());

               foreach (var column in dtCols)
                  table.Add(column.ToString());

            }//Using
         }//Using

         return table;
      }//GetColumnNames

      //-------------------------------------------------------------------------------------------------------//
      /// <summary>
      /// 
      /// </summary>
      /// <param name="connectionString"></param>
      /// <param name="tableName"></param>
      /// <returns></returns>
      public static List<string> GetTableNames(string connectionString)
      {
         List<string> database = new List<string>();


         using (OdbcConnection connection = new OdbcConnection(connectionString))
         {
            using (OdbcDataAdapter adapter = new OdbcDataAdapter("", connection))
            {
               connection.Open();

               var dtTables = connection.GetSchema("Tables")
                        .AsEnumerable()
                        .Select(r => r.ItemArray[2])
                        .OrderBy(r => r.ToString());

               foreach (var table in dtTables)
                  database.Add(table.ToString());

            }//Using
         }//Using

         return database;
      }//GetTableNames

      //-------------------------------------------------------------------------------------------------------//

   }//Cls
}//NS