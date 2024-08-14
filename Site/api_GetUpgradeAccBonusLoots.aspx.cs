using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class api_GetUpgradeAccBonusLoots : WOApiWebPage
{   
    void OutBonusData(StringBuilder xml)
    {
        xml.Append("<b ");        
        xml.Append(xml_attr("i", reader["ItemID"]));
        xml.Append(xml_attr("a", reader["Amount"]));
        xml.Append("/>");
    }
    
    void GetBonusList()
    {
		string AccType = web.Param("AccType");
		
        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_GetUpgradeAccBonusLootData";
		sqcmd.Parameters.AddWithValue("@in_AccType", AccType);

        if (!CallWOApi(sqcmd))
            return;

        StringBuilder xml = new StringBuilder();
        xml.Append("<?xml version=\"1.0\"?>\n");
        xml.Append("<bonuslist>");

        while (reader.Read())
        {
            OutBonusData(xml);
        }

        xml.Append("</bonuslist>");

        GResponse.Write(xml.ToString());
    }

    protected override void Execute()
    {
        if (!WoCheckLoginSession())
            return;
       
        GetBonusList();
    }
}
