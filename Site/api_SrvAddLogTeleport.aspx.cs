using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class api_SrvAddLogTeleport : WOApiWebPage
{
    protected override void Execute()
    {
        string skey1 = web.Param("skey1");
        if (skey1 != SERVER_API_KEY)
            throw new ApiExitException("bad key");        

        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_SRV_AddLogTeleport";       
       
        sqcmd.Parameters.AddWithValue("@in_MapID", web.Param("MapID"));
        sqcmd.Parameters.AddWithValue("@in_TeleportPos", web.Param("TeleportPos"));       

        if (!CallWOApi(sqcmd))
            return;

        Response.Write("WO_0");
    }
}
