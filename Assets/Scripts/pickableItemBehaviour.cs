using UnityEngine;

public class pickableItemBehaviour : MonoBehaviour
{
    float distance;
    [SerializeField] Transform player;
    [SerializeField] GameObject inspectIcon;
    [SerializeField] Transform inspectParent;

    Vector3 startPosition;
    Quaternion startRotation;

    bool isOn;
    mode itemMode;

    public GameObject mouseButtonHint;
    float rotationSpeed = 90f;

    public string itemName;
    enum mode
    {
        normal,
        inspect
    }
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
    void Update()
    {
        /* Przy wiêkszej iloœci przedmiotów w scenie gry ci¹g³e obliczanie dystansu mo¿e byæ kosztowne dla wydajnoœci.
           Tutaj to nie przeszkadza, bo jest to tylko jedno pomieszczenie, ale np. w domu z wieloma pokojami zrobi³bym to inaczej.
           Poustawia³bym "strefy" za pomoc¹ colliderów ustawionych jako triggery. Dystans zacznie byæ obliczany dopiero wtedy, gry
           gracz znajdzie siê w danym pokoju. 
           Mo¿na by te¿ zrobiæ wtedy bez obliczania dystansu i wszystkie ikonki w³¹cz¹ siê po wejœciu do pokoju, ale to ju¿ zale¿y
           od tego co lepiej bêdzie pasowaæ do gry. 
        */
        if (itemMode == mode.normal)
        {
            distance = Vector3.Distance(transform.position, player.position);

            if (distance <= 8 && !isOn)
            {
                inspectIcon.SetActive(true);
                isOn = true; // Ma³e zabezpieczenie, ¿eby ikona nie w³¹cza³a siê w ka¿dej klatce mimo tego, ¿e jest ju¿ w³¹czona
            }
            else if (distance > 8 && isOn)
            {
                inspectIcon.SetActive(false);
                isOn = false; // to samo co wy¿ej, tylko na odwrót
            }

            // Ci¹g³e obracanie siê ikony w stronê gracza
            inspectIcon.transform.LookAt(player);
        }
        else // Obracanie za pomoc¹ wsad lub strza³ek.
        {
            float horizontal = Input.GetAxis("Horizontal"); 
            float vertical = Input.GetAxis("Vertical");

            transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, -vertical * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
    public void inspect() // odpalamy tryb inspekcji
    {
        inspectIcon.SetActive(false);
        transform.parent = inspectParent;
        transform.position = inspectParent.position;
        transform.rotation = inspectParent.rotation;
        itemMode = mode.inspect;
        mouseButtonHint.SetActive(false);
    }
    public void resetItem() // powrót na poprzednie miejsce
    {
        transform.parent = null;
        transform.position = startPosition;
        transform.rotation = startRotation;
        inspectIcon.SetActive(true); 
        itemMode = mode.normal;
    }
    private void OnMouseOver() 
    {
        if (distance <= 5f && itemMode == mode.normal)
        {
            mouseButtonHint.SetActive(true);   
        }
    }
    private void OnMouseExit()
    {
        mouseButtonHint.SetActive(false);
    }
}
 