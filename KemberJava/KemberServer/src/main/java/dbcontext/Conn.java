package dbcontext;

import java.sql.*;
import java.util.Properties;

import org.sqlite.JDBC;

public class Conn {

    public static Connection conn;
    public static Statement statmt;
    public static ResultSet resSet;

    public static void conn() throws SQLException, ClassNotFoundException
    {
        try {

        conn = JDBC.createConnection("jdbc:sqlite:D:\\Kember\\LogDB.db", new Properties());
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
        statmt = conn.createStatement();
    }


    public static boolean readDB(String n) throws SQLException
    {
        resSet = statmt.executeQuery("SELECT * FROM Users");

        while(resSet.next())
        {
            String s = resSet.getString("Name");
            if(s.equals(n))
            {
                return true;
            }
        }
        return false;
    }


    public static void closeDB() throws SQLException
    {
        conn.close();
        statmt.close();
        resSet.close();
    }
}
