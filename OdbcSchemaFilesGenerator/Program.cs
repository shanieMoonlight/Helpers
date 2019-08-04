using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OdbcSchemaFilesGenerator
{
   class Program
   {
      //----------------------------------------------------------------------------------------------------//


      private static string ConnectionString;
      private static string ColumnNamespace;
      private static string TableNamespace;
      private static ClasssGenerator genny;

      //----------------------------------------------------------------------------------------------------//

      static void Main(string[] args)
      {
         //Set strings and genereator first
         ConnectionString = GetString("Enter connection string:");
         ColumnNamespace = GetColumnNamespaceString();
         TableNamespace = GetTableNamespaceString();
         genny = new ClasssGenerator();

         CreateTableClassFile();

         foreach (var tableName in GetTableNames())
         {
            CreateColumnClassFile(tableName);
         }//foreach

         Console.WriteLine("Boom!!");
         Beep();
         Console.ReadKey();
      }//Main

      //----------------------------------------------------------------------------------------------------//

      /// <summary>
      /// Create a c# class file fith all the ODBC tables as string fields
      /// </summary>
      private static void CreateTableClassFile()
      {
         var strFields = CreateFieldsString(GetTableNames());

         genny.CreateTableClass("Tables", TableNamespace, strFields);

      }//CreateTableClassFile

      //----------------------------------------------------------------------------------------------------//

      /// <summary>
      /// Creeate c# file for table columns as string fields
      /// </summary>
      /// <param name="tableName">Name of table th ecolumns belong to</param>
      private static void CreateColumnClassFile(string tableName)
      {

         var className = tableName.UnderscoreToCamelCase() + "Columns";

         //Get Column names
         var columns = ODBCHelpers.GetColumnNames(ConnectionString, tableName);

         var strFields = CreateFieldsString(columns);

         genny.CreateColumnClass(className, ColumnNamespace, strFields);
      }//CreateColumnClassFile

      //----------------------------------------------------------------------------------------------------//

      /// <summary>
      /// Turn List of strings into  a string of fields
      /// </summary>
      /// <param name="fields">list of fields values/names</param>
      /// <returns>Single string containing ch style string fields</returns>
      private static string CreateFieldsString(IEnumerable<string> fields)
      {

         //Convert to list of fields.
         StringBuilder sbFieldsText = new StringBuilder();

         foreach (var field in fields)
         {
            var camelField = field.UnderscoreToCamelCase();
            sbFieldsText.AppendLine($@"public readonly string {camelField} = ""{field}"";");
         }//foreach

         return sbFieldsText.ToString();

      }//CreateColumnClassFile

      //----------------------------------------------------------------------------------------------------//

      private static IEnumerable<string> GetTableNames()
      {
         return ODBCHelpers.GetTableNames(ConnectionString);
      }//GetTableNames

      //----------------------------------------------------------------------------------------------------//

      private static string GetString(string message)
      {
         Console.WriteLine(message);
         return Console.ReadLine();
      }//GetString

      //----------------------------------------------------------------------------------------------------//

      private static string GetConnectionString()
      {
         return GetString("Enter connection string:");
      }//GetConnectionString

      //----------------------------------------------------------------------------------------------------//

      private static string GetTableNamespaceString()
      {
         return GetString("Enter TABLE class namespace:");
      }//GetConnectionString

      //----------------------------------------------------------------------------------------------------//

      private static string GetColumnNamespaceString()
      {
         return GetString("Enter COLUMN class namespace:");
      }//GetConnectionString

      //----------------------------------------------------------------------------------------------------//

      private static void Beep()
      {
         Console.Beep(300, 500);
         Thread.Sleep(50);
         Console.Beep(300, 500);
         Thread.Sleep(50);
         Console.Beep(300, 500);
         Thread.Sleep(50);
         Console.Beep(250, 500);
         Thread.Sleep(50);
         Console.Beep(350, 250);
         Console.Beep(300, 500);
         Thread.Sleep(50);
         Console.Beep(250, 500);
         Thread.Sleep(50);
         Console.Beep(350, 250);
         Console.Beep(300, 500);
         Thread.Sleep(50);

      }//Beep

      //----------------------------------------------------------------------------------------------------//

   }//Cls

}//NS