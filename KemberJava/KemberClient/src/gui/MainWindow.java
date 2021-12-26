package gui;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.*;
import java.util.*;
import java.net.http.*;
import java.net.*;


public class MainWindow {

    static  Vector<String> dlls = new Vector<>();
    static  Vector<String> paths = new Vector<>();
    static HashMap<String, IMetric> dictionary = new HashMap<String, IMetric>();
    static String now;
    static String key;

    public static void main(String[] args) {
        JFrame frame = new JFrame("Kember");
        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);

        frame.setLayout(new BorderLayout());


        JPanel mainPanel = new JPanel();
        mainPanel.setBackground(Color.BLACK);
        mainPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
        JButton addButton = new JButton("ДОБАВИТЬ");
        addButton.setBackground(Color.BLACK);
        addButton.setForeground(Color.WHITE);
        JButton invButton = new JButton("ЗАПУСТИТЬ");
        invButton.setEnabled(false);
        invButton.setBackground(Color.BLACK);
        invButton.setForeground(Color.WHITE);
        JButton delButton = new JButton("УДАЛИТЬ");
        delButton.setBackground(Color.BLACK);
        delButton.setForeground(Color.WHITE);
        JButton loadButton = new JButton("ЗАГРУЗИТЬ");
        loadButton.setBackground(Color.BLACK);
        loadButton.setForeground(Color.WHITE);
        JCheckBox saveButton = new JCheckBox("СОХРАНЯТЬ ПРИ РАСЧЕТАХ");
        saveButton.setBackground(Color.BLACK);
        saveButton.setForeground(Color.WHITE);
        mainPanel.add(addButton);
        mainPanel.add(invButton);
        mainPanel.add(delButton);
        mainPanel.add(loadButton);
        mainPanel.add(saveButton);
        frame.add(mainPanel, BorderLayout.NORTH);

        JPanel metricPanel = new JPanel();
        metricPanel.setBackground(Color.BLUE);
        metricPanel.setLayout(new BoxLayout(metricPanel, BoxLayout.Y_AXIS));
        JButton wmcButton = new JButton("WMC");
        metricPanel.add(wmcButton);
        JButton wmc2Button = new JButton("WMC2");
        metricPanel.add(wmc2Button);
        JButton anamButton = new JButton("ANAM");
        metricPanel.add(anamButton);
        JButton ditButton = new JButton("DIT");
        metricPanel.add(ditButton);
        JButton nocButton = new JButton("NOC");
        metricPanel.add(nocButton);
        JButton bitButton = new JButton("BIT");
        metricPanel.add(bitButton);
        frame.add(metricPanel, BorderLayout.WEST);

        JList<String> list = new JList<String>();
        list.setBackground(Color.RED);
        list.setFixedCellWidth(100);
        frame.add(list, BorderLayout.EAST);

        dictionary.put("WMC", new WMC());

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
                    if (paths.stream().count() > 0)
                    {
                        String res = "";
                        for (int i = 0; i < paths.stream().count(); i++) {


                            String s = "";
                            if(saveButton.isSelected()){
                                s += key  + "\r\n";;
                            }
                            s += System.getProperty("user.name") + "\r\n";
                            s += dictionary.get(now).Invoke()  + "\r\n";;
                            s += now  + "\r\n";;

                            BufferedReader reader = new BufferedReader( new FileReader(paths.get(i)));
                            while (reader.ready()){
                                s += reader.readLine() + "\r\n";
                            }
                            s.trim();

                            HttpClient client = HttpClient.newHttpClient();
                            HttpRequest request = HttpRequest.newBuilder()
                                    .uri(URI.create("http://localhost:8080/Kember_war/Invoke"))
                                    .header("Content-Type", "application/text")
                                    .POST(HttpRequest.BodyPublishers.ofString(s))
                                    .build();

                            HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());

                            if(i + 1 == paths.stream().count())
                                res += response.body() + (char)(1);
                            else
                                res += response.body();


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


        frame.setSize(600, 500);
        frame.setLocationRelativeTo(null);
        frame.setVisible(true);
    }
}
