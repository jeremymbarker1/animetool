using AnimeTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows.Input;

namespace AnimeTool.Binding
{
    internal class AnimeItem : ObservableObject
    {
        // keep myanimelist item for original values
        private readonly MALItem _animeItem;

        #region Immutable MAL details

        public string Series_Anime_DB_ID { get => _animeItem.series_animedb_id; }
        public string Series_Title { get => _animeItem.series_title; }
        public string Series_Type { get => _animeItem.series_type; }
        public string Series_Episodes { get => _animeItem.series_episodes; }

        #endregion

        #region MAL user details

        // Status
        //    Watching
        //    Completed
        //    On-Hold
        //    Dropped
        //    Plan to Watch
        private string my_status;
        public string My_Status
        {
            get => my_status;
            set { if (SetProperty(ref my_status, value)) ApplyUpdate(); }
        }

        // Episodes Watched
        //    int [0-series_episodes] (0 = default)
        private string my_watched_episodes;
        public string My_Watched_Episodes
        {
            get => my_watched_episodes;
            set { if (SetProperty(ref my_watched_episodes, value)) ApplyUpdate(); }
        }

        // Re-watching
        //    0 (default)
        //    1
        private string my_rewatching;
        public string My_Rewatching
        {
            get => my_rewatching;
            set { if (SetProperty(ref my_rewatching, value)) ApplyUpdate(); }
        }

        // Episodes Watched
        //    * when my_status=Completed & my_rewatching=1
        //    int [0-series_episodes] (1 = default)
        private string my_rewatching_ep;
        public string My_Rewatching_Ep
        {
            get => my_rewatching_ep;
            set { if (SetProperty(ref my_rewatching_ep, value)) ApplyUpdate(); }
        }

        // Your Score
        //    int [0-10] (0 = default)
        public List<string> Scores { get; }
        private string my_score;
        public string My_Score
        {
            get => my_score;
            set
            {
                // value is null on selected item change
                if (value == null) return;
                if (SetProperty(ref my_score, value)) ApplyUpdate();
            }
        }

        // Start Date
        //    yyyy-mm-dd
        //    0000-00-00 = Unknown Date (default)
        private string my_start_date;
        public string My_Start_Date
        {
            get => my_start_date;
            set { if (SetProperty(ref my_start_date, value)) ApplyUpdate(); }
        }

        // Finish Date
        //    yyyy-mm-dd
        //    0000-00-00 = Unknown Date (default)
        private string my_finish_date;
        public string My_Finish_Date
        {
            get => my_finish_date;
            set { if (SetProperty(ref my_finish_date, value)) ApplyUpdate(); }
        }

        #endregion

        #region MAL advancced user details

        // Tags
        //    text field (empty = default)
        private string my_tags;
        public string My_Tags
        {
            get => my_tags;
            set { if (SetProperty(ref my_tags, value)) ApplyUpdate(); }
        }

        // Priority
        //    Low    (LOW = default)
        //    Medium (MEDIUM)
        //    High   (HIGH)
        private string my_priority;
        public string My_Priority
        {
            get => my_priority;
            set { if (SetProperty(ref my_priority, value)) ApplyUpdate(); }
        }

        // Storage
        //    Select storage type (empty = default)
        //    Hard Drive
        //    External HD
        //    NAS
        //    Blu-Ray
        //    DVD / CD
        //    Retail DVD
        //    VHS
        //    None
        private string my_storage;
        public string My_Storage
        {
            get => my_storage;
            set { if (SetProperty(ref my_storage, value)) ApplyUpdate(); }
        }

        // Total Times Re-watched Series
        //    int (>0 --- 0 = default)
        private string my_times_watched;
        public string My_Times_Watched
        {
            get => my_times_watched;
            set { if (SetProperty(ref my_times_watched, value)) ApplyUpdate(); }
        }

        // Rewatch Value
        //    Select rewatch value (empty = default)
        //    Very Low
        //    Low
        //    Medium
        //    High
        //    Very High
        private string my_rewatch_value;
        public string My_Rewatch_Value
        {
            get => my_rewatch_value;
            set { if (SetProperty(ref my_rewatch_value, value)) ApplyUpdate(); }
        }

        // Notes
        private string my_comments;
        public string My_Comments
        {
            get => my_comments;
            set { if (SetProperty(ref my_comments, value)) ApplyUpdate(); }
        }

        // Ask to Discuss?
        //    Ask to discuss an episode after you update your episode count (1 = default)
        //    Do not ask to discuss (0)
        private string my_discuss;
        public string My_Discuss
        {
            get => my_discuss;
            set { if (SetProperty(ref my_discuss, value)) ApplyUpdate(); }
        }

        // Post to SNS
        //    Follow default setting (default = default)
        //    Post with confirmation
        //    Post every time (without confirmation)
        //    Do not post
        private string my_sns;
        public string My_SNS
        {
            get => my_sns;
            set { if (SetProperty(ref my_sns, value)) ApplyUpdate(); }
        }

        #endregion

        #region MAL Unused details

        public string My_ID { get => _animeItem.my_id; }
        public string My_Rated { get => _animeItem.my_rated; }
        public string My_Storeage_Value { get => _animeItem.my_storage_value; }

        #endregion

        #region MAL export/import

        private string update_on_import;
        public string Update_On_Import
        {
            get => update_on_import;
            set => SetProperty(ref update_on_import, value);
        }

        #endregion

        private bool isFiltered;
        public bool IsFiltered
        {
            get => isFiltered;
            set => SetProperty(ref isFiltered, value);
        }

        private bool isHidden;
        public bool IsHidden
        {
            get => isHidden;
            set => SetProperty(ref isHidden, value);
        }

        public ICommand OnIncrementScoreCommand { get; }
        public ICommand OnDecrementScoreCommand { get; }
        public ICommand OnRestoreOriginalValuesCommand { get; }

        public AnimeItem(MALItem animeItem)
        {
            _animeItem = animeItem;
            My_Status = _animeItem.my_status;
            My_Watched_Episodes = _animeItem.my_watched_episodes;
            My_Rewatching = _animeItem.my_rewatching;
            My_Rewatching_Ep = _animeItem.my_rewatching_ep;
            My_Score = _animeItem.my_score;
            My_Start_Date = _animeItem.my_start_date;
            My_Finish_Date = _animeItem.my_finish_date;
            My_Tags = _animeItem.my_tags;
            My_Priority = _animeItem.my_priority;
            My_Storage = _animeItem.my_storage;
            My_Times_Watched = _animeItem.my_times_watched;
            My_Rewatch_Value = _animeItem.my_rewatch_value;
            My_Comments = _animeItem.my_comments;
            My_Discuss = _animeItem.my_discuss;
            My_SNS = _animeItem.my_sns;
            Update_On_Import = _animeItem.update_on_import;

            Scores = new List<string> { "10", "9", "8", "7", "6", "5", "4", "3", "2", "1", "0" };

            IsHidden = false;
            IsFiltered = false;

            OnRestoreOriginalValuesCommand = new RelayCommand(RestoreOriginalValues);
        }

        private void ApplyUpdate()
        {
            var isChanged = My_Status != _animeItem.my_status ||
                            My_Watched_Episodes != _animeItem.my_watched_episodes ||
                            My_Rewatching != _animeItem.my_rewatching ||
                            My_Rewatching_Ep != _animeItem.my_rewatching_ep ||
                            My_Score != _animeItem.my_score ||
                            My_Start_Date != _animeItem.my_start_date ||
                            My_Finish_Date != _animeItem.my_finish_date ||
                            My_Tags != _animeItem.my_tags ||
                            My_Priority != _animeItem.my_priority ||
                            My_Storage != _animeItem.my_storage ||
                            My_Times_Watched != _animeItem.my_times_watched ||
                            My_Rewatch_Value != _animeItem.my_rewatch_value ||
                            My_Comments != _animeItem.my_comments ||
                            My_Discuss != _animeItem.my_discuss ||
                            My_SNS != _animeItem.my_sns;
            Update_On_Import = isChanged ? "1" : "0";
        }

        public void RestoreOriginalValues()
        {
            My_Status = _animeItem.my_status;
            My_Watched_Episodes = _animeItem.my_watched_episodes;
            My_Rewatching = _animeItem.my_rewatching;
            My_Rewatching_Ep = _animeItem.my_rewatching_ep;
            My_Score = _animeItem.my_score;
            My_Start_Date = _animeItem.my_start_date;
            My_Finish_Date = _animeItem.my_finish_date;
            My_Tags = _animeItem.my_tags;
            My_Priority = _animeItem.my_priority;
            My_Storage = _animeItem.my_storage;
            My_Times_Watched = _animeItem.my_times_watched;
            My_Rewatch_Value = _animeItem.my_rewatch_value;
            My_Comments = _animeItem.my_comments;
            My_Discuss = _animeItem.my_discuss;
            My_SNS = _animeItem.my_sns;
        }
    }
}
