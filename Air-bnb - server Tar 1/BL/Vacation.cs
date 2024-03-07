namespace Air_bnb.BL
{
    public class Vacation
    {
        int id, flatId;
        string userEmail;
        DateTime startDate, endDate;

        public Vacation(int id, string userEmail, int flatId, DateTime startDate, DateTime endDate)
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
        public int FlatId { get => flatId; set => flatId = value; }
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
            if (vacationsList.TrueForAll(vac => !SameDatesAndFlat(vac, this)))
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

        static public List<Vacation> Read(string id)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadVacationById(id);
        }


       /* static bool BetweenDates(DateTime start, DateTime end, DateTime check)
        {
            return start <= check && check <= end;
        }*/

        static public List<Vacation> GetByDates(DateTime startDate, DateTime endDate)
        {
            //return vacationsList.FindAll(vac => BetweenDates(startDate, endDate, vac.startDate) || BetweenDates(startDate, endDate, vac.EndDate));
            DBservices dbs = new DBservices();
            return dbs.ReadVacationByIdDates(startDate, endDate);
        }

        static public List<Object> getAveragePerNight(int month)
        {
            DBservices dbs = new DBservices();
            return dbs.getAveragePerNight(month);
        }
    }

}
