using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float spawnRate = 0.1f;
    private float timer;
    private SpriteRenderer playerSR;

    void Start()
    {
        playerSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Se passou tempo suficiente, cria um novo fantasma e zera o timer
        if (timer >= spawnRate)
        {
            SpawnGhost();
            timer = 0f;
        }
    }

    void SpawnGhost()
    {
        GameObject ghost = Instantiate(ghostPrefab, transform.position, transform.rotation);
        SpriteRenderer ghostSR = ghost.GetComponent<SpriteRenderer>();
        ghostSR.sprite = playerSR.sprite;
        ghostSR.flipX = playerSR.flipX;

        // Garante que o fantasma fique na mesma layer e atrás do player.
        ghostSR.sortingLayerID = playerSR.sortingLayerID;
        ghostSR.sortingOrder = playerSR.sortingOrder - 1;
    }
}