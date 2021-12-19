using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool r = true, esc = true;
    private void Update()
    {
        if (r && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (esc && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
