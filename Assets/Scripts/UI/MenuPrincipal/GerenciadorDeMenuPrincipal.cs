using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeMenuPrincipal : MonoBehaviour
{
    [SerializeField]
    private GameObject telaPrincipal;
    [SerializeField]
    private GameObject telaTutorial;
    [SerializeField]
    private GameObject telaCreditos;
    [SerializeField]
    private GameObject telaAtiva;
    // Start is called before the first frame update
    public void TrocarCena(string nomeDaCena)
    {
        GerenciadorDeCenas.TrocarDeCena(nomeDaCena);
    }
    void Start()
    {
        if(telaAtiva==telaPrincipal)
        {
            telaPrincipal.SetActive(true);
        }
        else
        { 
            telaPrincipal.SetActive(false); 
        }
        if(telaAtiva==telaCreditos)
        {
            telaCreditos.SetActive(true);
        }
        else
        {
            telaCreditos.SetActive(false);
        }
        if(telaAtiva==telaTutorial)
        {
            telaTutorial.SetActive(true);
        }
        else
        {
            telaTutorial.SetActive(false);
        }
    }
    public void TrocarTela(GameObject telaAIniciar)
    {
        telaAtiva.SetActive(false);
        telaAtiva= telaAIniciar;
        telaAtiva.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
