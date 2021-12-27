package servlets;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.*;
import java.lang.Process;

public class SavedInvokeServlet extends  HttpServlet {

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        try (PrintWriter writer = resp.getWriter()) {
            Process process = new ProcessBuilder("D:\\Kember\\KemberBackend.exe").redirectErrorStream(true).start(); //Runtime.getRuntime().exec("D:\\Kember\\KemberBackend.exe");
            OutputStream stdin = process.getOutputStream();
            InputStream stdout = process.getInputStream();
            BufferedReader input = new BufferedReader(new InputStreamReader(stdout));
            BufferedWriter output = new BufferedWriter(new OutputStreamWriter(stdin));
            output.write("Login\r\n");
            String name = req.getReader().readLine();
            output.write(name + "\r\n");
            output.flush();
            if (input.readLine().equals("True")) {
                String key = req.getReader().readLine(), fir = req.getReader().readLine(), sec = req.getReader().readLine(), th = "";
                while (req.getReader().ready()) {
                    th += req.getReader().readLine() + "\r\n";
                }
                File file = new File("D:\\Kember\\Data\\" + name + ".dll");
                file.createNewFile();
                FileWriter fileWriter = new FileWriter(file);
                fileWriter.write(th);
                fileWriter.flush();
                fileWriter.close();
                output.write("Invoke\r\n");
                output.write("D:\\Kember\\Data\\" + name + ".dll" + "\r\n");
                output.write(fir + "\r\n");
                output.write(sec + "\r\n");
                output.flush();
                String s = input.readLine();
                file.delete();
                output.write("Save\r\n");
                output.write(key);
                output.flush();
                output.close();
                output.flush();
                input.readLine();
                process.destroy();
                writer.println(s);
            }
        }
    }

}
