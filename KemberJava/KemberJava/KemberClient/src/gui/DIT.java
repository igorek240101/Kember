package gui;

import javax.swing.*;
import javax.swing.tree.DefaultMutableTreeNode;
import java.awt.*;

public class DIT extends Panel implements IMetric {
    final int min = 3, max = 7;


    JTree tree;
    JScrollPane scroll = new JScrollPane();

    public DIT(){
        setBackground(Color.DARK_GRAY);
        setLayout(new BorderLayout());
        scroll.setBackground(Color.DARK_GRAY);
        scroll.createHorizontalScrollBar();
        scroll.createVerticalScrollBar();
        scroll.setSize(500,500);
        add(scroll, BorderLayout.CENTER);
    }

    @Override
    public String Invoke() {
        return "";
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
