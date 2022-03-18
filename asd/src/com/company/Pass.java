package com.company;

import java.util.Scanner;

public class Pass extends Document implements Comparable<Document>
{
    private String NameCompany;
    private String NameSection;
    private String DateToOver;
    public void  Input()
    {
        super.Input();

        Scanner in = new Scanner(System.in);
        System.out.print("Input NameCompany: ");
        NameCompany = in.next();

        System.out.print("Input NameSection: ");
        NameSection = in.next();

        System.out.print("Input DateToOver: ");
        DateToOver = in.next();

    }
    public void  Output()
    {
        super.Output();
        System.out.println("NameCompany: " + NameCompany + " ");

        System.out.println("NameSection: " + NameSection + " ");
        System.out.println("DateToOver: " + DateToOver + " ");

        System.out.println("------------------");

    }
    public String getDateToOver() {
        return DateToOver;
    }

    public String getNameCompany() {
        return NameCompany;
    }

    public String getNameSection() {
        return NameSection;
    }

    public void setDateToOver(String dateToOver) {
        DateToOver = dateToOver;
    }

    public void setNameCompany(String nameCompany) {
        NameCompany = nameCompany;
    }

    public void setNameSection(String nameSection) {
        NameSection = nameSection;
    }

    public int compareTo(Pass member) {
        if (this.NameCompany.equals(member.getNameCompany())) {
            return 0;
        } else if (!(this.NameCompany.equals(member.getNameCompany()))) {
            return 1;
        } else {
            return -1;
        }
    }

}
