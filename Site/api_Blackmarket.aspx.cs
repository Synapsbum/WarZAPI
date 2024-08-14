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

class BlackmarketReturn_t
{
    bool valid;
    MemoryStream ms;

    public BlackmarketReturn_t()
    {
        valid = false;
    }

    public BlackmarketReturn_t(MemoryStream ms, bool hasRows)
    {
        valid = hasRows;
        this.ms = ms;
    }

    public bool isValid()
    {
        return valid;
    }

    public MemoryStream GetMemoryStream()
    {
        return ms;
    }
}

public partial class api_Blackmarket : WOApiWebPage
{
    void OutBlackmarketInformation(MemoryStream ms)
    {
        int PK_BlackmarketID = getInt("PK_BlackmarketID");
        int FK_CustomerID = getInt("FK_CustomerID");
        int ItemID = getInt("ItemID");
        int Quantity = getInt("Quantity");
        int SinglePrice = getInt("SinglePrice");
        int CurrencyType = getInt("CurrencyType");
        if (CurrencyType != 4 && CurrencyType != 8)
            return;

        msWriteInt(ms, PK_BlackmarketID);
        msWriteInt(ms, FK_CustomerID);
        msWriteInt(ms, ItemID);
        msWriteInt(ms, Quantity);
        msWriteInt(ms, SinglePrice);
        msWriteInt(ms, CurrencyType);
    }

    BlackmarketReturn_t FetchBlackmarketInformation(int catid)
    {
        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_GetBlackmarket";
        sqcmd.Parameters.AddWithValue("@in_Category", catid);

        if (!CallWOApi(sqcmd))
            return new BlackmarketReturn_t();

        MemoryStream ms = new MemoryStream();

        // all items
        int readCount = 0;
        while (reader.Read())
        {
            OutBlackmarketInformation(ms);
            ++readCount;
        }

        if(readCount == 0)
        {
            return new BlackmarketReturn_t();
        }

        return new BlackmarketReturn_t(ms, readCount != 0);
    }

    public static byte[] Concat(byte[] a, byte[] b)
    {
        byte[] output = new byte[a.Length + b.Length];
        for (int i = 0; i < a.Length; i++)
            output[i] = a[i];
        for (int j = 0; j < b.Length; j++)
            output[a.Length + j] = b[j];
        return output;
    }

    protected override void Execute()
    {
        var catStr = web.Param("Category");
        if (catStr[0] != '[')
        {
            GResponse.Write("WO_1");
            return;
        }

        if (catStr[catStr.Length - 1] != ']')
        {
            GResponse.Write("WO_1");
            return;
        }

        catStr = catStr.Substring(1, catStr.Length - 2);
        if(catStr == "")
        {
            GResponse.Write("WO_0");
            return;
        }

        var catStrArr = catStr.Split(',');
        if(catStrArr.Length == 0)
        {
            GResponse.Write("WO_0");
            return;
        }

        List<BlackmarketReturn_t> listReturn = new List<BlackmarketReturn_t>();
        foreach (var cat in catStrArr)
        {
            var catid = Convert.ToInt32(cat);
            var fetch = FetchBlackmarketInformation(catid);
            if (!fetch.isValid())
                continue;

            listReturn.Add(fetch);
        }

        GResponse.Write("WO_0");

        string header = "BLACKMARKET";
        var byteHeader = Encoding.GetEncoding("UTF-8").GetBytes(header.ToCharArray());

        MemoryStream ms = new MemoryStream();
        ms.Write(byteHeader, 0, byteHeader.Length);

        foreach (var entry in listReturn)
        {
            var tmp = entry.GetMemoryStream().ToArray();
            ms.Write(tmp, 0, tmp.Length);
        }

        ms.Write(byteHeader, 0, byteHeader.Length);
        GResponse.BinaryWrite(ms.ToArray());
    }
}
