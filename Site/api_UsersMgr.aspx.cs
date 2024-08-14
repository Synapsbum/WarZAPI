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

public partial class api_UsersMgr : WOApiWebPage
{
    void Teleport()
    {
        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_Teleport";
        sqcmd.Parameters.AddWithValue("@in_CustomerID", web.CustomerID());
        sqcmd.Parameters.AddWithValue("@in_CharID", web.Param("charid"));
        sqcmd.Parameters.AddWithValue("@in_Pos", web.Param("Pos"));
        sqcmd.Parameters.AddWithValue("@in_MapID", web.Param("MapID"));

        if (!CallWOApi(sqcmd))
            return;

        reader.Read();
        Response.Write("WO_0");
    }   

    protected override void Execute()
    {
        if (!WoCheckLoginSession())
            return;

        string func = web.Param("func");

       if (func == "teleport")
            Teleport();
        else
            throw new ApiExitException("bad func");
    }
}
