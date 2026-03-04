using UnityEngine;
using UnityEngine.Events;

public class DamageHandler : MonoBehaviour
{
	[SerializeField] private float damage = 1;
	[SerializeReference, SubclassSelector] private BaseDamageModifier modifier;
	[SerializeField, NaughtyAttributes.EnumFlags] private Health.Type affects = Health.Type.PLAYER;
	[SerializeField] private UnityEvent<Health> onHit = new UnityEvent<Health>();

	private void OnValidate(){

		//	Set up default damage modifier
		if(modifier == null){

			//	Variable: Player projectile component
			BasePlayerProjectile playerProj = gameObject.GetComponent<BasePlayerProjectile>();

			//	Determine best modifier type
			if(playerProj == null) modifier = new BaseDamageModifier.Enemy();
			else modifier = new BaseDamageModifier.PlayerProjectile(playerProj);

		}

	}

    void OnTriggerEnter2D(Collider2D collider)
    {

        Health healthOfCollider = collider.GetComponent<Health>();

        if ((healthOfCollider != null) && ((healthOfCollider.getType() & affects) != Health.Type.NONE))
        {
            healthOfCollider.TakeDamage(damage * modifier.getDamageModifier());

			//	Call `onClick` event on `healthOfCollider`
			onHit.Invoke(healthOfCollider);

        }

    }

	public void destroyProjectile(Health health){
		ObjectDestroyer.destroy(gameObject, ObjectDestroyer.Cause.PROJECTILE_EXPIRE);
	}

}
