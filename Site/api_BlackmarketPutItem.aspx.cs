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

public partial class api_BlackmarketPutItem : WOApiWebPage
{
    protected override void Execute()
    {
        if (!WoCheckLoginSession())
            return;

        string CustomerID = web.CustomerID();

        Int64 InventoryID = Convert.ToInt64(web.Param("InventoryID"));
        int SinglePrice = web.GetInt("SinglePrice");
        int Quantity = web.GetInt("Quantity");
        int CurrencyType = web.GetInt("CurrencyType");

        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_BlackmarketPutItem";
        sqcmd.Parameters.AddWithValue("@in_CustomerID", Convert.ToInt32(CustomerID));
        sqcmd.Parameters.AddWithValue("@in_InventoryID", InventoryID);
        sqcmd.Parameters.AddWithValue("@in_SinglePrice", SinglePrice);
        sqcmd.Parameters.AddWithValue("@in_Quantity", Quantity);
        sqcmd.Parameters.AddWithValue("@in_CurrencyType", CurrencyType);

        if (!CallWOApi(sqcmd))
            return;

        reader.Read();
        Int64 OutInventoryID = Convert.ToInt64(reader["InventoryID"]);
        int OutQuantity = getInt("Quantity");
        int OutCurrencyType = getInt("CurrencyType");
        int OutMoney = getInt("OutMoney");

        Response.Write("WO_0");
        Response.Write(string.Format("{0} {1} {2} {3}", OutInventoryID, OutQuantity, OutCurrencyType, OutMoney));
    }
}
