using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

     public void MeuMenu()
    {
        SceneManager.LoadScene("JungleAdventureFinal");
    }

    public void Sair()
    {
        Debug.Log("Saiu");
        Application.Quit();
    }
}
