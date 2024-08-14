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

public partial class api_VipItem : WOApiWebPage
{
    protected override void Execute()
    {
        string skey1 = web.Param("skey1");
        if (skey1 != SERVER_API_KEY)
            throw new ApiExitException("bad key");

        string CustomerID = web.CustomerID();       
        string ItemID = web.Param("ItemID");

        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "FN_AddVipItemToUserInv";
        sqcmd.Parameters.AddWithValue("@in_CustomerID", CustomerID);        
        sqcmd.Parameters.AddWithValue("@in_ItemID", ItemID);

        if (!CallWOApi(sqcmd))
            return;

        Response.Write("WO_0");
        return;
    }
}
