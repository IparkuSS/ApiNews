package com.company;

import java.util.Scanner;

public class Passport extends Document implements Comparable<Document>
{
    private String Serial;
    private String Number;
    private String Organ;




    public void  Input()
    {
        super.Input();

        Scanner in = new Scanner(System.in);
        System.out.print("Input Serial: ");
        Serial = in.next();

        System.out.print("Input Number: ");
        Number = in.next();

        System.out.print("Input Organ: ");
        Organ = in.next();

    }
    public void  Output()
    {
        super.Output();
        System.out.println("Serial: " + Serial + " ");
        System.out.println("Number: " + Number + " ");
        System.out.println("Organ: " + Organ + " ");

        System.out.println("------------------");

    }
    public String getNumber() {
        return Number;
    }

    public void setNumber(String number) {
        Number = number;
    }

    public String getOrgan() {
        return Organ;
    }

    public void setOrgan(String organ) {
        Organ = organ;
    }

    public String getSerial() {
        return Serial;
    }

    public void setSerial(String serial) {
        Serial = serial;
    }

    public int compareTo(Passport member) {
        if (this.Serial.equals(member.getSerial())) {
            return 0;
        } else if (!(this.Serial.equals(member.getSerial()))) {
            return 1;
        } else {
            return -1;
        }
    }


}
