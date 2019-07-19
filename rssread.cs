using System;
using System.Xml;
using SplashKitSDK;
using System.Text.RegularExpressions;


public class rssread
{
    private XmlDocument doc;
    private XmlNodeList xmlList;

    public rssread(string type)
    {
        //get rss 
        HttpResponse _response =  SplashKit.HttpGet($"https://www.news.com.au/content-feeds/latest-news-{type}/", 443);

        //save the xml file to our program directory
        SplashKit.SaveResponseToFile(_response, "new.xml");

        //xml
        doc = new XmlDocument();
        doc.Load("new.xml");

    }

    public void ShowNews()
    {
        //list out the news title
        xmlList = doc.SelectNodes("/rss/channel/item");
        foreach (XmlNode node in xmlList)
        {
            string title  = node["title"].InnerText;
        }

        for (int i = 0; i < xmlList.Count; i++)
        {
            Console.WriteLine($"{i+1}. {xmlList[i]["title"].InnerText}");
        }


    }


    public void ReadNews(int num)
    {
        string _newscontent;
        string _output = "";
        Match _match;
        
        HttpResponse _response = SplashKit.HttpGet($"{xmlList[num-1]["link"].InnerText}", 443);
        _newscontent = SplashKit.HttpResponseToString(_response);

        
        //filter the content, i found out that the news article is stored in <p>, so we basically get all content in <p>
        _match = Regex.Match( _newscontent, @"<\s*p[^>]*>(.*?)<\s*/\s*p>");
        while (_match.Success) 
        {
        _output = _output + _match.Value;
        _match = _match.NextMatch();
        }

        //remove unwanted html tag.
        _output = Regex.Replace(_output,"<[^>]*>","");

        //remove unwanted unicode hex character.
        _output = Regex.Replace(_output,@"&[^;]{1,6};","");


        Console.WriteLine($"\n{xmlList[num-1]["title"].InnerText}\n");
        Console.WriteLine(_output);  
    }





}