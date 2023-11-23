using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorDeCaixasAPerder : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI texto;
    private void Start()
    {
        AtualizarTexto();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(GerenciadorDeClientesScirpt.ExisteUmGerenciadorDeClientesSingleton)
        {
            GerenciadorDeClientesScirpt.GetGerenciadorDeClientesSingleton.eventosAlteracaoDeCaixasAPerder += AtualizarTexto;
        }
    }
    private void OnDisable()
    {
        if (GerenciadorDeClientesScirpt.ExisteUmGerenciadorDeClientesSingleton)
        {
            GerenciadorDeClientesScirpt.GetGerenciadorDeClientesSingleton.eventosAlteracaoDeCaixasAPerder -= AtualizarTexto;
        }
    }
    public void AtualizarTexto()
    {
        if(GerenciadorDeClientesScirpt.GetGerenciadorDeClientesSingleton)
        {
            texto.text = GerenciadorDeClientesScirpt.GetGerenciadorDeClientesSingleton.GetQuantidadeDeCaixasAPerder.ToString();
        }
    }
}
