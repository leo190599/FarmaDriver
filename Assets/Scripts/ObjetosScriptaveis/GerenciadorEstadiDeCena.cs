using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="objetosCustomizados/GerenciadorEstadiDeCena",fileName ="novoGerenciadorDeEstadoDeCena")]
public class GerenciadorEstadiDeCena : ScriptableObject
{
    public enum EstadoCena
    {
        jogando=0,
        pausado=1,
        venceu=2,
        perdeu=3
    }
    [SerializeField]
    private EstadoCena estadoCena;

    public UnityAction eventosEntrarEstadoJogando;
    public UnityAction eventosEntrarEstadoPausado;
    public UnityAction eventosEntrarEstadoVenceu;
    public UnityAction eventosEntrarEstadoPerdeu;
    public UnityAction eventosSaidaEstado;


    public void TrocarEstadoCena(EstadoCena novoEstado)
    {
        estadoCena=novoEstado;
        switch (novoEstado)
        {
            case EstadoCena.jogando:
                Time.timeScale = 1;
                if(eventosSaidaEstado!=null)
                {
                    eventosSaidaEstado.Invoke();
                }
                if(eventosEntrarEstadoJogando!=null)
                {
                    eventosEntrarEstadoJogando.Invoke();
                }
                break;
            case EstadoCena.pausado:
                Time.timeScale = 0;
                if (eventosSaidaEstado != null)
                {
                    eventosSaidaEstado.Invoke();
                }
                if (eventosEntrarEstadoPausado!=null)
                {
                    eventosEntrarEstadoPausado.Invoke();
                }
                break;
            case EstadoCena.venceu:
                Time.timeScale = 0;
                if (eventosSaidaEstado != null)
                {
                    eventosSaidaEstado.Invoke();
                }
                if (eventosEntrarEstadoVenceu!=null)
                {
                    eventosEntrarEstadoVenceu.Invoke();
                }
                break;
            case EstadoCena.perdeu:
                Time.timeScale = 0;
                if (eventosSaidaEstado != null)
                {
                    eventosSaidaEstado.Invoke();
                }
                if (eventosEntrarEstadoPerdeu!=null)
                {
                    eventosEntrarEstadoPerdeu.Invoke();
                }
                break;
            default:
                Debug.LogError("utilize um estado valido");
            break;
        }
    }

    public EstadoCena GetEstadoCena => estadoCena;
}
