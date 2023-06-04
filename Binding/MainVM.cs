using AnimeTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace AnimeTool.Binding
{
    // ViewModel for data binding with MainPage
    internal class MainVM : ObservableObject
    {
        private ObservableCollection<AnimeItem> displayAnimeList;
        public ObservableCollection<AnimeItem> DisplayAnimeList
        {
            get => displayAnimeList;
            set => SetProperty(ref displayAnimeList, value);
        }

        private AnimeItem selectedAnimeItem;
        public AnimeItem SelectedAnimeItem
        {
            get => selectedAnimeItem;
            set => SetProperty(ref selectedAnimeItem, value);
        }

        private UserStats userStats;
        public UserStats UserStats
        {
            get => userStats;
            set => SetProperty(ref userStats, value);
        }

        public enum SortOptions { AlphabetAsc, AlphanetDes, ScoreAsc, ScoreDes };
        public List<SortOptions> SortOptionsList { get; } = new List<SortOptions>(Enum.GetValues(typeof(SortOptions)).Cast<SortOptions>());
        private SortOptions selectedSortOption;
        public SortOptions SelectedSortOption
        {
            get => selectedSortOption;
            set
            {
                if (!SetProperty(ref selectedSortOption, value)) return;
                SortAnimeList();
            }
        }

        public enum FilterOptions { All, NotScored, Modified, Hidden };
        public List<FilterOptions> FilterOptionsList { get; } = new List<FilterOptions>(Enum.GetValues(typeof(FilterOptions)).Cast<FilterOptions>());
        private FilterOptions selectedFilterOption;
        public FilterOptions SelectedFilterOption
        {
            get => selectedFilterOption;
            set
            {
                if (!SetProperty(ref selectedFilterOption, value)) return;
                FilterAnimeList();
            }
        }

        private bool isAllRadioChecked;
        public bool IsAllRadioChecked
        {
            get => isAllRadioChecked;
            set
            {
                SetProperty(ref isAllRadioChecked, value);
                FilterAnimeList();
            }
        }
        private bool isWatchingRadioChecked;
        public bool IsWatchingRadioChecked
        {
            get => isWatchingRadioChecked;
            set
            {
                SetProperty(ref isWatchingRadioChecked, value);
                FilterAnimeList();
            }
        }
        private bool isCompletedRadioChecked;
        public bool IsCompletedRadioChecked
        {
            get => isCompletedRadioChecked;
            set
            {
                SetProperty(ref isCompletedRadioChecked, value);
                FilterAnimeList();
            }
        }
        private bool isOnHoldRadioChecked;
        public bool IsOnHoldRadioChecked
        {
            get => isOnHoldRadioChecked;
            set
            {
                SetProperty(ref isOnHoldRadioChecked, value);
                FilterAnimeList();
            }
        }
        private bool isDroppedRadioChecked;
        public bool IsDroppedRadioChecked
        {
            get => isDroppedRadioChecked;
            set
            {
                SetProperty(ref isDroppedRadioChecked, value);
                FilterAnimeList();
            }
        }
        private bool isPlanToWatchRadioChecked;
        public bool IsPlanToWatchRadioChecked
        {
            get => isPlanToWatchRadioChecked;
            set
            {
                SetProperty(ref isPlanToWatchRadioChecked, value);
                FilterAnimeList();
            }
        }

        public ICommand OnImportListCommand { get; }
        public ICommand OnExportListCommand { get; }
        public ICommand OnShowAdvancedCommand { get; }
        public ICommand OnIncrementCommand { get; }
        public ICommand OnDecrementCommand { get; }
        public ICommand OnToggleHiddenCommand { get; }
        public ICommand OnZeroScoresCommand { get; }
        public ICommand RestoreOriginalCommand { get; }
        public ICommand UnhideAllCommand { get; }

        private readonly MALParser _parser;
        private List<AnimeItem> _animeList;
        private bool showHidden;

        public MainVM()
        {
            _parser = new MALParser();
            OnImportListCommand = new RelayCommand(OnImportList);
            OnExportListCommand = new RelayCommand(OnExportList);
            OnIncrementCommand = new RelayCommand<AnimeItem>((x) => IncrementScore(x, 1));
            OnDecrementCommand = new RelayCommand<AnimeItem>((x) => IncrementScore(x, -1));
            OnToggleHiddenCommand = new RelayCommand<AnimeItem>((x) => OnToggleHidden(x));
            OnZeroScoresCommand = new RelayCommand(OnZeroScores);
            RestoreOriginalCommand = new RelayCommand(OnRestoreOriginalValues);
            UnhideAllCommand = new RelayCommand(OnUnhideAll);
            SelectedSortOption = SortOptions.AlphabetAsc;
            SelectedFilterOption = FilterOptions.All;
            showHidden = false;
            IsAllRadioChecked = true;
            IsWatchingRadioChecked = false;
            IsCompletedRadioChecked = false;
            IsOnHoldRadioChecked = false;
            IsDroppedRadioChecked = false;
            IsPlanToWatchRadioChecked = false;
        }

        private async void OnImportList()
        {
            var (info, list) = await _parser.OnImportListAsync();
            if (info == null || list == null) return;
            UserStats = new UserStats(info);
            _animeList = list;

            UpdateDisplayedList();
            UpdateStats();
        }
        private async void OnExportList()
        {
            if (UserStats == null || _animeList == null) return;
            await _parser.OnExportListAsync(UserStats.Info, _animeList);
        }

        private void IncrementScore(AnimeItem item, int value)
        {
            var score = int.Parse(item.My_Score) + Math.Sign(value);
            item.My_Score = string.Format("{0}", Math.Max(0, Math.Min(10, score)));
            UpdateStats();
        }

        private void SortAnimeList()
        {
            if (_animeList == null) return;
            switch (SelectedSortOption)
            {
                case SortOptions.AlphabetAsc:
                    _animeList.Sort((a, b) => a.Series_Title.CompareTo(b.Series_Title));
                    break;
                case SortOptions.AlphanetDes:
                    _animeList.Sort((a, b) => b.Series_Title.CompareTo(a.Series_Title));
                    break;
                case SortOptions.ScoreAsc:
                    _animeList.Sort((a, b) => int.Parse(a.My_Score).CompareTo(int.Parse(b.My_Score)));
                    break;
                case SortOptions.ScoreDes:
                    _animeList.Sort((a, b) => int.Parse(b.My_Score).CompareTo(int.Parse(a.My_Score)));
                    break;
                default:
                    break;
            }
            UpdateDisplayedList();
        }
        private void FilterAnimeList()
        {
            if (_animeList == null) return;

            showHidden = selectedFilterOption == FilterOptions.Hidden;

            foreach (var item in _animeList)
            {
                item.IsFiltered = false;

                // apply special filters
                switch (SelectedFilterOption)
                {
                    case FilterOptions.All:
                        // already unset ... do nothing
                        break;
                    case FilterOptions.NotScored:
                        item.IsFiltered = item.My_Score != "0";
                        break;
                    case FilterOptions.Modified:
                        item.IsFiltered = item.Update_On_Import == "0";
                        break;
                    case FilterOptions.Hidden:
                        item.IsFiltered = !item.IsHidden;
                        break;
                    default:
                        break;
                }
                // apply status filters
                // already unset ... do nothing for 'All'
                if (!IsAllRadioChecked)
                {
                    item.IsFiltered = item.IsFiltered || (item.My_Status == "Watching" && !IsWatchingRadioChecked);
                    item.IsFiltered = item.IsFiltered || (item.My_Status == "Completed" && !IsCompletedRadioChecked);
                    item.IsFiltered = item.IsFiltered || (item.My_Status == "On-Hold" && !IsOnHoldRadioChecked);
                    item.IsFiltered = item.IsFiltered || (item.My_Status == "Dropped" && !IsDroppedRadioChecked);
                    item.IsFiltered = item.IsFiltered || (item.My_Status == "Plan to Watch" && !IsPlanToWatchRadioChecked);
                }
            }

            UpdateDisplayedList();
        }

        private void OnToggleHidden(AnimeItem item)
        {
            item.IsHidden = !item.IsHidden;
            UpdateDisplayedList();
        }

        private void UpdateDisplayedList()
        {
            if (_animeList == null) return;
            if (showHidden)
            {
                DisplayAnimeList = new ObservableCollection<AnimeItem>(_animeList.Where(x => !x.IsFiltered && x.IsHidden));
            }
            else
            {
                DisplayAnimeList = new ObservableCollection<AnimeItem>(_animeList.Where(x => !(x.IsFiltered || x.IsHidden)));
            }
        }

        public void UpdateStats(object _0, SelectionChangedEventArgs _1)
        {
            UpdateStats();
        }

        private void UpdateStats()
        {
            if (_animeList == null) return;
            UserStats.UpdateStats(_animeList);
        }

        private void OnZeroScores()
        {
            if (_animeList == null) return;
            foreach (var item in _animeList.Where(x => !(x.IsFiltered || x.IsHidden)))
            {
                item.My_Score = "0";
            }
            UpdateDisplayedList();
            UpdateStats();
        }
        private void OnRestoreOriginalValues()
        {
            if (_animeList == null) return;
            foreach (var item in _animeList.Where(x => !(x.IsFiltered || x.IsHidden)))
            {
                item.RestoreOriginalValues();
            }
            UpdateDisplayedList();
            UpdateStats();
        }

        private void OnUnhideAll()
        {
            if (_animeList == null) return;
            foreach (var item in _animeList.Where(x => !x.IsFiltered))
            {
                item.IsHidden = false;
            }
            UpdateDisplayedList();
            UpdateStats();
        }
    }
}
