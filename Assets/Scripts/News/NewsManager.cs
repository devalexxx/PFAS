using PFAS.UI;
using UnityEngine;

namespace PFAS.News
{
    public class NewsManager : MonoBehaviour
    {
        NewsList _news;
        public ScrollingText newsText;
        
        void Start()
        {
            _news = Resources.Load<NewsList>("News List");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                newsText.SetUpNews(_news.news[Random.Range(0, _news.news.Count)]);
            }
        }
    }
}
