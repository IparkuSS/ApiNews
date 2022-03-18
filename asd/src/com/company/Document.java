package com.company;
import java.util.Scanner;
public abstract class Document implements Comparable<Document>
{
    protected String Name;
    protected String SurName;
    protected String DateIssue;

    protected void  Input()
    {
        Scanner in = new Scanner(System.in);
        System.out.print("Input Name: ");
        Name = in.next();

        System.out.print("Input SurName: ");
        SurName = in.next();

        System.out.print("Input DateIssue: ");
        DateIssue = in.next();

    }
    public enum MyEnumForSort {
        Name,
        SurName,
        DateIssue;
    }

    protected void  Output()
    {
        System.out.println("Name: " + Name + " ");
        System.out.println("SurName: " + SurName + " ");
        System.out.println("DateIssue: " + DateIssue + " ");



    }

    public void setName(String name) {
        Name = name;
    }

    public void setSurName(String surName) {
        SurName = surName;
    }

    public String getSurName() {
        return SurName;
    }

    public String getName() {
        return Name;
    }

    public String getDateIssue() {
        return DateIssue;
    }

    public void setDateIssue(String dateIssue) {
        DateIssue = dateIssue;
    }
    public int compareTo(Document member) {
        if (this.Name.equals(member.getName())) {
            return 0;
        } else if (!(this.Name.equals(member.getName()))) {
            return 1;
        } else {
            return -1;
        }
    }
}
