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
        /* Przy wi�kszej ilo�ci przedmiot�w w scenie gry ci�g�e obliczanie dystansu mo�e by� kosztowne dla wydajno�ci.
           Tutaj to nie przeszkadza, bo jest to tylko jedno pomieszczenie, ale np. w domu z wieloma pokojami zrobi�bym to inaczej.
           Poustawia�bym "strefy" za pomoc� collider�w ustawionych jako triggery. Dystans zacznie by� obliczany dopiero wtedy, gry
           gracz znajdzie si� w danym pokoju. 
           Mo�na by te� zrobi� wtedy bez obliczania dystansu i wszystkie ikonki w��cz� si� po wej�ciu do pokoju, ale to ju� zale�y
           od tego co lepiej b�dzie pasowa� do gry. 
        */
        if (itemMode == mode.normal)
        {
            distance = Vector3.Distance(transform.position, player.position);

            if (distance <= 8 && !isOn)
            {
                inspectIcon.SetActive(true);
                isOn = true; // Ma�e zabezpieczenie, �eby ikona nie w��cza�a si� w ka�dej klatce mimo tego, �e jest ju� w��czona
            }
            else if (distance > 8 && isOn)
            {
                inspectIcon.SetActive(false);
                isOn = false; // to samo co wy�ej, tylko na odwr�t
            }

            // Ci�g�e obracanie si� ikony w stron� gracza
            inspectIcon.transform.LookAt(player);
        }
        else // Obracanie za pomoc� wsad lub strza�ek.
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
    public void resetItem() // powr�t na poprzednie miejsce
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
 