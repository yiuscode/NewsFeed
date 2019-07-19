using System;
using SplashKitSDK;

public enum MenuOption
{
    national,
    world,
    lifestyle,
    travel,
    entertainment
}




public class Program
{
    

    public static void Main()
    {
        rssread _rssread;
        int option;
        
        Console.WriteLine("Welcome to 7News reader, what kind of news you would like to read?\n\n");
        Console.WriteLine("Enter 1 - National.");
        Console.WriteLine("Enter 2 - World.");
        Console.WriteLine("Enter 3 - Lifestyle");
        Console.WriteLine("Enter 4 - Travel");
        Console.WriteLine("Enter 5 - Entertainment");

        option = Convert.ToInt32(Console.ReadLine());
        
        //create an rss read object
        _rssread = new rssread(((MenuOption)option-1).ToString());

        //show all news from chosen type
        _rssread.ShowNews();

        Console.WriteLine("Which one you would like to read?");
        option = Convert.ToInt32(Console.ReadLine());

        //read chosen news
        _rssread.ReadNews(option);
        
    }
}
