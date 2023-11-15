using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorCarrinhosLivresLabel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI texto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        AtualizarTexto();
        if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.eventosAtualizacaoDeCarrinhosLivres += AtualizarTexto;
        }
    }
    private void OnDisable()
    {
        if (GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.eventosAtualizacaoDeCarrinhosLivres -= AtualizarTexto;
        }
    }
    public void AtualizarTexto()
    {
        if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            texto.text=GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetQuantidadeDeCarrinhosLivres.ToString();
        }
    }
}
