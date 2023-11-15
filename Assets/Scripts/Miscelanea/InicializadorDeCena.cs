using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicializadorDeCena : MonoBehaviour
{
    [SerializeField]
    private GerenciadorEstadiDeCena gerenciadorEstadiDeCena;
    // Start is called before the first frame update
    void Start()
    {
        gerenciadorEstadiDeCena.TrocarEstadoCena(GerenciadorEstadiDeCena.EstadoCena.jogando);
    }
}
