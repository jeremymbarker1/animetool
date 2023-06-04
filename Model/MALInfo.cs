namespace AnimeTool.Model
{
    internal class MALInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string user_id { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string user_name { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string user_export_type { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string user_total_anime { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string user_total_watching { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string user_total_completed { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string user_total_onhold { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string user_total_dropped { get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Match MAL format")]
        public string user_total_plantowatch { get; }

        public MALInfo(string _user_id,
                       string _user_name,
                       string _user_export_type,
                       string _user_total_anime,
                       string _user_total_watching,
                       string _user_total_completed,
                       string _user_total_onhold,
                       string _user_total_dropped,
                       string _user_total_plantowatch)
        {
            user_id = _user_id;
            user_name = _user_name;
            user_export_type = _user_export_type;
            user_total_anime = _user_total_anime;
            user_total_watching = _user_total_watching;
            user_total_completed = _user_total_completed;
            user_total_onhold = _user_total_onhold;
            user_total_dropped = _user_total_dropped;
            user_total_plantowatch = _user_total_plantowatch;
        }
    }
}
