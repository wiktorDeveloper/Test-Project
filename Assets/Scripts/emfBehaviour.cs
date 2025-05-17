using UnityEngine;

public class emfBehaviour : MonoBehaviour
{
    [SerializeField] GameObject currentEntity; // W wiêkszej grze mo¿e byæ ich wiêcej, dlatego ta pozycja by³aby dynamiczna
    [SerializeField] GameObject[] lights; // 5 œwiate³ - 5 poziomów
    float distance;
    void Update()
    {
        distance = Vector3.Distance(transform.position, currentEntity.transform.position);

        // Wygl¹da to brzydko z tymi ifami, no ale sposobu lepszego nie ma (albo ja go nie odkry³em)
        // Te odleg³oœci s¹ oczywiœcie wpisane bez wiêkszego zastanowienia. To wszystko zale¿y co bêdzie pasowaæ do gry
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
        // najpierw musimy wy³¹czyæ wszystkie œwiat³a 
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
        // dopiero teraz w³¹czamy odpowiedni¹ iloœæ
        for (int i = 0; i < amount; i++)
        {
            lights[i].SetActive(true);
        }
    }
}
