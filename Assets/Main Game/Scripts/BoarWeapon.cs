	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarWeapon : MonoBehaviour
{
	public int attackDamage = 20;
	public int enragedAttackDamage = 40;
	public ParticleSystem hitEffect;
	public Vector3 attackOffset;
	public float attackRange = 1f;
    public float enragedAttackRange = 1f;
    public LayerMask attackMask;

	public void Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;
		CameraShake.instance.ShakeCamera(0.75f, 0.2f);
		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		Instantiate(hitEffect, pos, Quaternion.identity);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
		}
	}

	public void EnragedAttack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

        CameraShake.instance.ShakeCamera(0.75f, 0.2f);
        Collider2D colInfo = Physics2D.OverlapCircle(pos, enragedAttackRange, attackMask);
        Instantiate(hitEffect, pos, Quaternion.identity);
        if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().TakeDamage(enragedAttackDamage);
		}
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
