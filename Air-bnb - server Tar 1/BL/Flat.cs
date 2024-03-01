namespace Air_bnb.BL
{
    public class Flat
    {
        string city,address;
        double price;
        int id, numberOfRooms;

        public Flat(int id, string city, string address, double price, int numberOfRooms)
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

        public int Id { get => id; set => id = value; }
        public string City { get => city; set => city = value; }
        public string Address { get => address; set => address = value; }
        public int NumberOfRooms { get => numberOfRooms; set => numberOfRooms = value; }
        public double Price { get => price; set => price = Discount(value); }


        public bool Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.Insert(this);
        }

        static public List<Flat> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadFlats();
        }
        
        static public Flat Read(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadFlatById(id);
        }

        double Discount(double val)
        {
            if (this.Id==0)
            {
                return (this.NumberOfRooms > 1 && val > 100) ? val * 0.9 : val;
            }
            return val;
        }

        static public List<Flat> GetMaxPriceInCity(string city, float price)
        {
            DBservices dbs = new DBservices();
            List<Flat> flatList = dbs.GetMaxPriceInCity(city, price);
            return flatList;
        }
    }
}
