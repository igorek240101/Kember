package gui;

import javax.swing.*;
import javax.swing.text.PasswordView;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.*;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.util.*;
import java.net.http.*;
import java.net.*;

public class RegForm extends JFrame{

    public RegForm(){
        setLayout(new FlowLayout());
        Label label = new Label("Введите ключ");
        TextField field = new TextField();
        Label danger = new Label("ключ должен состоять из 4 цифр");
        danger.setForeground(Color.RED);
        danger.setVisible(false);
        Button button = new Button("ОТПРАВИТЬ");

        button.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                String s = System.getProperty("user.name") + "\r\n";
                s += field.getText() + "\r\n";
                HttpClient client = HttpClient.newHttpClient();
                HttpRequest request = HttpRequest.newBuilder()
                        .uri(URI.create("http://localhost:8080/Kember_war/Registration"))
                        .header("Content-Type", "application/text")
                        .POST(HttpRequest.BodyPublishers.ofString(s))
                        .build();

                try {
                    HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString(Charset.forName("UTF-8")));
                    if(response.body() == "False"){
                        danger.setVisible(true);
                    }
                    else {
                        MainWindow.key = field.getText();
                        setVisible(false);
                        MainWindow.frame.setVisible(true);
                    }
                } catch (IOException ex) {
                    ex.printStackTrace();
                } catch (InterruptedException ex) {
                    ex.printStackTrace();
                }
            }
        });


        add(label);
        add(field);
        add(danger);
        add(button);
        setSize(500, 500);
    }


}
