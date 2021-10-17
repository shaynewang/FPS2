using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameoverCanvas;

    private void Start() {
        gameoverCanvas.enabled = false;
    }

    public void DisplayGameoverCanvas() {
        gameoverCanvas.enabled = true;
        Time.timeScale = 0;
        Debug.Log(Cursor.lockState);
        Debug.Log(Cursor.visible);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("after unlock");
        Debug.Log(Cursor.lockState);
        Debug.Log(Cursor.visible);
    }
}
