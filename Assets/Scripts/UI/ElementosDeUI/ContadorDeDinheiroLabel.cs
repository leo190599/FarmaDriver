using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorDeDinheiroLabel : MonoBehaviour
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
        if (GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.eventosAtualizacaoDeDinheiro += AtualizarTexto;
        }
    }
    private void OnDisable()
    {
        if (GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.eventosAtualizacaoDeDinheiro -= AtualizarTexto;
        }
    }
    public void AtualizarTexto()
    {
        if (GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            texto.text = GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetQuantidadeDeDinheiro.ToString();
        }
    }
}
