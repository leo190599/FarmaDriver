using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissorDeAudio : MonoBehaviour
{
    [SerializeField]
    private static EmissorDeAudio emissorDeAudioSingleton=null;
    [SerializeField]

    private AudioSource audioSourceSomDeBoost;
    [SerializeField]
    private AudioSource audioSourceSomDeEntregaFeita;

    private void Awake()
    {
        if(emissorDeAudioSingleton==null)
        {
            emissorDeAudioSingleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        if(emissorDeAudioSingleton==this)
        {
            emissorDeAudioSingleton=null;
        }
    }

    public void ExecutarSomDeEntregaFeita()
    {
        audioSourceSomDeEntregaFeita.Play();
    }
    public void ExecutarSomDeBoost()
    {
        audioSourceSomDeBoost.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static bool ExisteUmEmissorDeAudioSingleton=>emissorDeAudioSingleton!=null;
    public static EmissorDeAudio GetEmissorDeAudioSingleton => emissorDeAudioSingleton;
}
