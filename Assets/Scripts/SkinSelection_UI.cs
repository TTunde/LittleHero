using TMPro;
using UnityEngine;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] GameObject buyBtn;
    [SerializeField] GameObject equipBtn;
    [SerializeField] int skinId;
    [SerializeField] Animator myAnimator;


    public bool[] skinPurchased;
    public int[] priceForSkin;


    private void SkinSetup()
    {
        equipBtn.SetActive(skinPurchased[skinId]);
        buyBtn.SetActive(!skinPurchased[skinId]);

        if (!skinPurchased[skinId])
            buyBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + priceForSkin[skinId];

        myAnimator.SetInteger("skinId", skinId);

    }
    public void NextSkin()
    {
        skinId++;

        if (skinId > 1)
            skinId = 0;
        SkinSetup();
    }

    public void PerviousSkin()
    {
        skinId--;

        if (skinId < 0)
            skinId = 1;
        SkinSetup();
    }


    public void Buy()
    {
        skinPurchased[skinId] = true;
        SkinSetup();
    }
    public void Equip()
    {
        Debug.Log("Felvettem");
    }
}
