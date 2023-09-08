using System.Data;

namespace BMSTestProject
{
    public class SQLTests
    {
        [Fact]
        public void QueryDatatableWithWrongConnectionString()
        {
            //assemble
            SQLFunctions sqlFunctions = new SQLFunctions("wrong connection string");

            //act
            DataTable dt = sqlFunctions.GetDatatableFromQuery("SELECT * FROM master");

            //assert
            dt.Should().NotBeNull();
            dt.Rows.Count.Should().Be(0);
        }

        [Fact]
        public void QueryScalarWithWrongConnectionString()
        {
            //assemble
            SQLFunctions sqlFunctions = new SQLFunctions("wrong connection string");

            //act
            object ret = sqlFunctions.ExecuteScalarQuery("SELECT TOP 1 Name FROM master");

            //assert
            ret.Should().NotBeNull();
        }


        [Fact]
        public void ExecuteNonQueryWithWrongConnectionString()
        {
            //assemble
            SQLFunctions sqlFunctions = new SQLFunctions("wrong connection string");

            //act
            int ret = sqlFunctions.ExecuteNoneQuery("SELECT TOP 1 Name FROM master");

            //assert
            ret.Should().Be(-1);
        }
    }
}