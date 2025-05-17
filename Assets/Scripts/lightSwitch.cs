using Unity.VisualScripting;
using UnityEngine;

public class lightSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] lightObj; // tablica bo czêsto mo¿e byæ tak, ¿e jeden w³¹cznik obs³uguje wiêcej ni¿ jedno Ÿród³o œwiat³a
    [SerializeField] Transform player; 
    [SerializeField] GameObject inspectIcon;
    bool isInspectIconOn;
    [SerializeField] bool isLightOn;
    float distance; 
    public GameObject mouseButtonHint;
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);

        if (distance <= 8 && !isInspectIconOn)
        {
            inspectIcon.SetActive(true);
            isInspectIconOn = true; // Zabezpieczenie (to samo co w pickableItemBehaviour)
        }
        else if (distance > 8 && isInspectIconOn)
        {
            inspectIcon.SetActive(false);
            isInspectIconOn = false; // to samo co wy¿ej, tylko na odwrót
        }
    }
    private void OnMouseOver() 
    {
        if (distance <= 5f)
        {
            mouseButtonHint.SetActive(true);
           
        }
    }
    private void OnMouseExit()
    {
        mouseButtonHint.SetActive(false);
    }
    public void lightTrigger()
    {
        if (Input.GetMouseButtonDown(0) && !isLightOn)
        {
            for (int i = 0; i < lightObj.Length; i++) // zwyk³a pêtla for, która obs³uguje wszystkie œwiat³a w tablicy
            {
                lightObj[i].SetActive(true);
            }
            isLightOn = true;
        }
        else if (Input.GetMouseButtonDown(0) && isLightOn)
        {
            for (int i = 0; i < lightObj.Length; i++)
            {
                lightObj[i].SetActive(false);
            }
            isLightOn = false;
        }
    }
}
