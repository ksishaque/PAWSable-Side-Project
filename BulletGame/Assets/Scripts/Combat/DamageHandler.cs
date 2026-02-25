using UnityEngine;

public class DamageHandler : MonoBehaviour
{
	[SerializeField] private float damage = 1;
	[SerializeReference, SubclassSelector] private BaseDamageModifier modifier;
	[SerializeField, NaughtyAttributes.EnumFlags] private Health.Type affects = Health.Type.PLAYER;
	[SerializeField] private int pierce = 1;
	[SerializeField] private int killPierce = 0;

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
			if(healthOfCollider.isAlive() || killPierce == 0) pierce -= 1;
			else killPierce -= 1;
        }

        if (pierce == 0)
        {
            ObjectDestroyer.destroy(gameObject, ObjectDestroyer.Cause.PROJECTILE_EXPIRE);
        }

    }
}
