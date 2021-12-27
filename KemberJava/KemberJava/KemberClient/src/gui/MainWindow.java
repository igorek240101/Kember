package gui;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.*;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.util.*;
import java.net.http.*;
import java.net.*;


public class MainWindow {

    static  Vector<String> dlls = new Vector<>();
    static  Vector<String> paths = new Vector<>();
    static HashMap<String, IMetric> dictionary = new HashMap<String, IMetric>();
    static String now;
    public static String key;
    public static JFrame frame;

    public static void main(String[] args) {
        frame = new JFrame("Kember");
        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);

        frame.setLayout(new BorderLayout());


        JPanel mainPanel = new JPanel();
        mainPanel.setBackground(Color.DARK_GRAY);
        mainPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
        JButton addButton = new JButton("ДОБАВИТЬ");
        addButton.setBackground(Color.DARK_GRAY);
        addButton.setForeground(Color.WHITE);
        JButton invButton = new JButton("ЗАПУСТИТЬ");
        invButton.setEnabled(false);
        invButton.setBackground(Color.DARK_GRAY);
        invButton.setForeground(Color.WHITE);
        JButton delButton = new JButton("УДАЛИТЬ");
        delButton.setBackground(Color.DARK_GRAY);
        delButton.setForeground(Color.WHITE);
        //JButton loadButton = new JButton("ЗАГРУЗИТЬ");
        //loadButton.setBackground(Color.BLACK);
        //loadButton.setForeground(Color.WHITE);
        //JCheckBox saveButton = new JCheckBox("СОХРАНЯТЬ ПРИ РАСЧЕТАХ");
        //saveButton.setBackground(Color.DARK_GRAY);
        //saveButton.setForeground(Color.WHITE);
        mainPanel.add(addButton);
        mainPanel.add(invButton);
        mainPanel.add(delButton);
        //mainPanel.add(loadButton);
        //mainPanel.add(saveButton);
        frame.add(mainPanel, BorderLayout.NORTH);

        JPanel metricPanel = new JPanel();
        metricPanel.setBackground(Color.DARK_GRAY);
        metricPanel.setLayout(new BoxLayout(metricPanel, BoxLayout.Y_AXIS));
        JButton wmcButton = new JButton("WMC");
        wmcButton.setBackground(Color.DARK_GRAY);
        wmcButton.setForeground(Color.WHITE);
        metricPanel.add(wmcButton);
        JButton wmc2Button = new JButton("WMC2");
        wmc2Button.setBackground(Color.DARK_GRAY);
        wmc2Button.setForeground(Color.WHITE);
        metricPanel.add(wmc2Button);
        JButton anamButton = new JButton("ANAM");
        anamButton.setBackground(Color.DARK_GRAY);
        anamButton.setForeground(Color.WHITE);
        metricPanel.add(anamButton);
        JButton ditButton = new JButton("DIT");
        ditButton.setBackground(Color.DARK_GRAY);
        ditButton.setForeground(Color.WHITE);
        metricPanel.add(ditButton);
        JButton nocButton = new JButton("NOC");
        nocButton.setBackground(Color.DARK_GRAY);
        nocButton.setForeground(Color.WHITE);
        metricPanel.add(nocButton);
        JButton bitButton = new JButton("BIH");
        bitButton.setBackground(Color.DARK_GRAY);
        bitButton.setForeground(Color.WHITE);
        metricPanel.add(bitButton);
        frame.add(metricPanel, BorderLayout.WEST);



        JList<String> list = new JList<String>();
        list.setBackground(Color.DARK_GRAY);
        list.setForeground(Color.WHITE);
        list.setFixedCellWidth(100);
        frame.add(list, BorderLayout.EAST);

        dictionary.put("WMC", new WMC());
        dictionary.put("WMC2", new WMC2());
        dictionary.put("ANAM", new ANAM());
        dictionary.put("DIT", new DIT());
        dictionary.put("NOC", new NOC());
        dictionary.put("BIH", new BIH());

        ActionListener addListener = new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                FileDialog fileDialog = new FileDialog(frame, "Выбирите dll", FileDialog.LOAD);
                fileDialog.setFilenameFilter((dir, name) -> name.substring(name.lastIndexOf('.')) == "dll");
                fileDialog.setMultipleMode(true);
                fileDialog.show();
                File[] files = fileDialog.getFiles();
                for(int i = 0; i < files.length; i++){
                    dlls.add(files[i].getName());
                    paths.add(files[i].getPath());
                }
                list.setListData(dlls);
            }
        };

        ActionListener delListener = new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                int[] index = list.getSelectedIndices();
                int max = Arrays.stream(index).max().getAsInt();
                while (max > -1){
                    dlls.removeElementAt(max);
                    paths.removeElementAt(max);
                    for (int i = 0; i < index.length; i++){
                        if(index[i] == max){
                            index[i] = -1;
                            break;
                        }
                    }
                    max = Arrays.stream(index).max().getAsInt();
                }
                list.setListData(dlls);
            }
        };

        ActionListener invListener = new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try
                {
                    if (paths.size() > 0)
                    {
                        String res = "";
                        for (int i = 0; i < paths.size(); i++) {

                            String path = "Invoke";
                            String s = "";
                            /*
                            if(saveButton.isSelected()){
                                s += key  + "\r\n";;
                                path = "Saved" + path;
                            }

                             */
                            s += System.getProperty("user.name") + "\r\n";
                            s += dictionary.get(now).Invoke()  + "\r\n";
                            s += now  + "\r\n";
                            s += paths.get(i)+ "\r\n";

                            FileInputStream reader = new FileInputStream(paths.get(i));
                            byte[] array = reader.readAllBytes();
                            reader.close();
                            for(int j = 0; j < array.length; j++){
                                if(j + 1 != array.length)
                                    s += array[j] + "\r\n";
                                else
                                    s += array[j];
                            }

                            HttpClient client = HttpClient.newHttpClient();
                            HttpRequest request = HttpRequest.newBuilder()
                                    .uri(URI.create("http://localhost:8080/Kember_war/Invoke"))
                                    .header("Content-Type", "application/text")
                                    .POST(HttpRequest.BodyPublishers.ofString(s))
                                    .build();

                            HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString(Charset.forName("UTF-8")));


                            String[] body = response.body().split(" ");
                            for (int k = 0; k < body.length; k++){
                                int integer = Integer.parseInt(body[k]);
                                if(integer == 0) res += '\0';
                                else  res += (char)Integer.parseInt(body[k]);
                            }
                            if(i + 1 != paths.stream().count())
                                res += (char)(1);


                        }

                        dictionary.get(now).SetResult(res);
                    }
                }
                catch (Exception ex) {  }
            }
        };

        ActionListener metricListener = new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                JButton b = (JButton)e.getSource();
                if(now != null){
                    frame.remove((Component)dictionary.get(now));
                }
                now = b.getText();
                frame.add((Panel) dictionary.get(now), BorderLayout.CENTER);
                invButton.setEnabled(true);
                frame.setSize(frame.getWidth()+1, frame.getHeight());
                frame.setSize(frame.getWidth()-1, frame.getHeight());
            }
        };

        addButton.addActionListener(addListener);
        delButton.addActionListener(delListener);
        invButton.addActionListener(invListener);

        wmcButton.addActionListener(metricListener);
        wmc2Button.addActionListener(metricListener);
        anamButton.addActionListener(metricListener);
        ditButton.addActionListener(metricListener);
        nocButton.addActionListener(metricListener);
        bitButton.addActionListener(metricListener);


        frame.setSize(600, 500);
        frame.setLocationRelativeTo(null);
        frame.setVisible(true);
    }
}
