package servlets;

import org.sqlite.SQLiteConfig;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.*;
import java.lang.Process;
import java.nio.charset.StandardCharsets;
import java.util.Base64;
import java.util.Vector;

public class InvokeServlet extends  HttpServlet {



    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        try (BufferedWriter writer =  new BufferedWriter(resp.getWriter()))
        {
            Process process = new ProcessBuilder("D:\\Kember\\KemberBackend.exe").redirectErrorStream(true).start(); //Runtime.getRuntime().exec("D:\\Kember\\KemberBackend.exe");
            OutputStream stdin = process.getOutputStream ();
            InputStream stdout = process.getInputStream ();
            BufferedReader input = new BufferedReader (new InputStreamReader(stdout, "CP866"));
            BufferedWriter output = new BufferedWriter(new OutputStreamWriter(stdin));
            output.write("Login\r\n");
            String name = req.getReader().readLine();
            output.write(name + "\r\n");
            output.flush();
            if(input.readLine().equals("True")) {
                String fir = req.getReader().readLine(), sec = req.getReader().readLine(), th = req.getReader().readLine();
                Vector<Integer> bytesVector = new Vector<>();
                while (req.getReader().ready()){
                    bytesVector.add(Integer.parseInt(req.getReader().readLine()));
                }
                File file = new File("D:\\Kember\\Data\\" + name + ".dll");
                file.createNewFile();
                FileOutputStream fileWriter = new FileOutputStream(file);
                for (int i = 0; i < bytesVector.size(); i++) fileWriter.write(bytesVector.get(i));
                fileWriter.flush();
                fileWriter.close();
                output.write("Invoke\r\n");
                output.write(th + "\r\n");
                output.write(fir + "\r\n");
                output.write(sec + "\r\n");
                output.flush();
                String s = input.readLine();
                System.out.println(s.split("\0").length);
                output.close();
                file.delete();
                process.destroy();
                for (int i = 0; i < s.length(); i++){
                    int ch = s.toCharArray()[i];
                    writer.write(ch+" ");
                }
                writer.flush();
                writer.close();
            }
        }
    }

}
