using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BtnCompra : MonoBehaviour
{
    [SerializeField]
    private Button botao;
    [SerializeField]
    private TextMeshProUGUI textoBotao;
    private void Start()
    {
        //Debug.Log(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos);
        if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.eventosCompraDeCarrinhos += AtualizarTextoBotao;
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.eventosCompraFinalDeCarrinhos += DesativarBotao;
        }
        AtualizarTextoBotao();
        botao.onClick.AddListener(EfetuarCompra);
    }
    private void OnDisable()
    {
        if (GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.eventosCompraDeCarrinhos -= AtualizarTextoBotao;
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.eventosCompraFinalDeCarrinhos += DesativarBotao;
        }
        botao.onClick.RemoveAllListeners();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DesativarBotao()
    {
        gameObject.SetActive(false);
    }

    public void EfetuarCompra()
    {
        if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.comprarCarrinho();
        }
    }

    public void AtualizarTextoBotao()
    {
        textoBotao.text = GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetPrecoCarrinho.ToString();
    }
}
