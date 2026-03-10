using UnityEngine;

//	Action specifically made for enemy pathing, with preview options
[System.Serializable] abstract public class BaseBehavior : BaseAction{

	//	Attempt to set the behavior as unending (i.e. it is the last behavior on a non-destructive list)
	virtual public bool setEndless(bool endless = true){
		return false;
	}

	//	Draw preview
	//abstract public void drawPreview(ref Vector2 position, ref float timeUntilImage, ref float timeUntilDurationImage, float imageRadius, bool endless = false);
	protected void drawImage(Vector2 position, float imageRadius){
		Gizmos.DrawSphere(position, imageRadius);
	}
	protected void drawDurationImage(Vector2 position, float imageRadius){
		Gizmos.DrawWireCube(position, new Vector3(imageRadius * 2, imageRadius * 2, 0));
	}

}