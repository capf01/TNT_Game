using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WheelHotbarController : MonoBehaviour
{
    public Animator anim;
    public float animationSpeed = 1f; // Velocidade da animação
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;
    [SerializeField] private CollectItem _powerUpManager;
    [SerializeField] private GameObject _menuFirst;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_menuFirst);
    }

    void Update()
    {
        if (Time.timeScale < 1)
        {
            anim.Update(animationSpeed * Time.unscaledDeltaTime);
        }

        //abrir inventario rotatorio "wheel hotbar"
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }
        if (weaponWheelSelected) 
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }
        
        //poderes de cada latinha em cada caso se selecionado
        switch (weaponID)
        {
            case 0:
                selectedItem.sprite = noImage;
                break;
            case 1:
                Debug.Log("Original");
                _powerUpManager.Original();
                break;
            case 2:
                Debug.Log("Mango");
                _powerUpManager.Mango();
                break;
            case 3:
                Debug.Log("Zero");
                _powerUpManager.Zero();
                break;
            case 4:
                Debug.Log("Focus");
                _powerUpManager.PinkLemonade();
                break;
            case 5:
                Debug.Log("Acai");
                _powerUpManager.Acai();
                break;
        }
    }

    public void StartAnimation()
    {
        weaponWheelSelected = !weaponWheelSelected;
    }
}
