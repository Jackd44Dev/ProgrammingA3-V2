using TMPro;
using UnityEngine;

public class CosmeticChoiceUpdater : MonoBehaviour
{
    public bool cosmeticLocked = true;
    public int cosmeticID;
    public LavaCosmeticUIPopulator cosmeticControlScript;
    public PlayerData playerData;
    public TextMeshProUGUI buttonText;

    public void updateCosmeticChoice()
    {
        if (cosmeticLocked) { return; } // don't allow selecting this cosmetic if it's not unlocked!
        if (playerData.selectedCosmetic == cosmeticID) { return; } // don't need to refresh the UI list if this cosmetic is already selected (don't want to have bad performance by constantly refreshing UI for no reason)
        playerData.selectedCosmetic = cosmeticID;
        cosmeticControlScript.updateAllUIText(cosmeticID);
    }
}
