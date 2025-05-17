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

    [SerializeField] GameObject mouseButtonHint;
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
        /* Przy większej ilości przedmiotów w scenie gry ciągłe obliczanie dystansu może być kosztowne dla wydajności.
           Tutaj to nie przeszkadza, bo jest to tylko jedno pomieszczenie, ale np. w domu z wieloma pokojami zrobiłbym to inaczej.
           Poustawiałbym "strefy" za pomocą colliderów ustawionych jako triggery. Dystans zacznie być obliczany dopiero wtedy, gry
           gracz znajdzie się w danym pokoju. 
           Można by też zrobić wtedy bez obliczania dystansu i wszystkie ikonki włączą się po wejściu do pokoju, ale to już zależy
           od tego co lepiej będzie pasowaæ do gry. 
        */
        if (itemMode == mode.normal)
        {
            distance = Vector3.Distance(transform.position, player.position);

            if (distance <= 8 && !isOn)
            {
                inspectIcon.SetActive(true);
                isOn = true; // Małe zabezpieczenie, żeby ikona nie włączała się w każdej klatce mimo tego, że jest już włączona
            }
            else if (distance > 8 && isOn)
            {
                inspectIcon.SetActive(false);
                isOn = false; // to samo co wyżej, tylko na odwrót
            }

            // Ciągłe obracanie się ikony w stronę gracza
            inspectIcon.transform.LookAt(player);
        }
        else // Obracanie za pomocą wsad lub strzałek.
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
 
