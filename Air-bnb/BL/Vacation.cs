namespace Air_bnb.BL
{
    public class Vacation
    {
        string id, userId, flatId;
        DateTime startDate, endDate;

        static List<Vacation> vacationsList = new();

        public Vacation(string id, string userId, string flatId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            UserId = userId;
            FlatId = flatId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Vacation()
        {
        }

        public string Id { get => id; set => id = value; }
        public string UserId { get => userId; set => userId = value; }
        public string FlatId { get => flatId; set => flatId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        public bool SameDatesAndFlat(Vacation vac1, Vacation vac2)
        {
            return !(vac1.EndDate <= vac2.StartDate || vac2.EndDate <= vac1.StartDate) && vac1.FlatId == vac2.FlatId;
        }

        public bool Insert()
        {

            if (vacationsList.TrueForAll(vac => vac.Id != this.Id && !SameDatesAndFlat(vac,this)))
            {
                vacationsList.Add(this);
                return true;
            }
            return false;
        }

        static public List<Vacation> Read()
        {
            return vacationsList;
        }
        
        static public Vacation? Read(string id)
        {
            return vacationsList.Find(v => v.Id == id);
        }

        static bool BetweenDates(DateTime start, DateTime end, DateTime check)
        {
            return start <= check && check <= end;
        }

        static public List<Vacation> GetByDates(DateTime startDate, DateTime endDate)
        {
            return vacationsList.FindAll(vac => BetweenDates(startDate, endDate,vac.startDate)|| BetweenDates(startDate, endDate, vac.EndDate));
        }
    }

}
