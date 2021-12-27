using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFbEntityFramework020
{
    class Program
    {
        static void Main(string[] args)
        {
            string strConnection2FB = makeCoonectionString2FB();

            //connToDBsimple(strConnection2FB);

            var dbcontext = connToDBEntity(strConnection2FB);

        }


        private static DbContext connToDBEntity(string sConnectionString)
        {

            try
            {


                using (var dbContent = new DbAppContext(sConnectionString))
                {



                    var simpleQueryOfVidConnects = dbContent.pO_TEL_VID_CONNECTs.Where(s => s.Id > 0);


                    Console.WriteLine("=================================================");
                    foreach (var oneElement in simpleQueryOfVidConnects)
                    {
                        Console.WriteLine(" Id = {0}  Kod СЃРІСЏР·Рё {1}  РќР°Р·РІР°РЅРёРµ РІРёРґР° СЃРІСЏР·Рё {2}", oneElement.Id, oneElement.KodOfConnect, oneElement.Name);
                    }
                    Console.WriteLine("=================================================");



                    //var dataTable = GetProviderFactoryClasses();

                    var simpleVidConnects = dbContent.pO_TEL_VID_CONNECTs;


                    foreach (var oneTEL_VID_CONNECT in simpleVidConnects)
                    {
                        Console.WriteLine(" Id = {0}  Kod СЃРІСЏР·Рё {1}  РќР°Р·РІР°РЅРёРµ РІРёРґР° СЃРІСЏР·Рё {2}", oneTEL_VID_CONNECT.Id, oneTEL_VID_CONNECT.KodOfConnect, oneTEL_VID_CONNECT.Name);

                    }



                    return dbContent;
                }

            }
            catch (Exception ex)
            {

            }
            return null;

            }





        private static int connToDBsimple(string sConnectionString)
        {
            bool correctConnectionToDB = false;
            int retCount = 0;

            using (DbConnection dbConnection = new FbConnection(sConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    string sSqlSelect = "SELECT * FROM TEL_VID_CONNECT;";
                    //SqlCommand sqlCommand = new SqlCommand(sSqlSelect, (SqlConnection)dbConnection);
                    FbCommand sqlCommand = new FbCommand(sSqlSelect, (FbConnection)dbConnection);


                    FbDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        retCount++;
                        Console.WriteLine("\t{0}\t{1}\t{2}", sqlDataReader[0], sqlDataReader[1], sqlDataReader[2]);
                    }
                    dbConnection.Close();
                    correctConnectionToDB = true;
                }
                catch (FbException fbSqlEx)
                {
                    ;
                }

                catch (SqlException sqlEx)
                {
                    ;
                }
            }
            return retCount;
        }




        private static string makeCoonectionString2FB()
        {

            string strExePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();


            //dbConnectionStringBuilder["ClientLibrary"] = @"C:\Program Files\Firebird\Firebird_2_5\bin\fbclient.dll";

            //dbConnectionStringBuilder["Data Source"] = "localhost";
            dbConnectionStringBuilder["Data Source"] = "127.0.0.1";
            dbConnectionStringBuilder["Initial Catalog"] = @"C:\SSG\PROJECTs\CourseS\TeachThrough\FDBWorking\ConsoleAppFbFramework019\bin\Debug\tmp.fdb";//@"C:\SSG\PROJECTs\TELET\DB4TELEFONE\sampd_cexs.fdb";//"sampd_cexs";
            //dbConnectionStringBuilder["Database"] = Path.Combine(strExePath, "tmp.fdb");

            dbConnectionStringBuilder["User ID"] = "sysdba";
            dbConnectionStringBuilder["Password"] = "masterkey";

            dbConnectionStringBuilder["Charset"] = "UTF8";


            //dbConnectionStringBuilder["Embedded"] = FbServerType.Embedded;
            //dbConnectionStringBuilder["integrated Security"] = "SSPI";

            return dbConnectionStringBuilder.ConnectionString;
        }






    }
}
