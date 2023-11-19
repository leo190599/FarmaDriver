using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptCliente : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float tempoParaEntrega=5;
    [SerializeField]
    private InstanciadorDeCliente instanciadorDeClientePai;
    [SerializeField]
    private ScriptCarrinho carrinhoVindoParaEntrega;
    IEnumerator corrotinaPerderEntrega;
    [SerializeField]
    private TextMeshProUGUI textoTempo;

    public void SetCarrinhoParaEntrega(ScriptCarrinho carrinhoParaEntrega)
    {
        carrinhoVindoParaEntrega = carrinhoParaEntrega;
    }

    private void Start()
    {
        corrotinaPerderEntrega = CorrotinaPerderEntrega();
        StartCoroutine(corrotinaPerderEntrega);
        if (textoTempo != null)
        {
            textoTempo.text = tempoParaEntrega.ToString();
        }
    }

    private IEnumerator CorrotinaPerderEntrega()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            tempoParaEntrega--;
            if(tempoParaEntrega<=0)
            {
                if (GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetListaClentesAtivos.Contains(this.gameObject))
                {
                    GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetListaClentesAtivos.Remove(this.gameObject);
                    instanciadorDeClientePai.LiberarInstanciador();
                }
                Destroy(gameObject);
                Debug.Log(carrinhoVindoParaEntrega);
                if (carrinhoVindoParaEntrega!=null)
                {
                    carrinhoVindoParaEntrega.FracassarEntrega();
                }
            }
            else
            {
                if(textoTempo!=null)
                {
                    textoTempo.text=tempoParaEntrega.ToString();
                }
            }
        }

    }
    public void SetInstanciadorDeClientePai(InstanciadorDeCliente instanciadorDeClientePai)
    {
        this.instanciadorDeClientePai = instanciadorDeClientePai;
    }
    private void OnDestroy()
    {
        if(GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            if(GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetListaClentesAtivos.Contains(this.gameObject))
            {
                GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetListaClentesAtivos.Remove(this.gameObject);
                instanciadorDeClientePai.LiberarInstanciador();
            }
        }
    }
}
