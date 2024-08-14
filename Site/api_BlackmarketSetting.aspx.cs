using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
public partial class api_BlackmarketSetting : WOApiWebPage
{
    protected override void Execute()
    {
        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_GetBlackmarketSetting";

        if (!CallWOApi(sqcmd))
            return;

        Response.Write("WO_0");

        while (reader.Read())
        {
            string opt_name = getString("opt_name");
            //float opt_value = getFloat("opt_value");
            Response.Write(string.Format("{0} {1}\n", opt_name));
        }
    }
}
