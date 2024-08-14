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

//AlexRedd:: Item Rewards System
public partial class api_RewardItem : WOApiWebPage
{
    string CustomerID = null;
    int ItemID = 0; 

    void RewardItemJob()
    {
        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_RewardItem";
        sqcmd.Parameters.AddWithValue("@in_CustomerID", CustomerID);
        sqcmd.Parameters.AddWithValue("@in_ItemID", ItemID);        

        if (!CallWOApi(sqcmd))
            return;
    }

    protected override void Execute()
    {
        string skey1 = web.Param("skey1");
        if (skey1 != SERVER_API_KEY)
            throw new ApiExitException("bad key");

        CustomerID = web.CustomerID();
        ItemID = web.GetInt("ItemID");       
                
        RewardItemJob();      

        Response.Write("WO_0");       
    }
}
