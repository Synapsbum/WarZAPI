using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class api_GetTeleportData : WOApiWebPage
{
    void OutTeleportData(StringBuilder xml)
    {
        xml.Append("<t ");        
        xml.Append(xml_attr("MapID", reader["MapID"]));
        xml.Append(xml_attr("TeleportPos", reader["TeleportPos"]));        
        xml.Append("/>");
    }   

    void GetTeleportPoints()
    {
        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_GetTeleportData";

        if (!CallWOApi(sqcmd))
            return;

        StringBuilder xml = new StringBuilder();
        xml.Append("<?xml version=\"1.0\"?>\n");
        xml.Append("<getpoints>");

        while (reader.Read())
        {
            OutTeleportData(xml);
        }

        xml.Append("</getpoints>");

        GResponse.Write(xml.ToString());
    }    

    protected override void Execute()
    {
        if (!WoCheckLoginSession())
            return;

        GetTeleportPoints();        
    }
}
