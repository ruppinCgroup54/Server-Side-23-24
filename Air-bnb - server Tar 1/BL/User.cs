namespace Air_bnb.BL
{
    public class User
    {
        string firstName, familyName, email, password;

        static List<User> usersList = new();

        public User(string firstName, string familyName, string email, string password)
        {
            this.FirstName = firstName;
            this.FamilyName = familyName;
            this.Email = email;
            this.Password = password;
        }
        public string FirstName { get => firstName; set => firstName = value; }
        public string FamilyName { get => familyName; set => familyName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.Insert(this);
        }

        public bool Login(string email, string password)
        {
            DBservices dbs = new DBservices();
            return dbs.Login(email, password);
        }

    }
}
