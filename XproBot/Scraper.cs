using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace XproBot
{
    public class Scraper
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument document;
        BotMachine botMachine;
        IDictionary<HtmlNode[], HtmlNode[]> nodeDicts = new Dictionary<HtmlNode[], HtmlNode[]>();
        public Scraper(BotMachine botMachine)
        {
            this.botMachine = botMachine;
            document = new HtmlDocument();
            document.LoadHtml(@botMachine.driver.PageSource);
        }

        public Scraper(String path)
        {
            document = new HtmlDocument();
            document.Load(@path);
        }

        public void refreshSite()
        {
            botMachine.driver.Navigate().Refresh();
            document.LoadHtml(@botMachine.driver.PageSource);
        }

        public string htmlCheck()
        {
            return botMachine.driver.PageSource;
        }

        public HtmlNode[] selectionData(string xPath)
        {
            return document.DocumentNode.SelectNodes(xPath).ToArray();
        }

        public async Task getMsgsTextAsync()
        {
           HtmlNode[] nodes = selectionData("//tbody/tr/td[2]/a[1]");
            HtmlNode[] nodes2 = selectionData("//tbody/tr/td[3]");
            var nodeAll = nodes.Zip(nodes2, (x, y) => new { Nodes = x, Nodes2 = y });
            //var htmlString = new StringBuilder();
            String htmlString = "";
            foreach(var item in nodeAll)
            {

                htmlString += item.Nodes.OuterHtml;
                htmlString += "\n";
                htmlString += item.Nodes2.OuterHtml;
                htmlString += "################################ \n\n";
            }
            Trace.WriteLine(htmlString);
            //foreach (HtmlNode item in nodes)
            //{
            //    //Trace.WriteLine(item.InnerText);
            //    htmlString.Append(item.OuterHtml);
            //}
            //String newHtmlString = ReplaceWhitespace(htmlString.ToString(), "###");
            List<String> tags = new List<string>() { "td", "b", "form", "h3", "input", "a", "i", "span", "div" };
            String newHtmlString = RemoveUnwantedHtmlTags(htmlString, tags);
            newHtmlString = ReplaceWhitespace(newHtmlString, "\n");
            //String[] strArray = stringToStringArray(newHtmlString);
            await File.WriteAllTextAsync("Z:\\Visual Studio Projects\\htmlscrape.txt", newHtmlString);
            //make extracted html file for all data
            //await File.WriteAllTextAsync("Z:\\Visual Studio Projects\\htmlscrape.txt", newHtmlString);
        }

        public String getMsgsText()
        {
            try
            {
                HtmlNode[] nodes = selectionData("//tbody/tr/td[2]/a[1]");
                HtmlNode[] nodes2 = selectionData("//tbody/tr/td[3]");
                String[] newNodes2 = { };
                var nodeAll = nodes.Zip(nodes2, (x, y) => new { Nodes = x, Nodes2 = y });
                String htmlString = "";
                foreach (var item in nodeAll)
                {

                    htmlString += item.Nodes.OuterHtml;
                    htmlString += "\n";
                    htmlString += item.Nodes2.OuterHtml;
                }
                Trace.WriteLine(htmlString);
                //String newHtmlString = ReplaceWhitespace(htmlString.ToString(), "###");
                List<String> tags = new List<string>() { "td", "b", "form", "h3", "input", "a", "i", "span", "div" };
                String newHtmlString = RemoveUnwantedHtmlTags(htmlString.ToString(), tags);
                newHtmlString = ReplaceWhitespace(newHtmlString, "\n");
                //String[] strArray = stringToStringArray(newHtmlString);
                //Trace.WriteLine(strArray[2]);
                //stringToStringArray(newHtmlString);
                return newHtmlString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ex.ToString();
            }
        }

        /////////////////////// OLD getmsgText() Method///////////////////////////

        //public String[] getMsgsText()
        //{
        //    try
        //    {
        //        HtmlNode[] nodes = selectionData();
        //        var htmlString = new StringBuilder();
        //        foreach (HtmlNode item in nodes)
        //        {
        //            //Trace.WriteLine(item.OuterHtml);
        //            htmlString.Append(item.OuterHtml);
        //        }
        //        //String newHtmlString = ReplaceWhitespace(htmlString.ToString(), "###");
        //        List<String> tags = new List<string>() { "td", "b", "form", "h3", "input", "a", "i", "span", "div" };
        //        String newHtmlString = RemoveUnwantedHtmlTags(htmlString.ToString(), tags);
        //        newHtmlString = ReplaceWhitespace(newHtmlString, "\n");
        //        //String[] strArray = stringToStringArray(newHtmlString);
        //        //Trace.WriteLine(strArray[2]);
        //        return stringToStringArray(newHtmlString);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        String[] error = { ex.ToString() };
        //        return error;
        //    }
        //}

        public string[] stringToStringArray(string input)
        {
            return input.Split("AddLog");
        }

        private static readonly Regex pattern = new Regex(@"&nbsp;");
        private static readonly Regex pattern2 = new Regex(@"[^\S\r\n]+");
        private static readonly Regex pattern3 = new Regex(@"<[^>]*>");
        private static readonly Regex pattern4 = new Regex(@"<br>");

        public string ReplaceWhitespace(string input,  string replacement)
        {
            String newInput = pattern.Replace(input, " ");
            String newInput2 = pattern2.Replace(newInput, " ");
            return pattern4.Replace(newInput2.Trim(), replacement);
        }

        public string RemoveUnwantedHtmlTags(string html, List<string> unwantedTags)
        {
            if (String.IsNullOrEmpty(html))
            {
                return html;
            }

            var document = new HtmlDocument();
            document.LoadHtml(html);

            HtmlNodeCollection tryGetNodes = document.DocumentNode.SelectNodes("./*|./text()");

            if (tryGetNodes == null || !tryGetNodes.Any())
            {
                return html;
            }

            var nodes = new Queue<HtmlNode>(tryGetNodes);

            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                var parentNode = node.ParentNode;

                var childNodes = node.SelectNodes("./*|./text()");

                if (childNodes != null)
                {
                    foreach (var child in childNodes)
                    {
                        nodes.Enqueue(child);
                    }
                }

                if (unwantedTags.Any(tag => tag == node.Name))
                {
                    if (childNodes != null)
                    {
                        foreach (var child in childNodes)
                        {
                            parentNode.InsertBefore(child, node);
                        }
                    }

                    parentNode.RemoveChild(node);

                }
            }

            return document.DocumentNode.InnerHtml;
        }


    }
}
