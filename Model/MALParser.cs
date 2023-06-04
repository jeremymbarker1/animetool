using AnimeTool.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnimeTool.Model
{
    internal class MALParser
    {
        public async Task<(MALInfo, List<AnimeItem>)> OnImportListAsync()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".xml");
            var file = await picker.PickSingleFileAsync();
            if (file == null) return (null, null);
            var contents = await Windows.Storage.FileIO.ReadTextAsync(file);
            return ImportList(contents);
        }

        private (MALInfo, List<AnimeItem>) ImportList(string xmlstring)
        {
            var xml = XElement.Parse(xmlstring);

            var myinfo = xml.Descendants("myinfo").First();
            var info = new MALInfo
                (
                    myinfo.Element("user_id").Value,
                    myinfo.Element("user_name").Value,
                    myinfo.Element("user_export_type").Value,
                    myinfo.Element("user_total_anime").Value,
                    myinfo.Element("user_total_watching").Value,
                    myinfo.Element("user_total_completed").Value,
                    myinfo.Element("user_total_onhold").Value,
                    myinfo.Element("user_total_dropped").Value,
                    myinfo.Element("user_total_plantowatch").Value
                );

            var list = new List<AnimeItem>();
            foreach (var anime in xml.Descendants("anime"))
            {
                var item = new MALItem
                (
                     anime.Element("series_animedb_id").Value,
                     anime.Element("series_title").Value,
                     anime.Element("series_type").Value,
                     anime.Element("series_episodes").Value,
                     anime.Element("my_id").Value,
                     anime.Element("my_watched_episodes").Value,
                     anime.Element("my_start_date").Value,
                     anime.Element("my_finish_date").Value,
                     anime.Element("my_rated").Value,
                     anime.Element("my_score").Value,
                     anime.Element("my_storage").Value,
                     anime.Element("my_storage_value").Value,
                     anime.Element("my_status").Value,
                     anime.Element("my_comments").Value,
                     anime.Element("my_times_watched").Value,
                     anime.Element("my_rewatch_value").Value,
                     anime.Element("my_priority").Value,
                     anime.Element("my_tags").Value,
                     anime.Element("my_rewatching").Value,
                     anime.Element("my_rewatching_ep").Value,
                     anime.Element("my_discuss").Value,
                     anime.Element("my_sns").Value,
                     anime.Element("update_on_import").Value
                );
                list.Add(new AnimeItem(item));
            }
            return (info, list);
        }

        public async Task OnExportListAsync(MALInfo info, List<AnimeItem> list)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();
            picker.FileTypeChoices.Add("XML file", new List<string>() { ".xml" });
            var file = await picker.PickSaveFileAsync();
            if (file == null) return;
            var xml = GetXmlString(info, list);
            await Windows.Storage.FileIO.WriteTextAsync(file, xml);
        }

        private string GetXmlString(MALInfo info, List<AnimeItem> list)
        {
            var doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));

            XElement myanimelist =
                new XElement("myanimelist",
                    new XElement("myinfo",
                        new XElement("user_id", info.user_id),
                        new XElement("user_name", info.user_name),
                        new XElement("user_export_type", info.user_export_type),
                        new XElement("user_total_anime", info.user_total_anime),
                        new XElement("user_total_watching", info.user_total_watching),
                        new XElement("user_total_completed", info.user_total_completed),
                        new XElement("user_total_onhold", info.user_total_onhold),
                        new XElement("user_total_dropped", info.user_total_dropped),
                        new XElement("user_total_plantowatch", info.user_total_plantowatch)));
            foreach (var anime in list)
            {
                myanimelist.Add(new XElement
                    ("anime",
                        new XElement("series_animedb_id", anime.Series_Anime_DB_ID),
                        new XElement("series_title", new XCData(anime.Series_Title)),
                        new XElement("series_type", anime.Series_Type),
                        new XElement("series_episodes", anime.Series_Episodes),
                        new XElement("my_id", anime.My_ID),
                        new XElement("my_watched_episodes", anime.My_Watched_Episodes),
                        new XElement("my_start_date", anime.My_Start_Date),
                        new XElement("my_finish_date", anime.My_Finish_Date),
                        new XElement("my_rated", anime.My_Rated),
                        new XElement("my_score", anime.My_Score),
                        new XElement("my_storage", anime.My_Storage),
                        new XElement("my_storage_value", anime.My_Storeage_Value),
                        new XElement("my_status", anime.My_Status),
                        new XElement("my_comments", new XCData(anime.My_Comments)),
                        new XElement("my_times_watched", anime.My_Times_Watched),
                        new XElement("my_rewatch_value", anime.My_Rewatch_Value),
                        new XElement("my_priority", anime.My_Priority),
                        new XElement("my_tags", new XCData(anime.My_Tags)),
                        new XElement("my_rewatching", anime.My_Rewatching),
                        new XElement("my_rewatching_ep", anime.My_Rewatching_Ep),
                        new XElement("my_discuss", anime.My_Discuss),
                        new XElement("my_sns", anime.My_SNS),
                        new XElement("update_on_import", anime.Update_On_Import)
                    ));
            }

            doc.Add(myanimelist);
            return string.Format("{0}\n{1}", doc.Declaration.ToString(), doc.ToString());
        }
    }
}
