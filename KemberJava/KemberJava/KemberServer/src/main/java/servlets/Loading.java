package servlets;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.*;
import java.lang.Process;

public class Loading extends HttpServlet {

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        try (PrintWriter writer = resp.getWriter())
        {
            Process process = new ProcessBuilder("D:\\Kember\\KemberBackend.exe").redirectErrorStream(true).start(); //Runtime.getRuntime().exec("D:\\Kember\\KemberBackend.exe");
            OutputStream stdin = process.getOutputStream ();
            InputStream stdout = process.getInputStream ();
            BufferedReader input = new BufferedReader (new InputStreamReader(stdout));
            BufferedWriter output = new BufferedWriter(new OutputStreamWriter(stdin));
            output.write("Login\r\n");
            output.write(req.getReader().readLine() + "\r\n");
            output.flush();
            if(input.readLine().equals("True")) {
                output.write("Loading\r\n");
                output.write(req.getReader().readLine() + "\r\n");
                output.flush();
                output.close();
                String s = input.readLine();
                process.destroy();
                writer.println(s);
            }
        }
    }

}
