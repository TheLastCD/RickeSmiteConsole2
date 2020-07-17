using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Timers;
namespace RickeSmiteConsole
{
    public class Manager
    {
        // /Users/charlie/Projects/RickeSmiteConsole2/Collection/Collection.txt (Mac solution Location)
        // C:\Users\lambo\source\repos\RickeSmite Console 2\Collection\Collection.txt (Windows Solution Location)
        public List<StockItem> StockList = new List<StockItem>();
        public List<int> EditsMade = new List<int>();

        string Path = @"Collection.txt";
        public void FillBase()
        {
            foreach(string item in File.ReadLines(Path))
            {
                if (item == "")
                {

                }
                else
                {
                    string[] itemAttributes = item.Split(',');

                    StockItem.CheckMiss holder = StockItem.CheckMiss.In_Place;
                    if (itemAttributes[6] == "Checked_out")
                        holder = StockItem.CheckMiss.Checked_Out;
                    if (itemAttributes[6] == "Missing")
                        holder = StockItem.CheckMiss.Missing;

                    try// passes Stock Item
                    {
                        Int32.Parse(itemAttributes[3]);
                        StockList.Add(new StockItem(Int32.Parse(itemAttributes[0]), itemAttributes[1], itemAttributes[2], Int32.Parse(itemAttributes[3]), itemAttributes[4], itemAttributes[5], holder));
                    }
                    catch//if Caught its a Bolt
                    {
                        string[] Seperate_Bolt_Entities = itemAttributes[2].Split(':');

                        StockItem.BoltType BT = StockItem.BoltType.ButtonHeads;
                        if (Seperate_Bolt_Entities[0] == "SocketHeads")
                            BT = StockItem.BoltType.SocketHeads;
                        if(Seperate_Bolt_Entities[0] == "CountersunkHead")
                            BT = StockItem.BoltType.CountersunkHead;
                        if (Seperate_Bolt_Entities[0] == "NormalHeads")
                            BT = StockItem.BoltType.NormalHeads;

                        StockItem.BoltAmount BA = StockItem.BoltAmount.High;
                        if (itemAttributes[3] == "Low")
                            BA = StockItem.BoltAmount.Low;
                        if (itemAttributes[3] == "None")
                            BA = StockItem.BoltAmount.None;

                        StockList.Add(new StockItem(Int32.Parse(itemAttributes[0]), itemAttributes[1], Seperate_Bolt_Entities[1],Int32.Parse(Seperate_Bolt_Entities[2]),BT, BA, itemAttributes[4], itemAttributes[5], holder));
                    }
                   
                }
            }
        }

        public void MiddleMan()
        {
            Console.WriteLine("Please enter: \n1) for Stock Items \n2) for Bolts");
            switch (Int_Verify(2))
            {
                case 1:
                    Additem_StockItem();
                    break;
                case 2:
                    Additem_Bolt();
                    break;
                default:
                    Console.WriteLine("there has been an unexpected error");
                    break;
            }
            
        }
        public void Additem_StockItem()
        {
            int nextROS = 0;
            try
            {
                nextROS = StockList.Last().RosID + 1;
            }
            catch
            {
            }
            
            Console.Write("Please enter an manufacturer:");
            string manu = Verifier(false).ToLower();
            Console.Write("Please enter an Part Number:");
            string MID = Verifier(false).ToLower();
            Console.Write("Please enter stock amount:");
            int stock = Int32.Parse(Verifier(true));
            Console.Write("Please enter the location:");
            string LOC = Verifier(false).ToLower();
            Console.Write("Please enter the box name:");
            string BN = Verifier(false).ToLower();
            Console.Write("Please enter the State of this item (1) Checked Out 2)In_Place 3) Missing)");
            StockItem.CheckMiss State= StockItem.CheckMiss.Checked_Out;
            switch (Int_Verify(3))
            {
                case 1:
                    break;
                case 2:
                    State = StockItem.CheckMiss.In_Place;
                    break;
                case 3:
                    State = StockItem.CheckMiss.Missing;
                    break;
                default:
                    Console.WriteLine("Chose an incorrect number its defaulted to Checked Out");
                    break;
            }
            NewEntity(nextROS, manu, MID, stock, LOC, BN, State);
            while (true)
            {
                Console.WriteLine("Would you like to enter another item: Y/N");
                string redo = Verifier(false);
                if (redo.ToUpper() == "Y")
                {
                    Additem_StockItem();
                }
                if(redo.ToUpper() == "N")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid response");
                }

            }
        }
        private void NewEntity(int R, string M, string MID, int S, string LOC, string BN, StockItem.CheckMiss C)
        {
            StockItem item = new StockItem(R,M,MID,S,LOC,BN,C);
            StockList.Add(item);
            string toSave = Environment.NewLine + $"{R},{M},{MID},{S},{LOC},{BN},{C}";
            if (!File.Exists(Path))
            {
                Console.WriteLine("The file cannot be found");
            }
            else
            {
                using (StreamWriter sw = File.AppendText(Path))
                {
                    sw.WriteLine(toSave);
                }
            }

        }

        public void Additem_Bolt()
        {
            int nextROS = 0;
            try
            {
                nextROS = StockList.Last().RosID + 1;
            }
            catch
            {
            }

            Console.Write("Please enter an supplier:");
            string Sup = Verifier(false).ToLower();

            Console.Write("Please enter an Size:");
            string Size = Verifier(false).ToLower();

            Console.WriteLine("please enter the Length of the bolt");
            int Length = Int32.Parse(Verifier(true));

            Console.Write("Please enter stock amount: (1) ButtonHeads 2) SocketHeads 3) Countersunk 4) NormalHeads");
            StockItem.BoltType BoltT = StockItem.BoltType.ButtonHeads;
            switch (Int_Verify(3))
            {
                case 1:
                    break;
                case 2:
                    BoltT = StockItem.BoltType.SocketHeads;
                    break;
                case 3:
                    BoltT = StockItem.BoltType.CountersunkHead;
                    break;
                case 4:
                    BoltT = StockItem.BoltType.NormalHeads;
                    break;
                default:
                    Console.WriteLine("Chose an incorrect number its defaulted to Checked Out");
                    break;
            }
            
            Console.Write("Please enter stock amount: (1) High 2) Low) 3) None");
            StockItem.BoltAmount BoltA = StockItem.BoltAmount.High;
            switch (Int_Verify(3))
            {
                case 1:
                    break;
                case 2:
                    BoltA = StockItem.BoltAmount.Low;
                    break;
                case 3:
                    BoltA = StockItem.BoltAmount.None;
                    break;
                default:
                    Console.WriteLine("Chose an incorrect number its defaulted to Checked Out");
                    break;
            }

            Console.Write("Please enter the location:");
            string LOC = Verifier(false).ToLower();

            Console.Write("Please enter the box name:");
            string BN = Verifier(false).ToLower();

            Console.Write("Please enter the State of this item (1) Checked Out 2)In_Place 3) Missing)");
            StockItem.CheckMiss State = StockItem.CheckMiss.Checked_Out;
            switch (Int_Verify(3))
            {
                case 1:
                    break;
                case 2:
                    State = StockItem.CheckMiss.In_Place;
                    break;
                case 3:
                    State = StockItem.CheckMiss.Missing;
                    break;
                default:
                    Console.WriteLine("Chose an incorrect number its defaulted to Checked Out");
                    break;
            }

            NewEntity(nextROS, Sup, Size,Length,BoltT,BoltA, LOC, BN, State);
            while (true)
            {
                Console.WriteLine("Would you like to enter another item: Y/N");
                string redo = Verifier(false);
                if (redo.ToUpper() == "Y")
                {
                    Additem_StockItem();
                }
                if (redo.ToUpper() == "N")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid response");
                }

            }
        }
        public void NewEntity(int R, string S, string SZ, int L, StockItem.BoltType BT,StockItem.BoltAmount BA, string LOC, string BN, StockItem.CheckMiss C)
        {
            StockItem item = new StockItem(R, S, SZ, L, BT, BA, LOC, BN,C);
            StockList.Add(item);
            string toSave = Environment.NewLine + $"{R},{S},{$"{BT.ToString()}: {SZ}: {L}"},{BA},{LOC},{BN},{C}";
            if (!File.Exists(Path))
            {
                Console.WriteLine("The file cannot be found");
            }
            else
            {
                using (StreamWriter sw = File.AppendText(Path))
                {
                    sw.WriteLine(toSave);
                }
            }
        }

        public void Search()
        {
            System.Collections.Generic.IEnumerable<RickeSmiteConsole.StockItem> queryresult;
            string Query = "";
            Console.WriteLine("what category would you like to search?");
            Console.WriteLine("1) By Manufacturer");
            Console.WriteLine("2) By Part Number");
            Console.WriteLine("3) By Stock Amount");
            Console.WriteLine("4) By Location");
            Console.WriteLine("5) By Box Name");
            Console.WriteLine("6) By State (currently doesnt work)");
            switch (Int_Verify(6))
            {
                case 1:
                    Query = Querier("Manufacturer");
                    queryresult = from x in StockList
                                  where x.Manufacturer.ToString().ToLower() == Query
                                  select x;
                    break;
                case 2:
                    Query = Querier("Part number");
                    queryresult = from x in StockList
                                  where x.ManufacturerID.ToString().ToLower() == Query
                                  select x;
                    break;
                case 3:
                    Query = Querier("Stock Amount");
                    queryresult = from x in StockList
                                  where x.Stock.ToString() == Query
                                  select x;
                    break;
                case 4:
                    Query = Querier("Location");
                    queryresult = from x in StockList
                                  where x.Location.ToString().ToLower() == Query
                                  select x;
                    break;
                case 5:
                    Query = Querier("Box Name");
                    queryresult = from x in StockList
                                  where x.BoxName.ToString().ToLower() == Query
                                  select x;
                    break;
                case 6:
                    Query = Querier("State");
                    queryresult = from x in StockList
                                  where x.Whereis.ToString().ToLower() == Query
                                  select x;
                    break;
                default:
                    Query = Querier("Manufacturer");
                    queryresult = from x in StockList
                                  where x.Manufacturer.ToString().ToLower() == Query
                                  select x;
                    break;
            }
            Console.WriteLine("Here are the results");
            Console.WriteLine("ROSIS  |Manufacturer   |Manufacturer Number   |Stock|Location   |Box Name   |");
            foreach (StockItem item in queryresult)
            {
                Console.WriteLine($"{item.RosID}|{item.Manufacturer}|{item.ManufacturerID}|{item.Stock}|{item.Location}|{item.BoxName}|");
            }
            while (true)
            {
                Console.WriteLine("Do you want to edit any of the existing data? Y/N");
                string redo = Verifier(false);
                if (redo.ToUpper() == "Y")
                {
                    SearchToEdit();
                }
                else
                {
                    if (redo.ToUpper() == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid response hi");
                    }
                }
            }


        }
        public void PrintAll()
        {
            Console.WriteLine("ROSIS  |Manufacturer   |Part Number   |Stock|Location   |Box Name   |");
            foreach(StockItem item in StockList)
            {
                if(item.Item == StockItem.ItemType.Stock)
                    Console.WriteLine($"{item.RosID}|{item.Manufacturer}|{item.ManufacturerID}|{item.Stock}|{item.Location}|{item.BoxName}|");
                else
                    Console.WriteLine($"{item.RosID}|{item.Supplier}|{item.Bolt_Type} {item.Size} {item.Length}|{item.Bolt_Amount}|{item.Location}|{item.BoxName}|");
            }
        }

        // for filtering text
        public string Verifier(bool isInt)
        {
            string verif = Console.ReadLine();
            if (isInt)
            {
                try
                {
                    Int32.Parse(verif);
                }
                catch
                {
                    Console.WriteLine("Please enter a number");
                    verif = Verifier(isInt);
                }
            }
            else
            {
                while (true)
                {
                    if (verif == "")
                    {
                        Console.WriteLine("please enter a word");
                        verif = Console.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }
                

            }
            return verif;
            
        }

        // for ranges of selection
        public int Int_Verify(int max)
        {
            int Chosen = -1;
            while (true)
            {
                try
                {
                    Chosen = Int32.Parse(Console.ReadLine());
                    if (Chosen == 0 || Chosen > max)
                    {
                        Console.WriteLine($"please enter a value in the range 1 - {max}");
                    }
                    else
                        break;
                }
                catch
                {
                    Console.WriteLine("Please enter a number!!!");
                }
            }
            return Chosen;
        }

        public string Querier(string querytype)
        {
            string query = "";
            while (true)
            {
                Console.WriteLine($"Please enter the {querytype} you are looking for: ");
                query = Console.ReadLine();
                if(query == "")
                {
                    Console.WriteLine("Please enter something");
                }
                else
                {
                    break;
                }
            }
            return query.ToLower();
            
        }

        // editting an entry
        private void Edit(int IDLoc)
        {
            bool Cancelled = false;
            StockItem Original;
            Console.WriteLine("Here is the info you can edit");
            if (StockList[IDLoc].Item == StockItem.ItemType.Stock)
            {
                Console.WriteLine($"1) Manufacturer: {StockList[IDLoc].Manufacturer}\n2) Part Number: {StockList[IDLoc].ManufacturerID}\n3) Stock: {StockList[IDLoc].Stock}\n4) Location: {StockList[IDLoc].Location}\n5) Box Name: {StockList[IDLoc].BoxName}\n6) to Cancel");
                Original = new StockItem(StockList[IDLoc].RosID,StockList[IDLoc].Manufacturer,StockList[IDLoc].ManufacturerID,StockList[IDLoc].Stock,StockList[IDLoc].Location,StockList[IDLoc].BoxName,StockList[IDLoc].Whereis);
                switch (Int_Verify(6))
                {
                    case 1:
                        Console.WriteLine("What is the new Manufacturer?");
                        StockList[IDLoc].Manufacturer = Verifier(false);
                        break;
                    case 2:
                        Console.WriteLine("What is the new Part Number?");
                        StockList[IDLoc].ManufacturerID = Verifier(false);
                        break;
                    case 3:
                        Console.WriteLine("What is the new stock amount?");
                        StockList[IDLoc].Stock = Int32.Parse(Verifier(true));
                        break;
                    case 4:
                        Console.WriteLine("What is the new Location?");
                        StockList[IDLoc].Location = Verifier(false);
                        break;
                    case 5:
                        Console.WriteLine("What is the new box name?");
                        StockList[IDLoc].BoxName = Verifier(false);
                        break;
                    case 6:
                        Cancelled = true;
                        break;
                }
            }
            else
            {
                Console.WriteLine($"1) Supplier: {StockList[IDLoc].Supplier}\n2) Bolt Type: {StockList[IDLoc].Bolt_Type}\n3) Size: {StockList[IDLoc].Size}\n4) Length: {StockList[IDLoc].Length}\n5) Stock: {StockList[IDLoc].Bolt_Amount}\n 6) Location: {StockList[IDLoc].Location} 7) Box Name: {StockList[IDLoc].BoxName} 8) To Cancel\n");
                Original = new StockItem(StockList[IDLoc].RosID,StockList[IDLoc].Supplier, StockList[IDLoc].Size, StockList[IDLoc].Length, StockList[IDLoc].Bolt_Type,StockList[IDLoc].Bolt_Amount,StockList[IDLoc].Location,StockList[IDLoc].BoxName,StockList[IDLoc].Whereis);
                switch (Int_Verify(8))
                {
                    case 1:
                        Console.WriteLine("What is the new Supplier?");
                        StockList[IDLoc].Supplier = Verifier(false);
                        break;
                    case 2:
                        Console.WriteLine("What is the new Bolt Type?");
                        
                        break;
                    case 3:
                        Console.WriteLine("What is the new Size?");
                        StockList[IDLoc].Size = Verifier(false);
                        break;
                    case 4:
                        Console.WriteLine("What is the new Length?");
                        StockList[IDLoc].Length = Int32.Parse(Verifier(true));
                        break;
                    case 5:
                        Console.WriteLine("What is the new Stock?");
                        //StockList[IDLoc].BoxName = Verifier(false);
                        break;
                    case 6:
                        Console.WriteLine("What is the new Location?");
                        StockList[IDLoc].Location = Verifier(false);
                        break;
                    case 7:
                        Console.WriteLine("What is the new BoxName?");
                        StockList[IDLoc].BoxName = Verifier(false);
                        break;
                    case 8:
                        Cancelled = true;
                        break;

                }
            }
            if (!Cancelled)
            {
                Resave(IDLoc, Original);
            }

        }
        private void SearchToEdit()
        {
            Console.WriteLine("please enter the ROSID of the inventory to be edited");
            Edit(Int32.Parse(Verifier(true)));

        }

        private void Resave(int IDLoc, StockItem Original)
        {
            string toSave = "",
                toReplace = "";
            if (Original.Item.ToString() == "Stock")
            {
                Console.WriteLine("hi");
                toSave = $"{StockList[IDLoc].RosID},{StockList[IDLoc].Manufacturer},{StockList[IDLoc].ManufacturerID},{StockList[IDLoc].Stock},{StockList[IDLoc].Location},{StockList[IDLoc].BoxName},{StockList[IDLoc].Whereis.ToString()}";
                toReplace = $"{Original.RosID},{Original.Manufacturer},{Original.ManufacturerID},{Original.Stock},{Original.Location},{Original.BoxName},{Original.Whereis.ToString()}";
            }
            else
            {
                toSave = $"{StockList[IDLoc].RosID},{StockList[IDLoc].Supplier},{$"{StockList[IDLoc].Bolt_Type.ToString()}: {StockList[IDLoc].Size}: {StockList[IDLoc].Length}"},{StockList[IDLoc].Bolt_Amount},{StockList[IDLoc].Location},{StockList[IDLoc].BoxName},{StockList[IDLoc].Whereis.ToString()}";
                toReplace = $"{Original.RosID},{Original.Supplier},{$"{Original.Bolt_Type.ToString()}: {Original.Size}: {Original.Length}"},{Original.Bolt_Amount},{Original.Location},{Original.BoxName},{Original.Whereis.ToString()}";
            }
            File.WriteAllText(Path, File.ReadAllText(Path).Replace(toReplace, toSave));
        }
    }
}
