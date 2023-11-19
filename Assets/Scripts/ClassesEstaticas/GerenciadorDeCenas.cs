using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class GerenciadorDeCenas
{
    public static void TrocarDeCena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }
    public static void RecarregarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
