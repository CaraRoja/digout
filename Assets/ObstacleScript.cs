using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.U2D.IK;

public class ObstacleScript : MonoBehaviour
{
    protected PlayerManager playerManager;
    public bool solved = false;
    private bool isSolvingProblem = false;

    // Start is called before the first frame update
    void Start()
    {
        // Tenta encontrar o PlayerManager na cena
        playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager == null)
        {
            Debug.LogError("PlayerManager não encontrado na cena!");
        }
    }

    public IEnumerator ObstacleTrigger()
    {
        // Verifica se o playerManager está presente antes de continuar
        if (playerManager == null)
        {
            yield break;  // Sai da corrotina se o playerManager for null
        }

        yield return new WaitForSeconds(playerManager.solvingTime); // Espera o tempo de resolução de problemas

        solved = true;
        isSolved();
        isSolvingProblem = false;
    }

    public void isSolved()
    {
        if (solved)
        {
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            else
            {
                Debug.LogError("Collider2D não encontrado no objeto " + gameObject.name);
            }
        }
    }
}
