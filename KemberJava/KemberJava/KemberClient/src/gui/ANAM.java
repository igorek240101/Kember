package gui;

import javax.swing.*;
import javax.swing.tree.DefaultMutableTreeNode;
import java.awt.*;

public class ANAM extends Panel implements IMetric{

    final int min = 1, max = 4;

    JCheckBox checkDelegate = new JCheckBox("Учитывать делегаты как типы данных");
    JCheckBox checkEnum = new JCheckBox("Учитывать структуры");
    JCheckBox checkInterface = new JCheckBox("Учитывать вложенные типы");
    JCheckBox checkNested = new JCheckBox("Учитывать перечисления");
    JCheckBox checkStatic = new JCheckBox("Учитывать интерфейсы");
    JCheckBox checkStruct = new JCheckBox("Учитывать все видимости (иначе только публичные)"  );
    JCheckBox checkPrivate = new JCheckBox("Учитывать статические методы");

    JTree tree;
    JScrollPane scroll = new JScrollPane();

    public ANAM(){
        setBackground(Color.DARK_GRAY);
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
        scroll.setBackground(Color.DARK_GRAY);
        scroll.createHorizontalScrollBar();
        scroll.createVerticalScrollBar();
        scroll.setSize(500,500);
        add(scroll, BorderLayout.CENTER);
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
        if(tree != null) scroll.remove(tree);
        tree = null;
        String[] strs = arg.split(Character.toString((char)1));
        DefaultMutableTreeNode metric = new DefaultMutableTreeNode();
        metric.setUserObject(getClass().getName());
        tree = new JTree(metric);
        for (int i = 0; i < strs.length; i++) {
            DefaultMutableTreeNode assembly = new DefaultMutableTreeNode();
            String[] ass = strs[i].split(Character.toString('\0'));
            assembly.setUserObject(ass[0]);
            metric.add(assembly);
            for(int j = 1; j < ass.length; j++) {
                DefaultMutableTreeNode type = new DefaultMutableTreeNode();
                type.setUserObject(ass[j]);
                assembly.add(type);
            }
        }
        scroll.setViewportView(tree);
        scroll.createVerticalScrollBar();
        scroll.createHorizontalScrollBar();
    }

}
