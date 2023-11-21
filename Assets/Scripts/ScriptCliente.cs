using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScriptCliente : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float tempoInicialParaEntrega=7;
    [SerializeField]
    private float tempoParaEntrega=5;
    [SerializeField]
    private float velAnimComeco=.2f;
    [SerializeField]
    private float velAnimFim=.5f;
    [SerializeField]
    private InstanciadorDeCliente instanciadorDeClientePai;
    [SerializeField]
    private ScriptCarrinho carrinhoVindoParaEntrega;
    IEnumerator corrotinaPerderEntrega;
    //[SerializeField]
    //private TextMeshProUGUI textoTempo;

    [SerializeField]
    private Image preenchimentoRelogio;

    [SerializeField]
    private Animator animatorCaixinha;

    public void SetCarrinhoParaEntrega(ScriptCarrinho carrinhoParaEntrega)
    {
        carrinhoVindoParaEntrega = carrinhoParaEntrega;
    }

    private void Start()
    {
        corrotinaPerderEntrega = CorrotinaPerderEntrega();
        StartCoroutine(corrotinaPerderEntrega);
        tempoParaEntrega = tempoInicialParaEntrega;
        animatorCaixinha.speed = velAnimComeco;
        if (!GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetListaClentesAtivos.Contains(this.gameObject))
        {
            GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetListaClentesAtivos.Add(this.gameObject);
            //instanciadorDeClientePai.LiberarInstanciador();
        }
        // if (textoTempo != null)
        //{
        //   textoTempo.text = tempoParaEntrega.ToString();
        //}

        if (preenchimentoRelogio!=null)
        {
            preenchimentoRelogio.fillAmount = 1;
        }
    }

    private IEnumerator CorrotinaPerderEntrega()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            tempoParaEntrega--;
            animatorCaixinha.speed = Mathf.Lerp(velAnimFim, velAnimComeco, (tempoParaEntrega / tempoInicialParaEntrega));
            preenchimentoRelogio.fillAmount = (tempoParaEntrega / tempoInicialParaEntrega);
           // Debug.Log((tempoParaEntrega / tempoInicialParaEntrega));
            if (tempoParaEntrega<=0)
            {
                Destroy(gameObject);
                //Debug.Log(carrinhoVindoParaEntrega);
                if (carrinhoVindoParaEntrega!=null)
                {
                    carrinhoVindoParaEntrega.FracassarEntrega();
                }
            }
            else
            {
                /*
                if(textoTempo!=null)
                {
                    textoTempo.text=tempoParaEntrega.ToString();
                }
                */
            }
        }

    }
    public void SetInstanciadorDeClientePai(InstanciadorDeCliente instanciadorDeClientePai)
    {
        this.instanciadorDeClientePai = instanciadorDeClientePai;
    }
    private void OnDestroy()
    {
        if (GerenciadorDeCarrinhos.ExisteUmGerenciadorDeCarrinhos)
        {
            if (GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetListaClentesAtivos.Contains(this.gameObject))
            {
                GerenciadorDeCarrinhos.GetGerenciadorDeCarrinhosSingleton.GetListaClentesAtivos.Remove(this.gameObject);
                //instanciadorDeClientePai.LiberarInstanciador();
            }
        }
        if (instanciadorDeClientePai != null)
        {
            instanciadorDeClientePai.LiberarInstanciador();
        }
    }
}
