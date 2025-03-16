using PFAS.Timer;
using PFAS.UI;
using UnityEngine;

namespace PFAS.News
{
    public class NewsManager : MonoBehaviour
    {
        NewsList _news;
        public ScrollingText newsText;

        public int eventDay = 10;
        int _currentDay = 0;

        void Start()
        {
            _news = Resources.Load<NewsList>("News List");
            TimerManager.OnTick += ChangeDay;
        }

        public void ChangeDay()
        {
            _currentDay++;
            if (_currentDay == eventDay)
            {
                if (Random.value < 0.5f) newsText.SetUpNews(_news.news[Random.Range(0, _news.news.Count)]);
                _currentDay = 0;
            }
        }
    }
}
