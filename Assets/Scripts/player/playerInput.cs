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
        // Wyłączanie inspekcji przedmiotu
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

        // EMF

        if (Input.GetKeyDown(KeyCode.Alpha2) && !itemInspectionOpen)
        {
            itemEquip(emfDetector);
        }

        // Chowanie

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hideItem();
        }

        // Wyłączanie gry

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void itemEquip(GameObject item)
    {
        if (currentItem != null) // sprawdzenie czy gracz w ogóle ma coś w ręcę
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
