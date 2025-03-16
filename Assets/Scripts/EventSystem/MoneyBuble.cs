using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoneyBuble : MonoBehaviour, IPointerClickHandler
{
    public float lifeTime = 5;
    public float reduceTime = 2;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(Duration());
    }

    public IEnumerator Duration()
    {
        yield return new WaitForSeconds(lifeTime);

        float t_time = 0;
        int t_steps = Mathf.CeilToInt(reduceTime / 0.1f); // Nombre d'itérations
        Vector3 t_scaleStep = transform.localScale / t_steps; // Réduction exacte par step

        while (t_time < reduceTime)
        {
            yield return new WaitForSeconds(0.1f);
            t_time += 0.1f;
            transform.localScale -= t_scaleStep;
        }

        // S'assurer que la taille est bien à zéro après la boucle
        transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }

}
