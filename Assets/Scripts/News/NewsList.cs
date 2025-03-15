using System.Collections.Generic;
using UnityEngine;

namespace PFAS.News
{
    [CreateAssetMenu(fileName = "News List", menuName = "EventSystem/NewsList")]
    public class NewsList : ScriptableObject
    {
        public List<string> news = new List<string>();
    }
}
