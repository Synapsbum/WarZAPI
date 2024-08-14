using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class api_DBG_Injection_Detection : WOApiWebPage
{
    protected override void Execute()
    {
        string skey1 = web.Param("skey1");
        if (skey1 != SERVER_API_KEY)
            throw new ApiExitException("bad key");

        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "ZH_Insert_Injection_Detection";
        sqcmd.Parameters.AddWithValue("@in_CustomerID", web.CustomerID());
        sqcmd.Parameters.AddWithValue("@in_Path", web.Param("path"));
        sqcmd.Parameters.AddWithValue("@in_BaseModule", web.Param("BaseModule"));

        if (!CallWOApi(sqcmd))
            return;

        Response.Write("WO_0");
    }
}
