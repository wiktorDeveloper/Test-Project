using UnityEngine;

public class playerInput : MonoBehaviour
{
    GameObject eqPanel;

    [HideInInspector] public bool itemInspectionOpen;
    public raycastSystem raycastSystem;

    [SerializeField] GameObject thermal;
    [SerializeField] GameObject emfDetector;
    public GameObject currentItemInInspection;

    GameObject currentItem;
    void Update()
    {
        // Wy³¹czanie inspekcji przedmiotu
        if (Input.GetKeyDown(KeyCode.Tab) && itemInspectionOpen)
        {
            raycastSystem.itemInspect(true);
            raycastSystem.resetInspection();
            itemInspectionOpen = false;
            currentItemInInspection.GetComponent<pickableItemBehaviour>().resetItem();
            currentItemInInspection = null;
        }

        // Termowizyjna kamera

        if (Input.GetKeyDown(KeyCode.Alpha1) && !itemInspectionOpen)
        {
            itemEquip(thermal);
        }

        // EMf

        if (Input.GetKeyDown(KeyCode.Alpha2) && !itemInspectionOpen)
        {
            itemEquip(emfDetector);
        }

        // Chowanie

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hideItem();
        }
    }
    void itemEquip(GameObject item)
    {
        if (currentItem != null) // sprawdzenie czy gracz w ogóle ma coœ w rêce
        {
            currentItem.SetActive(false);
            currentItem = null;
        }
        item.SetActive(true);
        currentItem = item;
    }
    public void hideItem()
    {
        if (currentItem != null)
        {
            currentItem.SetActive(false);
            currentItem = null;
        }
    }
}
