using System.Collections;
using UnityEngine;

public class EnemyDeadState : State<EnemyStateManager>
{
    public override void EnterState(EnemyStateManager state)
    {
        state.anim.SetTrigger("Dead");

        state.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        state.GetComponent<Collider2D>().enabled = false;
        state.GetComponent <Rigidbody2D>().simulated = false;

        state.StartCoroutine(WaitToDead(state));
    }

    public override void ExitState(EnemyStateManager state) { }

    public override void UpdateState(EnemyStateManager state) { }

    IEnumerator WaitToDead(EnemyStateManager state)
    {
        yield return new WaitForSeconds(1f);
        state.GetComponent<Collider2D>().enabled = true;
        state.GetComponent<Rigidbody2D>().simulated = true;
        state.gameObject.SetActive(false);
    }
}
