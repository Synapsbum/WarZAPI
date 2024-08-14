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

public partial class api_BlackmarketRemoveItem : WOApiWebPage
{
    protected override void Execute()
    {
        if (!WoCheckLoginSession())
            return;

        string CustomerID = web.CustomerID();

        int PK_BlackmarketID = web.GetInt("PK_BlackmarketID");
        int Quantity = web.GetInt("Quantity");

        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_BlackmarketRemoveItem";
        sqcmd.Parameters.AddWithValue("@in_CustomerID", Convert.ToInt32(CustomerID));
        sqcmd.Parameters.AddWithValue("@in_PK_BlackmarketID", PK_BlackmarketID);
        sqcmd.Parameters.AddWithValue("@in_Quantity", Quantity);

        if (!CallWOApi(sqcmd))
            return;

        reader.Read();
        int InventoryID = getInt("InventoryID");
        int ItemID = getInt("ItemID");
        int OutQuantity = getInt("Quantity");
        int Var1 = getInt("Var1");
        int Var2 = getInt("Var2");
        int Var3 = getInt("Var3");

        Response.Write("WO_0");
        Response.Write(string.Format("{0} {1} {2} {3} {4} {5}", InventoryID, ItemID, OutQuantity, Var1, Var2, Var3));
    }
}
