package com.company;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class ListDocument<T extends Document>
{
    private List<T> _trainList  = new ArrayList<T>();
    private T Doc;

    public enum MyEnum {
        Sort,
        ReversSort;
    }
    public void SortList(int choose,int chooseForAt)
    {
       /* List<Document> fakeList  = new ArrayList<Document>();
        for(Document train: _trainList )
        {
            fakeList.add(train);
        }
        _trainList.removeAll(_trainList);
        MyEnum myEnum = null;
        Document.MyEnumForSort myEnumForSort = null;
        myEnumForSort = myEnumForSort.Name;
        if(choose == 1) {
            myEnum = myEnum.Sort;
        }
        if(choose == 2) {
            myEnum = myEnum.ReversSort;
        }


        if(chooseForAt == 2) {
            myEnumForSort = myEnumForSort.SurName;
        }
        if(chooseForAt == 3) {
            myEnumForSort = myEnumForSort.DateIssue;
        }

        if(myEnum ==  myEnum.Sort) {

                if(myEnumForSort == myEnumForSort.Name) {
                    _trainList = fakeList.stream()
                            .sorted(Comparator.comparing(Document::getName))
                            .collect(Collectors.toList());
                }
            if(myEnumForSort == myEnumForSort.SurName) {
                _trainList = fakeList.stream()
                        .sorted(Comparator.comparing(Document::getSurName))
                        .collect(Collectors.toList());
            }
            if(myEnumForSort == myEnumForSort.DateIssue) {
                _trainList = fakeList.stream()
                        .sorted(Comparator.comparing(Document::getDateIssue))
                        .collect(Collectors.toList());
            }

            }

        if(myEnum ==  myEnum.ReversSort) {
            if(myEnumForSort == myEnumForSort.Name) {
                _trainList = fakeList.stream()
                        .sorted(Comparator.comparing(Document::getName))
                        .collect(Collectors.toList());
            }
            if(myEnumForSort == myEnumForSort.SurName) {
                _trainList = fakeList.stream()
                        .sorted(Comparator.comparing(Document::getSurName))
                        .collect(Collectors.toList());
            }
            if(myEnumForSort == myEnumForSort.DateIssue) {
                _trainList = fakeList.stream()
                        .sorted(Comparator.comparing(Document::getDateIssue))
                        .collect(Collectors.toList());
            }
        }*/

    }

    public void GetList()
    {
        Scanner in = new Scanner(System.in);

        int choose = 0;
        System.out.print("What do you want to choose {1 - Passport}-{2 - Pass}-{3 - Certificate}-{4 - All}: ");
        choose = in.nextInt();
        for(Document document: _trainList )
        {
            var CheckClass = document.getClass();
            if(choose == 1) {
                if ("com.company.Passport".equals(CheckClass.getName())) {
                    Passport passport = new Passport();
                    passport = (Passport) document;
                    passport.Output();
                }
            }
            if(choose == 2) {
                if ("com.company.Pass".equals(CheckClass.getName())) {
                    Pass pass = new Pass();
                    pass = (Pass) document;
                    pass.Output();
                }
            }
            if(choose == 3) {
                if ("com.company.Certificate".equals(CheckClass.getName())) {
                    Certificate certificate = new Certificate();
                    certificate = (Certificate) document;
                    certificate.Output();
                }
            }
            if(choose == 4) {
                document.Output();
                }
        }
    }
    public void PostList()
    {
        Scanner in = new Scanner(System.in);
        Document document = null;
        int choose = 0;
        while (choose != 4)
        {
            System.out.print("What do you want to choose {1 - Passport}-{2 - Pass}-{3 - Certificate}-{4 - exit}: ");
            choose = in.nextInt();
            if(choose == 4 ) break;
            switch (choose) {
                case 1:
                    document = new Passport();

                    break;
                case 2:
                    document = new Pass();
                    break;
                case 3:
                    document = new Certificate();
                    break;
                default:break;
            }
            document.Input();
            _trainList.add((T) document);

        }
    }
    public void AddToEnd()
    {

        System.out.print("Input elements to list: ");
        Doc.Input();
        _trainList.add (Doc);
    }
    public void AddToCenter(T D)
    {
        System.out.print("Input elements to list: ");
        D.Input();
        var MiddleNumber = _trainList.size()/2;
        _trainList.add(MiddleNumber, D);
    }
    public <V> void Delete(V number)
    {
        Scanner in = new Scanner(System.in);

        _trainList.remove(number);

    }
    public int GetCount()
    {
        return  _trainList.size();
    }
    public void GetOne() {
        Scanner in = new Scanner(System.in);
        var counter = 0;
        System.out.print("input to index: ");
        int number = in.nextInt();
        for (Document train : _trainList) {
            if (number == counter) {
                train.Output();
                System.out.println(" ");
            }
            counter++;
        }
    }
    public void Edit()
    {
        Scanner in = new Scanner(System.in);
        var counter = 0;
        System.out.print("input to index: ");
        int number = in.nextInt();
        for (Document train : _trainList) {
            if (number == counter) {
                train.Input();

                _trainList.set(number, (T) train);

            }
            counter++;
        }

    }
    public void GetType()
    {
        Scanner in = new Scanner(System.in);

        for (Document train : _trainList) {
            System.out.println(train.getClass().getName().toString());
        }
    }

}
