using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneWait : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitCutscene());
    }

    IEnumerator WaitCutscene()
    {
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene("Bedroom");
    }
}
