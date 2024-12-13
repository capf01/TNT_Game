using UnityEngine;
using UnityEngine.UI;

public class WheelHotbarController : MonoBehaviour
{
    public Animator anim;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;

    void Update()
    {
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
                break;
            case 2:
                Debug.Log("Mango");
                break;
            case 3:
                Debug.Log("Zero");
                break;
            case 4:
                Debug.Log("Focus");
                break;
            case 5:
                Debug.Log("Acai");
                break;
        }
    }
}
