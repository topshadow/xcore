using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft;
using Newtonsoft.Json;

namespace xcore.Area.Test.Service
{
    public enum ActivitySourceType
    {
        ActivityWang,

    }

    /// <summary>
    /// 活动汪
    /// </summary>
    public class CommonActivity
    {
        public string id { get; set; }
        public ActivitySourceType sourceType { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string tags { get; set; }
        public string price { get; set; }
        public string downloadNum { get; set; }
        public string authorName { get; set; }
        public string url { get; set; }
        public string viewNum { get; set; }
        public string viewPrice { get; set; }
        public string publishAt { get; set; }

    }

    /// <summary>
    /// 分词分组
    /// </summary>
    public class KeywordGroup
    {
        public string keyword { get; set; }
        public int workNum { get; set; }
        public int downloadNum { get; set; }
    }

    /// <summary>
    /// 用户分组
    /// </summary>
    public class UserGroup
    {
        public string nickname { get; set; }


    }


    public class ActivityWangService
    {

        /// <summary>
        /// 解析活动汪
        /// </summary>
        /// <param name="html"></param>
        public string parseActivityHtml(string filePath)
        {
            var html = File.ReadAllText(filePath);
            var xmlDocument = new HtmlDocument();
            xmlDocument.LoadHtml(html);
            var titleNodes = xmlDocument.DocumentNode.SelectSingleNode(@"/html/head/title");
            var title = titleNodes.InnerText;
            var authorName = xmlDocument.DocumentNode.SelectSingleNode(@"/html/body/div[2]/div[2]/div[2]/div[1]/div[1]").InnerText;
            var price = xmlDocument.DocumentNode.SelectSingleNode(@"/html/body/div[2]/div[2]/div[2]/div[2]/div[2]/div[2]").InnerText;
            var download = (from n in xmlDocument.DocumentNode.Descendants() where n.ParentNode.HasClass("download_count") select n.InnerText).FirstOrDefault();
            Console.Write(title + "下载量" + download);
            return title;

        }

        /// <summary>
        ///  把文件解析成通用活动数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public CommonActivity parseActivityHtml2CommonActivity(string filePath)
        {

            var html = File.ReadAllText(filePath);
            var xmlDocument = new HtmlDocument();
            xmlDocument.LoadHtml(html);
            var titleNodes = xmlDocument.DocumentNode.SelectSingleNode(@"/html/head/title");
            var title = titleNodes.InnerText;
            //Console.WriteLine(title);
            var authorName = xmlDocument.DocumentNode.SelectSingleNode(@"/html/body/div[2]/div[2]/div[2]/div[1]/div[1]").InnerText;
            var price = xmlDocument.DocumentNode.SelectSingleNode(@"/html/body/div[2]/div[2]/div[2]/div[2]/div[2]/div[2]").InnerText;
            //var download = xmlDocument.DocumentNode.SelectSingleNode(@"//*[@id="+"\""+"design_resource_file_3941\"]/div[2]/span")?.InnerText;
            var download = (from n in xmlDocument.DocumentNode.Descendants() where n.ParentNode.HasClass("download_count") select n.InnerText).FirstOrDefault();
            var viewNum = xmlDocument.DocumentNode.SelectSingleNode(@"/html/body/div[2]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]").InnerText;
           var halfPrice= xmlDocument.DocumentNode.SelectSingleNode(@"/html/body/div[2]/div[2]/div[2]/div[2]/div[3]/div[2]").InnerText;
            var tags =  string.Join(" ",(from a in xmlDocument.DocumentNode.Descendants() where a.ParentNode.HasClass("tags") && a.Name == "a" select a.InnerText));
            var publishAt = (from a in xmlDocument.DocumentNode.Descendants() where a.HasClass("item_content")&&a.InnerText.StartsWith("201") select a).LastOrDefault()?.InnerText;
            Console.Write(title + "下载量" + download);
            
            var id = filePath.Split("\\").Last();
            return new CommonActivity
            {
                id = id,
                title = title,
                authorName = authorName,
                price = price,
                downloadNum = download,
                url = "https://www.eventwang.cn/DesignResource/detail/design_resource_id/" + id,
                viewNum = viewNum,
                viewPrice= halfPrice,
                tags=tags,
                publishAt=publishAt
            };

        }

        /// <summary>
        /// 解析json
        /// </summary>
        /// <returns></returns>
        public List<CommonActivity> parseJsonFile()
        {
           var json= File.ReadAllText("activitys.json");
          return  JsonConvert.DeserializeObject<List<CommonActivity>>(json);
        }

        public void getAllActivity()
        {

            var dist = @"E:\download\www.eventwang.cn\DesignResource\detail\design_resource_id";
            var pages = Directory.GetFiles(dist);
            var activitys = new List<CommonActivity>();
            foreach (var file in pages)
            {
                var activity = this.parseActivityHtml2CommonActivity(file);
                activitys.Add(activity);
            }
            File.WriteAllText("activitys.json", JsonConvert.SerializeObject(activitys));

        }

        public string getSingleActivityHtmlText()
        {
            var dist = @"E:\download\www.eventwang.cn\DesignResource\detail\design_resource_id";
            var pages = Directory.GetFiles(dist);
            return pages[100];
        }

        public List<KeywordGroup> getKeywordsGroup()
        {
            return null;

        }

    }
}
