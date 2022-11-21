using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string TargetRoom = "SceneDev";
    public Button Button;
    //public Button ButtonQuit;

    public void OnClickPlay() {
        SceneManager.LoadScene(TargetRoom);
    }

    public void OnClickQuit() {
        Application.Quit();
    }

    public void OnHovered() {
        Button.transform.localScale = new Vector3(1.1f, 1.1f, 0);
    }

    public void OnNotHovered()
    {
        Button.transform.localScale = new Vector3(1, 1, 0);
    }
}
