using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeSubtelaTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject[] subtelas;
    [SerializeField]
    private int indexSubtelaAtual=0;
    // Start is called before the first frame update
    void OnEnable()
    {
        indexSubtelaAtual = 0;
        if(subtelas.Length>1)
        {
            //Debug.Log(subtelas.Length);
            subtelas[0].SetActive(true);
            for(int i=1;i<subtelas.Length;i++)
            {
                Debug.Log(i);
                subtelas[i].SetActive(false);
            }
        }

    }
    public void AvancarTela()
    {
        if((indexSubtelaAtual+1)<subtelas.Length)
        {
            subtelas[indexSubtelaAtual].SetActive(false);
            indexSubtelaAtual++;
            subtelas[indexSubtelaAtual].SetActive(true);
        }
    }
    public void RegredirTela()
    {
        if(indexSubtelaAtual-1>=0)
        {
            subtelas[indexSubtelaAtual].SetActive(false);
            indexSubtelaAtual--;
            subtelas[indexSubtelaAtual].SetActive(true);
        }
    }
}
