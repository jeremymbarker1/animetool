using AnimeTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace AnimeTool.Binding
{
    internal class UserStats : ObservableObject
    {
        private float meanScore;
        public float MeanScore
        {
            get => meanScore;
            set { SetProperty(ref meanScore, value); }
        }

        private int totalWatching;
        public int TotalWatching
        {
            get => totalWatching;
            set { SetProperty(ref totalWatching, value); }
        }
        private int totalCompleted;
        public int TotalCompleted
        {
            get => totalCompleted;
            set { SetProperty(ref totalCompleted, value); }
        }
        private int totalOnHold;
        public int TotalOnHold
        {
            get => totalOnHold;
            set { SetProperty(ref totalOnHold, value); }
        }
        private int totalDropped;
        public int TotalDropped
        {
            get => totalDropped;
            set { SetProperty(ref totalDropped, value); }
        }
        private int totalPlanToWatch;
        public int TotalPlanToWatch
        {
            get => totalPlanToWatch;
            set { SetProperty(ref totalPlanToWatch, value); }
        }

        private int totalEntries;
        public int TotalEntries
        {
            get => totalEntries;
            set { SetProperty(ref totalEntries, value); }
        }

        private int totalScored;
        public int TotalScored
        {
            get => totalScored;
            set { SetProperty(ref totalScored, value); }
        }

        private List<float> actualPercents;
        public List<float> ActualPercents
        {
            get => actualPercents;
            set => SetProperty(ref actualPercents, value);
        }
        private List<int> actualCounts;
        public List<int> ActualCounts
        {
            get => actualCounts;
            set => SetProperty(ref actualCounts, value);
        }


        private readonly MALInfo _info;
        public MALInfo Info { get => _info; }

        public UserStats(MALInfo info)
        {
            _info = info;
            actualPercents = new List<float>(new float[10]);
            actualCounts = new List<int>(new int[10]);
        }

        public void UpdateStats(List<AnimeItem> list)
        {
            UpdateEntryStats(list);
            UpdateScoreTable(list);
        }

        private void UpdateEntryStats(List<AnimeItem> list)
        {
            var mean = 0;
            var watching = 0;
            var completed = 0;
            var onhold = 0;
            var dropped = 0;
            var plantowatch = 0;
            var scored = 0;

            foreach (var item in list)
            {
                if (!(item.My_Score == "0"))
                {
                    mean += int.Parse(item.My_Score);
                    scored++;
                }
                switch (item.My_Status)
                {
                    case "Watching":
                        watching++;
                        break;
                    case "Completed":
                        completed++;
                        break;
                    case "On-Hold":
                        onhold++;
                        break;
                    case "Dropped":
                        dropped++;
                        break;
                    case "Plan to Watch":
                        plantowatch++;
                        break;
                    default:
                        break;
                }
            }
            MeanScore = scored == 0 ? 0 : mean / (float)scored;
            TotalWatching = watching;
            TotalCompleted = completed;
            TotalOnHold = onhold;
            TotalDropped = dropped;
            TotalPlanToWatch = plantowatch;
            TotalEntries = list.Count();
            TotalScored = scored;
        }

        private void UpdateScoreTable(List<AnimeItem> list)
        {
            var total = list.Count;
            var ignore = list.Where(x => int.TryParse(x.My_Score, out int _score) && _score == 0).Count();
            var n = total - ignore;

            var percents = new List<float>(new float[10]);
            var counts = new List<int>(new int[10]);

            for (var i = 0; i < 10; i++)
            {
                counts[i] = list.Where(x => int.TryParse(x.My_Score, out int _score) && _score == i + 1).Count();
            }

            for (var i = 0; i < 10; i++)
            {
                percents[i] = n == 0 ? 0 : counts[i] / (float)n * 100.0f;
            }
            ActualPercents = percents;
            ActualCounts = counts;
        }
    }
}
