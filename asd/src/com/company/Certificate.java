package com.company;

import java.util.Scanner;

public class Certificate extends Document implements Comparable<Document>
{
    private int CertificateNumber;
    private String Text;
    private String Name;
    private String SurName;
    public void  Input()
    {
        super.Input();

        Scanner in = new Scanner(System.in);
        System.out.print("Input CertificateNumber: ");
        CertificateNumber = in.nextInt();

        System.out.print("Input Text: ");
        Text = in.next();

        System.out.print("Input Name: ");
        Name = in.next();
        System.out.print("Input SurName: ");
        SurName = in.next();

    }
    public void  Output()
    {
        super.Output();
        System.out.println("CertificateNumber: " + CertificateNumber + " ");
        System.out.println("Text: " + Text + " ");
        System.out.println("Name: " + Name + " ");
        System.out.println("SurName: " + SurName + " ");
        System.out.println("------------------");


    }

    public int getCertificateNumber() {
        return CertificateNumber;
    }

    public void setText(String text) {
        Text = text;
    }

    public String getName() {
        return Name;
    }

    public String getSurName() {
        return SurName;
    }

    public String getText() {
        return Text;
    }

    public void setCertificateNumber(int certificateNumber) {
        CertificateNumber = certificateNumber;
    }

    public void setName(String name) {
        Name = name;
    }

    public void setSurName(String surName) {
        SurName = surName;
    }

    public int compareTo(Certificate member) {
        if (this.SurName.equals(member.getSurName())) {
            return 0;
        } else if (!(this.SurName.equals(member.getSurName()))) {
            return 1;
        } else {
            return -1;
        }
    }

}
