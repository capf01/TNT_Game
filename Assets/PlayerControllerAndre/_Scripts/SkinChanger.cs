using UnityEngine;
using UnityEngine.U2D.Animation;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private SpriteLibraryAsset whiteAcai;
    [SerializeField] private SpriteLibraryAsset whiteMango;
    [SerializeField] private SpriteLibraryAsset whiteOriginal;
    [SerializeField] private SpriteLibraryAsset whitePinkLemonade;
    [SerializeField] private SpriteLibraryAsset whiteZero;
    [SerializeField] private SpriteLibraryAsset blackAcai;
    [SerializeField] private SpriteLibraryAsset blackMango;
    [SerializeField] private SpriteLibraryAsset blackOriginal;
    [SerializeField] private SpriteLibraryAsset blackPinkLemonade;
    [SerializeField] private SpriteLibraryAsset blackZero;

    [SerializeField] private SpriteLibraryAsset girlWhiteAcai;
    [SerializeField] private SpriteLibraryAsset girlWhiteMango;
    [SerializeField] private SpriteLibraryAsset girlWhiteOriginal;
    [SerializeField] private SpriteLibraryAsset girlWhitePinkLemonade;
    [SerializeField] private SpriteLibraryAsset girlWhiteZero;
    [SerializeField] private SpriteLibraryAsset girlBlackAcai;
    [SerializeField] private SpriteLibraryAsset girlBlackMango;
    [SerializeField] private SpriteLibraryAsset girlBlackOriginal;
    [SerializeField] private SpriteLibraryAsset girlBlackPinkLemonade;
    [SerializeField] private SpriteLibraryAsset girlBlackZero;

    [SerializeField] private SpriteLibrary spriteLibrary; // Referência ao Sprite Library do player

    public bool isWhite;
    public bool isMan;

    void Start()
    {
        // Obtém o componente Sprite Library do player
        spriteLibrary = GetComponent<SpriteLibrary>();

        if (spriteLibrary == null)
        {
            Debug.LogError("Sprite Library não encontrado no GameObject!");
        }
    }

    public void ApplyLibrary(int library)
    {
        if (spriteLibrary != null)
        {
            if (isMan)
            {
                if (!isWhite)
                {
                    if (library == 1) spriteLibrary.spriteLibraryAsset = blackAcai;
                    else if (library == 2) spriteLibrary.spriteLibraryAsset = blackMango;
                    else if (library == 3) spriteLibrary.spriteLibraryAsset = blackOriginal;
                    else if (library == 4) spriteLibrary.spriteLibraryAsset = blackPinkLemonade;
                    else if (library == 5) spriteLibrary.spriteLibraryAsset = blackZero;
                }

                else
                {
                    if (library == 1) spriteLibrary.spriteLibraryAsset = whiteAcai;
                    else if (library == 2) spriteLibrary.spriteLibraryAsset = whiteMango;
                    else if (library == 3) spriteLibrary.spriteLibraryAsset = whiteOriginal;
                    else if (library == 4) spriteLibrary.spriteLibraryAsset = whitePinkLemonade;
                    else if (library == 5) spriteLibrary.spriteLibraryAsset = whiteZero;

                }
            }

            else
            {
                if (!isWhite)
                {
                    if (library == 1) spriteLibrary.spriteLibraryAsset = girlBlackAcai;
                    else if (library == 2) spriteLibrary.spriteLibraryAsset = girlBlackMango;
                    else if (library == 3) spriteLibrary.spriteLibraryAsset = girlBlackOriginal;
                    else if (library == 4) spriteLibrary.spriteLibraryAsset = girlBlackPinkLemonade;
                    else if (library == 5) spriteLibrary.spriteLibraryAsset = girlBlackZero;
                }

                else
                {
                    if (library == 1) spriteLibrary.spriteLibraryAsset = girlWhiteAcai;
                    else if (library == 2) spriteLibrary.spriteLibraryAsset = girlWhiteMango;
                    else if (library == 3) spriteLibrary.spriteLibraryAsset = girlWhiteOriginal;
                    else if (library == 4) spriteLibrary.spriteLibraryAsset = girlWhitePinkLemonade;
                    else if (library == 5) spriteLibrary.spriteLibraryAsset = girlWhiteZero;

                }
            }
            spriteLibrary.RefreshSpriteResolvers(); // Atualiza todos os Sprite Resolvers
        }
    }
}
