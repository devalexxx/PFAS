using System.Linq;
using PFAS.Timer;
using UnityEngine;

namespace PFAS.Propagation
{
    public class PropagationManager : MonoBehaviour
    {
        [SerializeField]
        private CityGraph _graph;

        private void Awake()
        {
            TimerManager.OnTick += _OnTimerTick;
        }

        private void Start()
        {
            _graph.BuildGraph(GameManager.instance.GetAllCities());
        }

        private void _OnTimerTick()
        {
            _graph.Propagate(GameManager.instance.globalStats);
        }
    }

}