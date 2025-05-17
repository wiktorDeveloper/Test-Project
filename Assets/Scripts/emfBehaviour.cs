using UnityEngine;

public class emfBehaviour : MonoBehaviour
{
    [SerializeField] GameObject currentEntity; // W większej grze może być ich więcej, dlatego ta pozycja byłaby dynamiczna
    [SerializeField] GameObject[] lights; // 5 świateł - 5 poziomów 
    float distance;
    void Update()
    {
        distance = Vector3.Distance(transform.position, currentEntity.transform.position);

        // Wygląda to brzydko z tymi ifami, no ale sposobu lepszego nie ma (albo ja go nie odkryłem)
        // Te odległości są oczywiście wpisane bez większego zastanowienia. To wszystko zależy co będzie pasować do gry
        if (distance > 25)
        {
            lightsMethod(0);
        }
        else if (distance <= 25 && distance >= 20)
        {
            lightsMethod(1);
        }
        else if (distance < 20 && distance >= 15)
        {
            lightsMethod(2);
        }
        else if (distance < 15 && distance >= 10)
        {
            lightsMethod(3);
        }
        else if (distance < 10 && distance >= 5)
        {
            lightsMethod(4);
        }
        else if (distance < 5)
        {
            lightsMethod(5);
        }
    }
    void lightsMethod(int amount)
    {
        // najpierw musimy wyłączyć wszystkie światła 
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
        // dopiero teraz włączamy odpowiednią ilość
        for (int i = 0; i < amount; i++)
        {
            lights[i].SetActive(true);
        }
    }
}
