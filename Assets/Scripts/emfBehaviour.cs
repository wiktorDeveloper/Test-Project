using UnityEngine;

public class emfBehaviour : MonoBehaviour
{
    [SerializeField] GameObject currentEntity; // W wi�kszej grze mo�e by� ich wi�cej, dlatego ta pozycja by�aby dynamiczna
    [SerializeField] GameObject[] lights; // 5 �wiate� - 5 poziom�w
    float distance;
    void Update()
    {
        distance = Vector3.Distance(transform.position, currentEntity.transform.position);

        // Wygl�da to brzydko z tymi ifami, no ale sposobu lepszego nie ma (albo ja go nie odkry�em)
        // Te odleg�o�ci s� oczywi�cie wpisane bez wi�kszego zastanowienia. To wszystko zale�y co b�dzie pasowa� do gry
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
        // najpierw musimy wy��czy� wszystkie �wiat�a 
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
        // dopiero teraz w��czamy odpowiedni� ilo��
        for (int i = 0; i < amount; i++)
        {
            lights[i].SetActive(true);
        }
    }
}
