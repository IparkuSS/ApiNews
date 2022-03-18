package com.company;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Main
{

    public static void main(String[] args)
    {
        Scanner in = new Scanner(System.in);

        ListDocument<Document> listDocument = new ListDocument<Document>();
        listDocument.PostList();
        System.out.print("What do you want to choose {1 add element to end }-{2 add element to center}-{3 delete element }-{4 edit element}-{5 get one element}-{6 get size list}-{7 get type}-{8 Sort List}-{9 outPut all list}-{10 exit}: ");
        var chooseTMP = 0;
        while (chooseTMP != 10)
        {
            chooseTMP = in.nextInt();
            switch (chooseTMP)
            {
                case 1:
                    listDocument.AddToEnd();
                    break;
                case 2:
                    Passport passport = new Passport();
                    listDocument.AddToCenter(passport);
                    break;
                case 3:
                    System.out.print("What do you want to delete: ");
                    int number = in.nextInt();
                    listDocument.<Integer>Delete(number);
                    break;
                case 4:
                    listDocument.Edit();
                    break;
                case 5:
                    listDocument.GetOne();
                    break;
                case 6:
                    System.out.print(listDocument.GetCount());
                    break;
                case 7:
                    listDocument.GetType();
                    break;
                case 8:
                    System.out.print("What do you want to choose for sort {1 - sort}-{2 - revers sort}: ");
                    int choose = in.nextInt();

                    System.out.print("What do you want to choose for sort {1 - Name}-{2 - SurName}-{3 - DateIssue}: ");
                    int chooseForAt = in.nextInt();

                    listDocument.SortList(choose,chooseForAt);
                    break;
                case 9:
                    listDocument.GetList();
                    break;
                case 10:
                    break;
            }
        }

    }
}
