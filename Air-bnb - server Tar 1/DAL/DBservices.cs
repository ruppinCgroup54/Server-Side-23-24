using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Air_bnb.BL;


/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }
    
    //----------------------------------//
    //insert new user
    public int Insert(User newUser)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateUserInsertCommandWithSP("Sp_InsertUser", con, newUser);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (SqlException e)
        {
            if (e.Message.Contains("Violation of PRIMARY KEY"))
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
                
            }
            
        }
        return 0;
    }

    // Create the SqlCommand for insert user
    private SqlCommand CreateUserInsertCommandWithSP(String spInsertUser, SqlConnection con, User newUser )
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spInsertUser;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@firstName", newUser.FirstName);
        cmd.Parameters.AddWithValue("@familyName", newUser.FamilyName);
        cmd.Parameters.AddWithValue("@email", newUser.Email);
        cmd.Parameters.AddWithValue("@password", newUser.Password);

        return cmd;
    }

    //update user
    public User UpdateUser(User updateUser)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateUserUpdateCommandWithSP("Sp_UpdateUser", con, updateUser);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(); // execute the command

            User user = new();

            while (dataReader.Read())
            {

                user.FirstName = dataReader["firstName"].ToString();
                user.FamilyName = dataReader["familyName"].ToString();
                user.Email = dataReader["email"].ToString();
                user.Password = dataReader["password"].ToString();
                user.IsActive = Convert.ToBoolean(dataReader["isActive"]);
            }

            return user;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();

            }

        }
    }

    // Create the SqlCommand for update user
    private SqlCommand CreateUserUpdateCommandWithSP(String spUpdateUser, SqlConnection con, User updateUser)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spUpdateUser;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@firstName", updateUser.FirstName);
        cmd.Parameters.AddWithValue("@familyName", updateUser.FamilyName);
        cmd.Parameters.AddWithValue("@email", updateUser.Email);
        cmd.Parameters.AddWithValue("@isActive", updateUser.IsActive);

        return cmd;
    }


    //----------------------------------//
    //get all users
    public List<User> ReadUsers()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateUsersCommandWithSP("SP_ReadUsers", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(); // execute the command

            List<User> usersList = new();

            while (dataReader.Read())
            {
                User u = new();

                u.FirstName = dataReader["firstName"].ToString();
                u.FamilyName = dataReader["familyName"].ToString();
                u.Email = dataReader["email"].ToString();
                u.Password = dataReader["password"].ToString();
                u.IsActive = Convert.ToBoolean(dataReader["isActive"]);
                usersList.Add(u);
            }

            return usersList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();

            }

        }
    }

    // Create the SqlCommand for get all users
    private SqlCommand CreateUsersCommandWithSP(String spInsertUser, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spInsertUser;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }


    //----------------------------------//
    //login user
    public User Login(User loginUser)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateUserLoginCommandWithSP("SP_LoginUser", con, loginUser);             // create the command

        User u = new User();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(); // execute the command

            while (dataReader.Read()) 
            { 
                u.FirstName = dataReader["firstName"].ToString();
                u.FamilyName = dataReader["familyName"].ToString();
                u.Email = dataReader["email"].ToString();
                u.Password = dataReader["password"].ToString();
            }

            return u;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    // Create the SqlCommand for login user
    private SqlCommand CreateUserLoginCommandWithSP(String spLoginUser, SqlConnection con, User loginUser)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spLoginUser;      // can be Select, Insert, Update, Delete 

        //cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@email", loginUser.Email);
        cmd.Parameters.AddWithValue("@password", loginUser.Password);

        return cmd;
    }

    //----------------------------------//
    //insert new flat
    public bool Insert(Flat newFlat)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateFlatInsertCommandWithSP("Sp_InsertFlat", con, newFlat);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected==1 ? true : false;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    // Create the SqlCommand for new flat
    private SqlCommand CreateFlatInsertCommandWithSP(String spInsertFlat, SqlConnection con, Flat newFlat)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spInsertFlat;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@city", newFlat.City);
        cmd.Parameters.AddWithValue("@address", newFlat.Address);
        cmd.Parameters.AddWithValue("@numOfRooms", newFlat.NumberOfRooms);
        cmd.Parameters.AddWithValue("@price", newFlat.Price);

        return cmd;
    }

    //----------------------------------//
    //find all the flats in a city with max price
    public List<Flat> GetMaxPriceInCity(string city, float price)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateFlatsByCityAndMaxPriceCommandWithSP("SP_ReadFlatsCityMaxPrice", con, city, price);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<Flat> flatList = new();

            while (dataReader.Read())
            {
                Flat f = new();
                f.Id = Convert.ToInt32(dataReader["id"]);
                f.City = dataReader["city"].ToString();
                f.Address = dataReader["address"].ToString();
                f.NumberOfRooms = Convert.ToInt32(dataReader["numOfRooms"]);
                f.Price = Convert.ToDouble(dataReader["price"]);

                flatList.Add(f);
            }

            return flatList;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand for flats in a city with max price
    private SqlCommand CreateFlatsByCityAndMaxPriceCommandWithSP(String spFlatsCityMaxPrice, SqlConnection con, string city, float price)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spFlatsCityMaxPrice;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@city", city);
        cmd.Parameters.AddWithValue("@price", price);

        return cmd;
    }

    //----------------------------------//
    //find flat by id
    public Flat ReadFlatById(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateFlatByIdCommandWithSP("SP_ReadFlatsById", con, id);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader();

            Flat f = new();

            while (dataReader.Read())
            {
                f.Id = Convert.ToInt32(dataReader["id"]);
                f.City = dataReader["city"].ToString();
                f.Address = dataReader["address"].ToString();
                f.NumberOfRooms = Convert.ToInt32(dataReader["numOfRooms"]);
                f.Price = Convert.ToDouble(dataReader["price"]);
            }

            return f;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand for flat by id
    private SqlCommand CreateFlatByIdCommandWithSP(String spFlatById, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spFlatById;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@id", id);

        return cmd;
    }

    //----------------------------------//
    //get all flats
    public List<Flat> ReadFlats()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateFlatsCommandWithSP("SP_ReadFlats", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<Flat> flatList = new();

            while (dataReader.Read())
            {
                Flat f = new();
                f.Id = Convert.ToInt32(dataReader["id"]);
                f.City = dataReader["city"].ToString();
                f.Address = dataReader["address"].ToString();
                f.NumberOfRooms = Convert.ToInt32(dataReader["numOfRooms"]);
                f.Price = Convert.ToDouble(dataReader["price"]);

                flatList.Add(f);
            }

            return flatList;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand for all flats
    private SqlCommand CreateFlatsCommandWithSP(String spFlats, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spFlats;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }

    //----------------------------------//
    //get all vacations
    public List<Vacation> ReadVacations()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateVacationsCommandWithSP("SP_ReadVacations", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<Vacation> vacationsList = new();

            while (dataReader.Read())
            {
                Vacation v = new();
                v.FlatId = dataReader["flatId"].ToString();
                v.UserEmail = dataReader["userEmail"].ToString();
                v.StartDate = Convert.ToDateTime(dataReader["startDate"]);
                v.EndDate = Convert.ToDateTime(dataReader["endDate"]);
                v.Id = Convert.ToInt32(dataReader["id"]);

                vacationsList.Add(v);
            }

            return vacationsList;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand for all vacations
    private SqlCommand CreateVacationsCommandWithSP(String spVacations, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spVacations;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }


    //----------------------------------//
    //get vacation by id
    public Vacation ReadVacationById(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateVacationByIdCommandWithSP("SP_ReadVacationById", con, id);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader();

            Vacation v = new();

            while (dataReader.Read())
            {
                v.FlatId = dataReader["flatId"].ToString();
                v.UserEmail = dataReader["userEmail"].ToString();
                v.StartDate = Convert.ToDateTime(dataReader["startDate"]);
                v.EndDate = Convert.ToDateTime(dataReader["endDate"]);
                v.Id = Convert.ToInt32(dataReader["id"]);
            }

            return v;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand for vacation by id
    private SqlCommand CreateVacationByIdCommandWithSP(String spVacationById, SqlConnection con, string id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spVacationById;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@id", id);

        return cmd;
    }

    //----------------------------------//
    //get vacation by dates
    public List<Vacation> ReadVacationByIdDates(DateTime startDate, DateTime endDate)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateVacationByDatesCommandWithSP("SP_ReadVacationByDates", con, startDate, endDate);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<Vacation> vacationsList = new();

            while (dataReader.Read())
            {
                Vacation v = new();
                v.FlatId = dataReader["flatId"].ToString();
                v.UserEmail = dataReader["userEmail"].ToString();
                v.StartDate = Convert.ToDateTime(dataReader["startDate"]);
                v.EndDate = Convert.ToDateTime(dataReader["endDate"]);
                v.Id = Convert.ToInt32(dataReader["id"]);
                vacationsList.Add(v);
            }

            return vacationsList;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand for vacation by dates
    private SqlCommand CreateVacationByDatesCommandWithSP(String spVacationByDates, SqlConnection con, DateTime startDate, DateTime endDate)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spVacationByDates;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@startDate", startDate.Date);
        cmd.Parameters.AddWithValue("@endDate", endDate.Date);

        return cmd;
    }

    //----------------------------------//
    //insert vacation
    public int InsertVacation(Vacation newVacation)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

        cmd = CreateInsertVacationCommandWithSP("SP_InsertVacation", con, newVacation);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery();
            return numEffected;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand for insert vacation
    private SqlCommand CreateInsertVacationCommandWithSP(String spInsertVacation, SqlConnection con, Vacation newVacation)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spInsertVacation;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@flatId", newVacation.FlatId);
        cmd.Parameters.AddWithValue("@userEmail", newVacation.UserEmail);
        cmd.Parameters.AddWithValue("@startDate", newVacation.StartDate.Date);
        cmd.Parameters.AddWithValue("@endDate", newVacation.EndDate.Date);

        return cmd;
    }

}
