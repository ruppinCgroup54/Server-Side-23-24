namespace Air_bnb.BL
{
    public class Flat
    {
        string id,city,address;
        double price;
        int numberOfRooms;
        //static List<Flat> FlatList = new List<Flat>()
        //{
        //    new Flat("1","tel aviv","yarkon 8",2000,8),
        //    new Flat("2","tel aviv","yarkon 9",1000,8),
        //    new Flat("3","tel aviv","yarkon 10",200,4),
        //    new Flat("4","tel aviv","yarkon 11",100,3),
        //    new Flat("5","tel aviv","yarkon 8",2000,8),
        //    new Flat("6","tel aviv","yarkon 9",1000,8),
        //    new Flat("7","tel aviv","yarkon 10",200,4),
        //    new Flat("8","tel aviv","yarkon 11",100,3)
        //};

        public Flat(string id, string city, string address, double price, int numberOfRooms)
        {
            Id = id;
            City = city;
            Address = address;
            NumberOfRooms = numberOfRooms;
            Price = price;
        }

        public Flat()
        {
        }

        public string Id { get => id; set => id = value; }
        public string City { get => city; set => city = value; }
        public string Address { get => address; set => address = value; }
        public int NumberOfRooms { get => numberOfRooms; set => numberOfRooms = value; }
        public double Price { get => price; set => price = Discount(value); }


        public bool Insert()
        {
            //if (!FlatList.Exists(flat => flat.id == this.id))
            //{
            //    FlatList.Add(this);
            //    return true;
            //}
            //return false;

            DBservices dbs = new DBservices();
            return dbs.Insert(this);
        }

        static public List<Flat> Read()
        {
            return FlatList;
        }
        
        static public Flat Read(string id)
        {
            return FlatList.Find(item => item.Id == id)?? new Flat() ;
        }

        double Discount(double val)
        {
            return (this.NumberOfRooms > 1 && val > 100) ? val * 0.9 : val;
        }

        static public List<Flat> GetMaxPriceInCity(string city, int price)
        {
            return FlatList.FindAll(flat => flat.City == city && flat.price < price);
        }
    }
}
