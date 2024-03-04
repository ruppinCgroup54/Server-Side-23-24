namespace Air_bnb.BL
{
    public class User
    {
        string firstName, familyName, email, password;
        bool isActive;
        public User(string firstName, string familyName, string email, string password, bool isActive)
        {
            this.FirstName = firstName;
            this.FamilyName = familyName;
            this.Email = email;
            this.Password = password;
            this.IsActive = isActive;
        }

        public User()
        {
        }
        public string FirstName { get => firstName; set => firstName = value; }
        public string FamilyName { get => familyName; set => familyName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.Insert(this);
        }

        public User Login()
        {
            DBservices dbs = new DBservices();
            User u = dbs.Login(this);
            if (u.Email==null)
            {
                throw new KeyNotFoundException();
            }
            if (!u.IsActive)
            {
                throw new AccessViolationException();
            }
            return u;
        }

        public static List<User> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadUsers();
        }

        public List<User> UpdateUser()
        {
            DBservices dbs = new DBservices();
            dbs.UpdateUser(this);
            
            return dbs.ReadUsers();
        }
        public User UpdateUser(string email)
        {
            DBservices dbs = new DBservices();
            
            return dbs.UpdateUser(this);
        }

    }
}
