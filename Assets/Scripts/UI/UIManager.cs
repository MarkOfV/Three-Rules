using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    public GameObject rulesBanner;

    void Update()
    {

        if (rulesBanner == null) return;

        rulesBanner.SetActive(Input.GetKey(KeyCode.Tab));
    }
}