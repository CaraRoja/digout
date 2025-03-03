using UnityEngine;

public class Coin : MonoBehaviour
{
    public ParticleSystem coinParticles; // Adicione um campo para o sistema de partículas

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            // Ativa o sistema de partículas
            if (coinParticles != null)
            {
                ParticleSystem particles = Instantiate(coinParticles, transform.position, Quaternion.identity);
                particles.Play();
                Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);
            }

            Destroy(gameObject);
        }
    }
}
