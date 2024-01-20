namespace Air_bnb.BL
{
    public class Vacation
    {
        string id, userId, flatId;
        DateTime startDate, endDate;

        static List<Vacation> vacationsList = new List<Vacation>();

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
    
        public bool CheckDates(DateTime dStart1, DateTime dEnd1, DateTime dStart2, DateTime dEnd2)
        {
            return dEnd1 < dStart2 || dEnd2 < dStart1;
        }

        public bool Insert()
        {
            
            if (vacationsList.TrueForAll(vac => vac.Id != this.Id && !(!CheckDates(vac.StartDate, vac.EndDate, this.startDate, this.endDate) && vac.FlatId==this.FlatId)))
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

        static public List<Vacation> GetByDates(DateTime startDate, DateTime endDate)
        {
            return vacationsList.FindAll(vac => vac.endDate <= endDate && startDate <= vac.startDate);
        }
    }

}
