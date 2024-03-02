namespace Air_bnb.BL
{
    public class Vacation
    {
        int id;
        string userEmail, flatId;
        DateTime startDate, endDate;

        //static List<Vacation> vacationsList = new()
        //{
        //new Vacation("a","a","1",new DateTime(2023,01,10),new DateTime(2023,01,15)),
        //new Vacation("b","b","1",new DateTime(2023,01,05),new DateTime(2023,01,08)),
        //new Vacation("c","c","2",new DateTime(2023,01,10),new DateTime(2023,01,15)),
        //new Vacation("d","d","3",new DateTime(2023,01,13),new DateTime(2023,01,18)),
        //};

        public Vacation(int id, string userEmail, string flatId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            UserEmail = userEmail;
            FlatId = flatId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Vacation()
        {
        }

        public int Id { get => id; set => id = value; }
        public string UserEmail { get => userEmail; set => userEmail = value; }
        public string FlatId { get => flatId; set => flatId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        public bool SameDatesAndFlat(Vacation vac1, Vacation vac2)
        {
            return !(vac1.EndDate <= vac2.StartDate || vac2.EndDate <= vac1.StartDate) && vac1.FlatId == vac2.FlatId;
        }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            List<Vacation> vacationsList = dbs.ReadVacations();
            if (vacationsList.TrueForAll(vac => vac.Id != this.Id && !SameDatesAndFlat(vac, this)))
            {
                return dbs.InsertVacation(this);
            }
            return 0;
        }

        static public List<Vacation> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadVacations();
        }

        static public Vacation Read(string id)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadVacationById(id);
        }


        static bool BetweenDates(DateTime start, DateTime end, DateTime check)
        {
            return start <= check && check <= end;
        }

        static public List<Vacation> GetByDates(DateTime startDate, DateTime endDate)
        {
            //return vacationsList.FindAll(vac => BetweenDates(startDate, endDate, vac.startDate) || BetweenDates(startDate, endDate, vac.EndDate));
            DBservices dbs = new DBservices();
            return dbs.ReadVacationByIdDates(startDate, endDate);
        }
    }

}
