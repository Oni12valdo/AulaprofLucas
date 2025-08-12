using UnityEngine;

public class GhostFade : MonoBehaviour
{
    public float fadeSpeed = 5f; // Velocidade com que o fantasma vai sumir (alpha vai diminuindo)
    private SpriteRenderer sr;
    private Color color;

    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        color = sr.color; // Armazena a cor inicial
    }

    public void Update()
    {
        color.a -= fadeSpeed * Time.deltaTime; // Diminui o valor do alpha ao longo do tempo (fica mais transparente)
        sr.color = color; // Aplica a nova cor (com alpha modificado) no sprite

        // Quando o alpha chegar a 0 ou menos, destrói o fantasma
        if (color.a <= 0)
            Destroy(gameObject);
    }
}