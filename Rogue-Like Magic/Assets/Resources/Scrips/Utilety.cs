using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilety : MonoBehaviour
{
    private float delay = 1.0f;
    public void ToStart()
    {
        StartCoroutine(Starter());
    }
    public void ToGame()
    {
        StartCoroutine(Game());
    }
    public void OnApplicationQuit()
    {
        StartCoroutine(Quit());
    }
    private IEnumerator Starter()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Start");
    }
    private IEnumerator Game()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Game");
    }

    private IEnumerator Quit()
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
