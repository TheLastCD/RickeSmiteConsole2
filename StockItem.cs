using System;
namespace RickeSmiteConsole
{
    public class StockItem
    {
        //defaults
        public enum CheckMiss
        {
            In_Place,
            Checked_Out,
            Missing
        }
        public enum BoltAmount
        {
           High,
           Low,
           None
        }

        //Stock Items
        public string Manufacturer, ManufacturerID, Location, BoxName;
        public int RosID, Stock;
        public CheckMiss Whereis;

        //Bolts
        public string Supplier, Size;
        public int Length;
        public BoltAmount Bolts;
        

        //Constructors
        public StockItem(int R, string M, string MID,int S, string LOC,string BN, CheckMiss C)
        {
            RosID = R;
            Manufacturer = M;
            ManufacturerID = MID;
            Stock = S;
            Location = LOC;
            BoxName = BN;
            Whereis = C;
        }

        public StockItem(int R, string S, string SZ,int L, BoltAmount BA, string LOC, string BN,CheckMiss C)
        {
            RosID = R;
            Supplier = S;
            Size = SZ + L;
            Bolts = BA;
            Location = LOC;
            BoxName = BN;
            Whereis = C;
            Length = L;
        }

        
    }
}
