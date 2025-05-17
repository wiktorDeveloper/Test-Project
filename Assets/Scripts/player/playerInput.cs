using UnityEngine;
using UnityEngine.SceneManagement;
public class playerInput : MonoBehaviour
{
    GameObject eqPanel;

    [HideInInspector] public bool itemInspectionOpen;

    [SerializeField] GameObject thermal;
    [SerializeField] GameObject emfDetector;

    public raycastSystem raycastSystem;
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

        // Wy³¹czanie gry

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
