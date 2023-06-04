namespace AnimeTool.Model
{
    // List item compatible with MyAnimeList
    class MALItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string series_animedb_id { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string series_title { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string series_type { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string series_episodes { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_id { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_watched_episodes { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_start_date { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_finish_date { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_rated { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_score { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_storage_value { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_storage { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_status { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_comments { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_times_watched { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_rewatch_value { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_priority { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_tags { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_rewatching { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_rewatching_ep { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_discuss { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string my_sns { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string update_on_import { get; }

        public MALItem(string _series_animedb_id,
                       string _series_title,
                       string _series_type,
                       string _series_episodes,
                       string _my_id,
                       string _my_watched_episodes,
                       string _my_start_date,
                       string _my_finish_date,
                       string _my_rated,
                       string _my_score,
                       string _my_storage,
                       string _my_storage_value,
                       string _my_status,
                       string _my_comments,
                       string _my_times_watched,
                       string _my_rewatch_value,
                       string _my_priority,
                       string _my_tags,
                       string _my_rewatching,
                       string _my_rewatching_ep,
                       string _my_discuss,
                       string _my_sns,
                       string _update_on_import)
        {
            // initial all fields
            series_animedb_id = _series_animedb_id;
            series_title = _series_title;
            series_type = _series_type;
            series_episodes = _series_episodes;
            my_id = _my_id;
            my_watched_episodes = _my_watched_episodes;
            my_start_date = _my_start_date;
            my_finish_date = _my_finish_date;
            my_rated = _my_rated;
            my_score = _my_score;
            my_storage = _my_storage;
            my_storage_value = _my_storage_value;
            my_status = _my_status;
            my_comments = _my_comments;
            my_times_watched = _my_times_watched;
            my_rewatch_value = _my_rewatch_value;
            my_priority = _my_priority;
            my_tags = _my_tags;
            my_rewatching = _my_rewatching;
            my_rewatching_ep = _my_rewatching_ep;
            my_discuss = _my_discuss;
            my_sns = _my_sns;
            update_on_import = _update_on_import;
        }
    }
}