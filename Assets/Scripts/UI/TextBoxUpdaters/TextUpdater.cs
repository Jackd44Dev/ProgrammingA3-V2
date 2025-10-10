using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public string text;
    public PlayerData playerData;
    
    public virtual void updateTextbox()
    {
        textBox.text = text;
    }
}
