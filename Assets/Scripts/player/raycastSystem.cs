using UnityEngine;
using TMPro;
public class raycastSystem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform camObj;
    [SerializeField] Camera uiCam;
    [SerializeField] Canvas canvas;

    float range = 5f;

    [Header("Scripts")]
    public movement movement;
    public cameraBehaviour cameraBehaviour;
    public playerInput playerInput;
    public GameObject itemMenu;
    public Animator itemMenuAnim;

    [Header("UI")]
    [SerializeField] TMP_Text itemNameText;
    [SerializeField] GameObject crosshair;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            raycastShoot();
        }
    }
    void raycastShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(camObj.position, camObj.forward, out hit, range))
        {
            // œwiat³o
            if (hit.transform.gameObject.GetComponent<lightSwitch>() != null)
            {
                hit.transform.gameObject.GetComponent<lightSwitch>().lightTrigger();
            }

            // itemy
            if (hit.transform.gameObject.GetComponent<pickableItemBehaviour>() != null)
            {
                itemInspect(false);
                itemMenuAnim.Play("backgroundFadeIn");
                Cursor.lockState = CursorLockMode.None;
                playerInput.itemInspectionOpen = true;
                playerInput.currentItemInInspection = hit.transform.gameObject;
                hit.transform.gameObject.GetComponent<pickableItemBehaviour>().inspect();
                itemNameText.text = hit.transform.gameObject.GetComponent<pickableItemBehaviour>().itemName; // pobieramy nazwê przedmiotu by j¹ wypisaæ
                // ¿eby przedmiot by³ pokazywany przed canvasem trzeba odpaliæ drug¹ kamerê, ¿eby najpierw rysowa³o siê œciemnione t³o, a dopiero póŸniej item
                canvas.worldCamera = uiCam; 
                canvas.planeDistance = 2; // trzeba to ustawienie zmieniæ, ¿eby UI by³o w odpowiednim miejscu
                playerInput.hideItem(); // chowamy przedmiot jeœli jakiœ trzymamy. Bez sensu ¿eby by³ na ekranie podczas inspekcji
            }
        }
    }
    public void itemInspect(bool status)
    {
        itemMenu.SetActive(!status);
        Cursor.visible = !status;
        movement.enabled = status;
        cameraBehaviour.enabled = status;
        crosshair.SetActive(status);
        uiCam.enabled = !status;
    }
    public void resetInspection()
    {
        canvas.worldCamera = null;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
