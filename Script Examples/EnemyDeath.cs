using System;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{

	void start()
	{
	}

    void update()
    {
    }

    void OnCollisionEnter(Collision col)
    {
        //if a trap is hit, destroy the enemy and take Prim down a level
        if (col.gameObject.CompareTag("Trap"))
        {
            Destroy(gameObject);
            GameObject prim = GameObject.FindWithTag("Player");
            PrimLVController primLevelControl= (PrimLVController)prim.GetComponent(typeof(PrimLVController));
            primLevelControl.decrementLevel();
        }
    }
}