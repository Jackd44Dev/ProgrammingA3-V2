using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LavaCosmeticUIPopulator : MonoBehaviour
{
    public GameObject cosmeticSelectorButton;
    public PlayerData playerData;
    public TextMeshProUGUI[] buttonTexts; // allows for changing all of the text when a new cosmetic is selected

    void Start()
    {
       buttonTexts = new TextMeshProUGUI[playerData.lavaMaterials.Length]; // properly set the array's length to the amount of materials in the game
       populateCosmeticList();
    }

    void populateCosmeticList() // uses a prefab to automatically fill out the list of available cosmetics, based on cosmetic data in PlayerData
    {
        for (int i = 0; i < playerData.lavaMaterials.Length; i++) // adds a button for each listed cosmetic
        {
            GameObject newlySpawnedButton = Instantiate(cosmeticSelectorButton, this.gameObject.transform);
            TextMeshProUGUI buttonText = newlySpawnedButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonTexts[i] = buttonText;
            CosmeticChoiceUpdater newButtonClickerScript = newlySpawnedButton.GetComponent<CosmeticChoiceUpdater>();
            newButtonClickerScript.cosmeticControlScript = this; // gives the new button's click script a reference to this script
            newButtonClickerScript.cosmeticID = i;

            if (playerData.coinsRequiredForCosmetics[i] > playerData.coins) // if player doesn't have enough coins to unlock this cosmetic, display it as hidden
            {
                newButtonClickerScript.cosmeticLocked = true;
                newlySpawnedButton.GetComponent<Image>().color = Color.red;
                buttonText.text = "LOCKED!<br>COINS REQUIRED: " + playerData.coinsRequiredForCosmetics[i];
            }
            else // if player has enough coins
            {
                newButtonClickerScript.cosmeticLocked = false;
                if (playerData.selectedCosmetic == i) // if this cosmetic is equipped
                {
                    newlySpawnedButton.GetComponent<Image>().color = new Color(85/255, 1, 0);
                    buttonText.text = playerData.lavaMaterials[i].name + "<br>(SELECTED)";
                    buttonText.color = Color.black;
                }
                else // if cosmetic is unlocked, but not equipped
                {
                    newlySpawnedButton.GetComponent<Image>().color = new Color(85 / 255, 1, 0);
                    buttonText.text = playerData.lavaMaterials[i].name;
                    buttonText.color = Color.black;
                }
            }
        }
    }

    public void updateAllUIText(int newSelected) // called from any button when it has been clicked, clears the "(SELECTED)" text from the old cosmetic that was previously selected
    {
        for (int i = 0; i < playerData.lavaMaterials.Length; i++) // iterates through all button texts
        {
            if (i == newSelected) // if this cosmetic is now selected
            {
                buttonTexts[i].text = playerData.lavaMaterials[i].name + "<br>(SELECTED)";
            }
            else if (playerData.coinsRequiredForCosmetics[i] <= playerData.coins) // if the cosmetic is unlocked but not selected
            {
                buttonTexts[i].text = playerData.lavaMaterials[i].name;
            }
            else // if the cosmetic is not unlocked
            {
                buttonTexts[i].text = "LOCKED!<br>COINS REQUIRED: " + playerData.coinsRequiredForCosmetics[i];
            }
        }

    }
}
