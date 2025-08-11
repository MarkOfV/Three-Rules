using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePopup : MonoBehaviour
{
    public CanvasGroup group;
    public TMP_Text text;                   // or TMP_Text
    public KeyCode closeKey = KeyCode.Space;

    bool open;
    private int lines = 3;
    void Awake() { Hide(); }

    public void Show(string line)
    {
        text.text = line;
        group.alpha = 1f;
        group.blocksRaycasts = true;
        group.interactable = true;
        Time.timeScale = 0f;
        open = true;
    }

    void Update()
    {
        if (open && Input.GetKeyDown(closeKey))
        {
            lines--;
            if (lines <= 0)
            {
                Hide();
            }
            else if(lines == 2)
            {
                // Optionally, you can change the text to indicate how many lines are left
                text.text = "........... THE DOC WAS RIGHT???";
            }
            else if(lines == 1)
            {
                // Optionally, you can change the text to indicate how many lines are left
                text.text = "You know what? I'm just gonna leave...";
            }
        }
    }

    public void Hide()
    {
        group.alpha = 0f;
        group.blocksRaycasts = false;
        group.interactable = false;
        Time.timeScale = 1f;
        open = false;
    }
}
