using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeMenuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update
    public void TrocarCena(string nomeDaCena)
    {
        GerenciadorDeCenas.TrocarDeCena(nomeDaCena);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
