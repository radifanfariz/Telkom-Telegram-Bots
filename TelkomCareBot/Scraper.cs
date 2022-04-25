using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace TelkomCareBot
{
    public class Scraper
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument document;
        BotMachine botMachine;
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
            botMachine.navigateToTheData();
            document.LoadHtml(@botMachine.driver.PageSource);
        }

        public string htmlCheck()
        {
            return botMachine.driver.PageSource;
        }

        public HtmlNode[] selectionData()
        {
            return document.DocumentNode.SelectNodes("//div[@class='panel-body square1-body bgcell']").ToArray();
        }

        public async void getMsgsTextAsync()
        {
            HtmlNode[] nodes = selectionData();
            //var htmlString = new StringBuilder();
            String htmlString = "";
            foreach (HtmlNode item in nodes)
            {
                //Trace.WriteLine(item.InnerText);
                //htmlString.Append(item.OuterHtml);
                htmlString += item.OuterHtml;

            }
            //String newHtmlString = ReplaceWhitespace(htmlString.ToString(), "###");
            List<String> tags = new List<string>() { "td", "b", "form", "h3", "input", "a", "i", "span","table","tbody","tr","img", "div" };
            String newHtmlString =RemoveUnwantedHtmlTags(htmlString, tags);
            newHtmlString = ReplaceWhitespace(newHtmlString, "\n");
            //to make msg to array (basically make message is sended by telegram one by one)
            //String[] strArray = stringToStringArray(newHtmlString);
            await File.WriteAllTextAsync("Z:\\Visual Studio Projects\\htmlscrape.txt", newHtmlString);
            //make extracted html file for all data
            //await File.WriteAllTextAsync("Z:\\Visual Studio Projects\\htmlscrape.txt", newHtmlString);
        }

        public String getMsgsText()
        {
            try
            {
                HtmlNode[] nodes = selectionData();
                //var htmlString = new StringBuilder();
                String htmlString = "";
                foreach (HtmlNode item in nodes)
                {
                    //Trace.WriteLine(item.OuterHtml);
                    //htmlString.Append(item.OuterHtml);
                    htmlString += item.OuterHtml;
                }
                //String newHtmlString = ReplaceWhitespace(htmlString.ToString(), "###");
                List<String> tags = new List<string>() { "td", "b", "form", "h3", "input", "a", "i", "span", "table", "tbody", "tr", "img", "div" };
                String newHtmlString = RemoveUnwantedHtmlTags(htmlString, tags);
                newHtmlString =ReplaceWhitespace(newHtmlString, "\n");
                String[] strArray = stringToStringArray(newHtmlString);
                //Trace.WriteLine(strArray[2]);
                return stringFiltering(strArray);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //String[] error = { ex.ToString() };
                return ex.ToString();
            }
        }

        public String[] stringToStringArray(string input, string pattern = "@")
        {
            return input.Split(pattern);
        }

        public String stringFiltering(String[] input)
        {
            String stringafterFiltering ="";
            foreach(String itemText in input)
            {
                if (pattern7.IsMatch(itemText))
                {
                    stringafterFiltering += itemText;
                    stringafterFiltering += "\n";
                }
            }
            return stringafterFiltering;
        }

        private static readonly Regex pattern = new Regex(@"&nbsp;");
        private static readonly Regex pattern2 = new Regex(@"[^\S\r\n]+");
        private static readonly Regex pattern3 = new Regex(@"<[^>]*>");
        private static readonly Regex pattern4 = new Regex(@"#");
        private static readonly Regex pattern5 = new Regex(@"[^\S]+");
        private static readonly Regex pattern6 = new Regex(@"K[\d]+");
        private static readonly Regex pattern7 = new Regex(@"SUMSEL");

        public string ReplaceWhitespace(string input, string replacement)
        {
            String newInput = pattern.Replace(input, "");
            newInput = pattern2.Replace(newInput, "");
            newInput = pattern4.Replace(newInput.Trim(), "@#");
            newInput = pattern3.Replace(newInput, "\n");
            newInput = pattern6.Replace(newInput, "");
            return pattern5.Replace(newInput.Trim(), replacement);
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
