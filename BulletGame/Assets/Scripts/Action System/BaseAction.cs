using UnityEngine;

//	Basic multi-purpose action node
[System.Serializable] abstract public partial class BaseAction{

	//	Variable: Running instance of the list
	protected Runner instance{
		get;
		private set;
	}

	//	Cloning
	abstract public BaseAction clone();

	//	Validate actions
	virtual public void validate(GameObject actor){}

	//	Initialize members, then start
	virtual protected void initialize(Runner runner){

		//	Set members
		instance = runner;

		//	Run `start()`
		start();

	}

	//	Initial and update steps of action
	virtual protected void start(){}
	virtual protected void update(ref float remainingTime){}

	//	Manage behaviors
	virtual public bool setEndless(bool endless = true){
		return false;
	}
	virtual protected bool isPreviewSafe() => false;

}

//	Basic action node that runs in an instant
[System.Serializable] abstract public class BaseInstantAction : BaseAction{

	//	Overrides
	sealed override protected void start(){}
	sealed override protected void update(ref float remainingTime){
		update();
	}

	//	Simplified version of `update()`, after `remainingTime` has been handled
	abstract protected void update();

}

//	Basic action node that runs in an instant
[System.Serializable] abstract public class BaseTimedAction : BaseAction{

	[Header("Timer")]
	/*	Variables:
	Duration: Inspector-editable initial duration
	duration: Duration of the action
	time: Time since start of the action
	function: Function to scale `completion` by
	completion: Completion rate of the action
	dCompletion: Amount of completion rate covered in the last frame
	pCompletion: Completion rate from the last frame
	*/
	[SerializeField] private float Duration;
	protected float duration => Duration;
	private float time = 0;
	[SerializeReference, SubclassSelector] private BaseScalingFunction completionFunction = new StandardScalingFunction();
	protected float completion => completionFunction.operate(time / Duration);
	protected float dCompletion => completion - pCompletion;
	protected float pCompletion{
		get;
		private set;
	} = 0;

	//	Constructors
	protected BaseTimedAction(){
		Duration = 1;
	}
	protected BaseTimedAction(float duration, BaseScalingFunction completionFunction){
		Duration = duration;
		this.completionFunction = completionFunction;
	}
	protected BaseTimedAction(BaseTimedAction origin){
		Duration = origin.Duration;
		completionFunction = origin.completionFunction;
	}

	//	Overrides
	sealed override protected void update(ref float remainingTime){

		//	Store `pCompletion`
		pCompletion = completion;

		//	Update `time` and `remainingTime`
		time += remainingTime;
		remainingTime = time - Duration;
		if(remainingTime > 0) time = Duration;

		//	Update
		update();

		//	Check if the action needs to end
		if(remainingTime >= 0) exit();

	}

	//	Simplified version of `update()`, after `remainingTime` has been handled
	abstract protected void update();

	//	Final step of action
	virtual protected void exit(){}

}

//	Action node to signify an error
[System.Serializable] public class ErrorAction : BaseInstantAction{

	//	Variable: Error message to log
	[SerializeField] private string message;

	//	Constructors
	public ErrorAction(){
		message = "";
	}
	public ErrorAction(string message){
		this.message = message;
	}

	//	Overrides
	override public BaseAction clone(){
		return new ErrorAction(message);
	}
	override protected void update(){
		Debug.LogError(message);
	}

}