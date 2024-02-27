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

    //login user
    public bool Login(User loginUser)
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

        try
        {
            object exist = cmd.ExecuteScalar(); // execute the command
            return exist==null ? false : true;
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

    // Create the SqlCommand for insert user
    private SqlCommand CreateUserLoginCommandWithSP(String spLoginUser, SqlConnection con, User loginUser)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spLoginUser;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@email", loginUser.Email);
        cmd.Parameters.AddWithValue("@password", loginUser.Password);

        return cmd;
    }

}
