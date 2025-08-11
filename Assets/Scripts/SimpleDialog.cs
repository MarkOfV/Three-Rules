using UnityEngine;
using UnityEngine.UI;         
using UnityEngine.SceneManagement;

public class SimpleDialogue : MonoBehaviour
{
    [TextArea] public string[] lines;
    public TMPro.TMP_Text text;                      
    public KeyCode advanceKey = KeyCode.Space;
    public string nextSceneName = "MainScene";

    int idx = -1;

    void Start() { Next(); }               
    void Update() { if (Input.GetKeyDown(advanceKey)) Next(); }

    void Next()
    {
        idx++;
        if (idx >= lines.Length)
        {
            SceneManager.LoadScene(nextSceneName);
            return;
        }
        text.text = lines[idx];
    }
}
