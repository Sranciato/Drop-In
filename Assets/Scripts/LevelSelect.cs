using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void SelectSmall()
    {
        SceneManager.LoadScene("SmallRamp");
    }
    public void SelectMedium()
    {
        SceneManager.LoadScene("Main");
    }
    public void SelectLarge()
    {
        SceneManager.LoadScene("HugeRamp");
    }
}
