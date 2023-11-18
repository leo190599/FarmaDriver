using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCliente : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private InstanciadorDeCliente instanciadorDeClientePai;

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
