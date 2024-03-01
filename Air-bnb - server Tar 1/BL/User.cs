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

        public User()
        {
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

        public User Login()
        {
            DBservices dbs = new DBservices();
            User u = dbs.Login(this);
            return u;
        }

    }
}
