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
            // �wiat�o
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
                itemNameText.text = hit.transform.gameObject.GetComponent<pickableItemBehaviour>().itemName; // pobieramy nazw� przedmiotu by j� wypisa�
                // �eby przedmiot by� pokazywany przed canvasem trzeba odpali� drug� kamer�, �eby najpierw rysowa�o si� �ciemnione t�o, a dopiero p�niej item
                canvas.worldCamera = uiCam; 
                canvas.planeDistance = 2; // trzeba to ustawienie zmieni�, �eby UI by�o w odpowiednim miejscu
                playerInput.hideItem(); // chowamy przedmiot je�li jaki� trzymamy. Bez sensu �eby by� na ekranie podczas inspekcji
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
