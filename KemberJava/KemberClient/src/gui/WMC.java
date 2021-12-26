package gui;

import javax.swing.*;
import javax.swing.tree.DefaultMutableTreeNode;
import javax.swing.tree.DefaultTreeCellRenderer;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.io.File;
import java.io.FilenameFilter;
import java.util.*;

public class WMC extends Panel implements IMetric {

    final int min = 3, max = 12;

    JCheckBox checkDelegate = new JCheckBox("Учитывать делегаты как типы данных");
    JCheckBox checkEnum = new JCheckBox("Учитывать структуры");
    JCheckBox checkInterface = new JCheckBox("Учитывать вложенные типы");
    JCheckBox checkNested = new JCheckBox("Учитывать перечисления");
    JCheckBox checkStatic = new JCheckBox("Учитывать интерфейсы");
    JCheckBox checkStruct = new JCheckBox("Учитывать все видимости (иначе только публичные)"  );
    JCheckBox checkPrivate = new JCheckBox("Учитывать статические методы");

    JTree tree;

    public WMC(){
        setBackground(Color.GREEN);
        setLayout(new BorderLayout());
        JPanel checkPanel = new JPanel();
        checkPanel.setLayout(new BoxLayout(checkPanel,BoxLayout.Y_AXIS));
        checkPanel.add(checkDelegate);
        checkPanel.add(checkEnum);
        checkPanel.add(checkInterface);
        checkPanel.add(checkNested);
        checkPanel.add(checkStatic);
        checkPanel.add(checkStruct);
        checkPanel.add(checkPrivate);
        add(checkPanel, BorderLayout.WEST);
    }

    @Override
    public String Invoke() {
        int flags = 0;
        int shablon = 2;
        if (checkDelegate.isSelected()) flags = flags | shablon;
        shablon <<= 2;
        if (checkStruct.isSelected()) flags = flags | shablon;
        shablon <<= 1;
        if (checkNested.isSelected()) flags = flags | shablon;
        shablon <<= 1;
        if (checkEnum.isSelected()) flags = flags | shablon;
        shablon <<= 1;
        if (checkInterface.isSelected()) flags = flags | shablon;
        shablon <<= 1;
        if (checkPrivate.isSelected()) flags = flags | shablon;
        shablon <<= 1;
        if (checkStatic.isSelected()) flags = flags | shablon;
        return Integer.toString(flags);
    }

    @Override
    public void SetResult(String arg) {
        if(tree != null) remove(tree);
        tree = null;
        String[] strs = arg.split(Character.toString((char)1));
        DefaultMutableTreeNode metric = new DefaultMutableTreeNode();
        metric.setUserObject(new JLabel(getClass().getName()));
        tree = new JTree(metric);
        for (int i = 0; i < strs.length; i++) {
            DefaultMutableTreeNode assembly = new DefaultMutableTreeNode();
            String[] ass = strs[i].split(Character.toString('\0'));
            assembly.setUserObject(new JLabel(ass[0]));
            metric.add(assembly);
            for(int j = 1; j < ass.length; j++) {
                DefaultMutableTreeNode type = new DefaultMutableTreeNode();
                int num = Integer.parseInt((ass[j].split(Character.toString('-'))[1]));
                JLabel label = new JLabel(ass[j]);
                if (num >= min && num <= max) {
                    label.setForeground(Color.BLACK);
                }
                else {
                    label.setForeground(Color.RED);
                }
                type.setUserObject(label);
                assembly.add(type);
            }
        }
    }


}
