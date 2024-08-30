using UnityEngine;
using TMPro;

public class Escape : MonoBehaviour
{
    public GameObject escapeText;
    private float holdTime = 3f;
    private float holdTimer = 0f;
    private bool isHolding = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            holdTimer = holdTime;
            isHolding = true;
            escapeText.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isHolding = false;
            escapeText.SetActive(false);
        }

        if (isHolding)
        {
            holdTimer = holdTimer - Time.unscaledDeltaTime;
            if (holdTimer <= 0)
            {
                Application.Quit();
            }
        }
    }
}
