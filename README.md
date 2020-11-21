# Helpers
Application to help with writing code


This is a program to create data classes for the tables and columns in your ODBC database.

It creates a .cs file for all the tables eg.

Tables.cs:

      namespace SgTbls
      {
         class Tables
         {

          public const string AccountStatus = "ACCOUNT_STATUS";
          public const string Accrual = "ACCRUAL";
          public const string AuditHeader = "AUDIT_HEADER";
          ...
          ...
          ...
        }
      }

It then creates a folder of different classes for the columns if each table eg.
AccountStatusColumns.cs:

      namespace SgCols
      {
         class AccountStatusColumns
         {
            public const string Number = "NUMBER";
            public const string OnHold = "ON_HOLD";
            public const string RecordCreateDate = "RECORD_CREATE_DATE";
            public const string RecordDeleted = "RECORD_DELETED";
            public const string RecordModifyDate = "RECORD_MODIFY_DATE";
            public const string Status = "STATUS";

         }//Cls
      }//NS


Finally it creates a folder of different classes for the data in each column of the table eg.

AccountStatusColumnsData.cs:

      using  OdbcSchemaFilesGenerator.Models;
      namespace SgCols
      {
         class AccountStatusColumnsData
         {

           public static readonly ColumnData Number = new ColumnData()
               {
                  Name = "NUMBER",
                  Description = "Record Number",
                  DataType = "SMALLINT",
                  ParentTable = "ACCOUNT_STATUS",
                  Length = 5,
               };
         public static readonly ColumnData OnHold = new ColumnData()
               {
                  Name = "ON_HOLD",
                  Description = "Account On Hold",
                  DataType = "TINYINT",
                  ParentTable = "ACCOUNT_STATUS",
                  Length = 3,
               };
          public static readonly ColumnData RecordCreateDate = new ColumnData()
               {
                  Name = "RECORD_CREATE_DATE",
                  Description = "Date and time when the record was created",
                  DataType = "",
                  ParentTable = "ACCOUNT_STATUS",
                  Length = 0,
               };
          public static readonly ColumnData RecordDeleted = new ColumnData()
               {
                  Name = "RECORD_DELETED",
                  Description = "Flag denoting if the record has been deleted or not.",
                  DataType = "SMALLINT",
                  ParentTable = "ACCOUNT_STATUS",
                  Length = 5,
               };
          public static readonly ColumnData RecordModifyDate = new ColumnData()
               {
                  Name = "RECORD_MODIFY_DATE",
                  Description = "Date and time when the record was modified",
                  DataType = "",
                  ParentTable = "ACCOUNT_STATUS",
                  Length = 0,
               };
          public static readonly ColumnData Status = new ColumnData()
               {
                  Name = "STATUS",
                  Description = "Account Status",
                  DataType = "VARCHAR",
                  ParentTable = "ACCOUNT_STATUS",
                  Length = 60,
               };
         }//Cls
      }//NS



Copy and paste these files into your solutions and then you can use them in your query strings etc. 
Like this:
      
      string queryString = $@"SELECT 
                                    {Tables.Customer}.{CustomerColumns.AccountRef},
                                    {Tables.Customer}.{CustomerColumns.Name},
                                    {Tables.Customer}.{CustomerColumns.Address1},
                                    {Tables.Customer}.{CustomerColumns.Address2},
                                    {Tables.Customer}.{CustomerColumns.Address3},
                                    {Tables.Customer}.{CustomerColumns.Address4},
                                    {Tables.Customer}.{CustomerColumns.Address5},
                                    {Tables.Customer}.{CustomerColumns.Email},
                                    {Tables.Customer}.{CustomerColumns.Telephone},
                                    {Tables.Customer}.{CustomerColumns.DefTaxCode},
                                    {Tables.Currency}.{CurrencyColumns.Rate},
                                    {Tables.Currency}.{CurrencyColumns.Code},
                                    {Tables.Currency}.{CurrencyColumns.Name},
                                   FROM
                                    { Tables.Customer}
                                   INNER JOIN 
                                    { Tables.Currency}
                                   ON
                                     {Tables.Customer}.{ CustomerColumns.Currency} = { Tables.Currency}.{ CurrencyColumns.Number}
                                   ORDER BY
                                    { Tables.Customer}.{ CustomerColumns.Name}"
                              ;

