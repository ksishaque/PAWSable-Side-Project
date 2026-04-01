using System.Collections.Generic;
using UnityEngine;

public abstract class PreviewImage : BaseBehavior.IActor{

	//	Update image
	abstract public void update(List<PreviewImage> images, float dt);

	//	Overrides
	public void addLocalPosition(Vector2 displacement){
	}
	public void destroySelf(){
	}

}