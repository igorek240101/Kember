package servlets;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.sql.SQLException;

import dbcontext.Conn;

public class LoginServlet extends HttpServlet {

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        try (PrintWriter writer = resp.getWriter())
        {
            try
            {
                Conn.conn();
                String input = req.getReader().readLine();
                if(Conn.readDB(input)) {
                    writer.println("True");
                }
                else {
                    writer.println("False");
                }
                Conn.closeDB();
            }
            catch (Exception e){
                writer.println(e.getMessage());
            }
        }
    }
}
