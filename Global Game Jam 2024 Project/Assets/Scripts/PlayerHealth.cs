using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    private int currentLives;

    public Transform respawnPointPlayer1;
    public Transform respawnPointPlayer2;

    private PlayerController playerController;

    private void Awake()
    {
        currentLives = lives;
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            Respawn();
            TakeLife(1);
        }
    }

    public void TakeLife(int damage)
    {
        currentLives -= damage;

        if (currentLives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Respawn()
    {
        playerController.ResetMovement();

        if (playerController.isPlayer1)
        {
            transform.position = respawnPointPlayer1.position;
        }
        else
        {
            transform.position = respawnPointPlayer2.position;
        }
    }
}
