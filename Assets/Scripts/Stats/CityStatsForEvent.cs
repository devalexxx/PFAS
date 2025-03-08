namespace PFAS.Stats
{
    [System.Serializable]
    public class CityStatsForEvent
    {
        public CityStats stateToChange;
        public int amount;

        // On peut aussi ajouter un constructeur pour simplifier l'ajout des éléments.
        public CityStatsForEvent(CityStats state, int amount)
        {
            this.stateToChange = state;
            this.amount = amount;
        }
    }
}
