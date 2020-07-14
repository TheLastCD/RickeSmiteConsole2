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

        //Storage
        public string Manufacturer, ManufacturerID, Location, BoxName;
        public int RosID, Stock;
        public CheckMiss Whereis;
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

        public StockItem(int R, string M, string MID, BoltAmount BA, string LOC, string BN, CheckMiss C)
        {
            RosID = R;
            Manufacturer = M;
            ManufacturerID = MID;
            Bolts = BA;
            Location = LOC;
            BoxName = BN;
            Whereis = C;
        }

        
    }
}
