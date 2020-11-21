using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OdbcSchemaFilesGenerator.Models
{
   public class ColumnData
   {
      public string Name { get; set; }
      public string ParentTable { get; set; }
      public string Description { get; set; }
      public string DataType { get; set; }
      public int Length { get; set; }

      //-------------------------------------------------------------------//

      public static ColumnData FromDataRow(DataRow r)
      {

         int.TryParse(r.ItemArray[ColumnPositions.LENGTH_IDX].ToString(), out var length);


         return new ColumnData()
         {
            Name = r.ItemArray[ColumnPositions.NAME_IDX].ToString(),
            DataType = r.ItemArray[ColumnPositions.DATA_TYPE_IDX].ToString(),
            Description = r.ItemArray[ColumnPositions.DESCRIPTION_IDX].ToString(),
            ParentTable = r.ItemArray[ColumnPositions.TABLE_IDX].ToString(),
            Length = length,
         };
 
      }//FromItemArray


      //-------------------------------------------------------------------//

      public string ToPropertyString()
      {
         return $@"new ColumnData()
         {{
            Name = ""{Name}"",
            Description = ""{Description}"",
            DataType = ""{DataType}"",
            ParentTable = ""{ParentTable}"",
            Length = {Length},
         }}";

      }//ToPropertyString

      //-------------------------------------------------------------------//

   }//Cls
}//NS
