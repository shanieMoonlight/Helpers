using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OdbcSchemaFilesGenerator
{
   public static class ExtensionMethods
   {

      //-----------------------------------------------------------------------------------------//

      /// <summary>
      /// MAke datatabel emumerable
      /// </summary>
      /// <param name="table"></param>
      /// <returns></returns>
      public static IEnumerable<DataRow> AsEnumerable(this DataTable table)
      {

         for (int i = 0; i < table.Rows.Count; i++)
         {
            yield return table.Rows[i];
         }//for

      }//AsEnumerable

      //-----------------------------------------------------------------------------------------//

      /// <summary>
      /// Turn string with underscores into string with camelcase
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public static string UnderscoreToCamelCase(this string name)
      {
         if (string.IsNullOrEmpty(name))
            return name;

         name = name.ToLowerInvariant();


         string[] array = name.Split('_');

         for (int i = 0; i < array.Length; i++)
         {
            string s = array[i];
            string first = string.Empty;
            string rest = string.Empty;

            if (s.Length > 0)
               first = char.ToUpperInvariant(s[0]).ToString();

            if (s.Length > 1)
               rest = s.Substring(1);

            array[i] = first + rest;
         }//for


         return string.Join("", array);

      }//UnderscoreToCamelCase

      //-----------------------------------------------------------------------------------------//

   }//Cls
}//NS