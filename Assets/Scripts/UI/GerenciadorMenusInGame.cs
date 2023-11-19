using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorMenusInGame : MonoBehaviour
{
    [SerializeField]
    private GerenciadorEstadiDeCena gerenciadorEstadiDeCena;
    [SerializeField]
    private GameObject menuJogando;
    [SerializeField] 
    private GameObject menuPause;
    [SerializeField]
    private GameObject menuVenceu;
    [SerializeField]
    private GameObject menuPerdeu;
    private void Start()
    {
        TrocarMenu();
    }
    public void TrocarCena(string nomeDaCena)
    {
        GerenciadorDeCenas.TrocarDeCena(nomeDaCena);
    }
    public void RecarregarCena()
    {
        GerenciadorDeCenas.RecarregarCena();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        gerenciadorEstadiDeCena.eventosEntrarEstadoJogando += TrocarMenu;
        gerenciadorEstadiDeCena.eventosEntrarEstadoPausado += TrocarMenu;
        gerenciadorEstadiDeCena.eventosEntrarEstadoVenceu += TrocarMenu;
        gerenciadorEstadiDeCena.eventosEntrarEstadoPerdeu += TrocarMenu;
    }
    private void OnDisable()
    {
        gerenciadorEstadiDeCena.eventosEntrarEstadoJogando -= TrocarMenu;
        gerenciadorEstadiDeCena.eventosEntrarEstadoPausado -= TrocarMenu;
        gerenciadorEstadiDeCena.eventosEntrarEstadoVenceu -= TrocarMenu;
        gerenciadorEstadiDeCena.eventosEntrarEstadoPerdeu -= TrocarMenu;
    }

    public void TrocarEstadoDeJogo(int novoEstado)
    {
        switch(novoEstado)
        {
            case 0:
                gerenciadorEstadiDeCena.TrocarEstadoCena(GerenciadorEstadiDeCena.EstadoCena.jogando); 
                break;
            case 1:
                gerenciadorEstadiDeCena.TrocarEstadoCena(GerenciadorEstadiDeCena.EstadoCena.pausado);
                break;
            case 2:
                gerenciadorEstadiDeCena.TrocarEstadoCena(GerenciadorEstadiDeCena.EstadoCena.venceu); 
                break;
            case 3:
                gerenciadorEstadiDeCena.TrocarEstadoCena(GerenciadorEstadiDeCena.EstadoCena.perdeu);
                break;
        }
    }

    public void TrocarMenu()
    {
        menuJogando.SetActive(false);
        menuPause.SetActive(false);
        menuVenceu.SetActive(false);
        menuPerdeu.SetActive(false);

        switch (gerenciadorEstadiDeCena.GetEstadoCena)
        {
            case GerenciadorEstadiDeCena.EstadoCena.jogando:
                menuJogando.SetActive(true);
                break;
            case GerenciadorEstadiDeCena.EstadoCena.pausado: 
                menuPause.SetActive(true);
                break;
            case GerenciadorEstadiDeCena.EstadoCena.venceu:
                menuVenceu.SetActive(true);
                break;
            case GerenciadorEstadiDeCena.EstadoCena.perdeu:
                menuPerdeu.SetActive(true);
                break;
        }

    }
}
