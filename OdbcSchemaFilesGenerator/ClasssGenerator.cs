using System;
using System.IO;
using System.Text;

namespace OdbcSchemaFilesGenerator
{
   internal class ClasssGenerator
   {
      private static string _tablesFilePath;
      private static string _tablesFilename = "tables.txt";
      private static string _classTemplateFilePath;
      private static string _classTemplateFilename = "column_class_template.txt";

      private readonly string _baseDirectoryPath;

      //---------------------------------------------------------------------------------------------//

      public ClasssGenerator()
      {
         _tablesFilePath = AppDomain.CurrentDomain.BaseDirectory
            .Replace(@"bin\Release\netcoreapp2.2\win10-x64\", _tablesFilename)
            .Replace(@"bin\Debug\netcoreapp2.2\", _tablesFilename)
            .Replace(@"bin\Release\netcoreapp2.2\", _tablesFilename);

         _classTemplateFilePath = AppDomain.CurrentDomain.BaseDirectory
            .Replace(@"bin\Release\netcoreapp2.2\win10-x64\", _classTemplateFilename)
            .Replace(@"bin\Debug\netcoreapp2.2\", _classTemplateFilename)
            .Replace(@"bin\Release\netcoreapp2.2\", _classTemplateFilename);

         var desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
         _baseDirectoryPath = Path.Combine(desktop, "ODBC_Schema");
      }//ctor

      //---------------------------------------------------------------------------------------------//

      /// <summary>
      /// Create c# class file for the Table definitions
      /// </summary>
      /// <param name="className">name of class</param>
      /// <param name="fields">list of string fields in the class</param>
      public void CreateColumnClass(string className, string nameSpace, string fields, string folder = "Columns", string usings = "")
      {
         CreateClass(className, nameSpace, fields, folder, usings);

      }//CreateClass

      //---------------------------------------------------------------------------------------------//

      /// <summary>
      /// Create c# class file for the Column definitions
      /// </summary>
      /// <param name="className">name of class</param>
      /// <param name="fields">list of string fields in the class</param>
      public void CreateTableClass(string className, string nameSpace, string fields)
      {
         CreateClass(className, nameSpace, fields, "Tables");

      }//CreateClass

      //---------------------------------------------------------------------------------------------//

      /// <summary>
      /// Create and store a c# class file
      /// </summary>
      /// <param name="className">name of class</param>
      /// <param name="fields">list of string fields in the class</param>
      /// <param name="folder">where to put the file</param>
      private void CreateClass(string className, string nameSpace, string fields, string folder, string usings = "")
      {
         //read the Excel file as byte array
         using (var sr = File.OpenText(_classTemplateFilePath))
         {
            //Read file
            var classTemplate = sr.ReadToEnd();
            //Replace placeholders
            var newClasstemplate = classTemplate
               .Replace("--using--", usings)
               .Replace("--fields--", fields)
               .Replace("--classname--", className)
               .Replace("--namespace--", nameSpace);

            CreateCSFile(className, newClasstemplate, folder);
         } //Using

      }//CreateClass

      //---------------------------------------------------------------------------------------------//

      /// <summary>
      /// Creates c# class file
      /// </summary>
      /// <param name="className">name of class</param>
      /// <param name="calssAsTxt">string version of class</param>
      /// <param name="folder">where to put the file</param>
      private void CreateCSFile(string className, string calssAsTxt, string folder)
      {
         //Create directory
         var directory = Path.Combine(_baseDirectoryPath, folder);
         var info = Directory.CreateDirectory(directory);

         //Create filename
         string fileName = Path.Combine(directory, className + ".cs");

         // Check if file already exists. If yes, clear it.     
         if (File.Exists(fileName))
            File.WriteAllText(fileName, string.Empty);


         // Create a new file     
         using (FileStream fs = File.Create(fileName))
         {
            // Add some text to file    
            byte[] data = new UTF8Encoding(true).GetBytes(calssAsTxt);
            fs.Write(data, 0, data.Length);
         }//using

      }//CreateCSFile

      //---------------------------------------------------------------------------------------------//


   }//Cls
}//NS