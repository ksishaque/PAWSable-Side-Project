#define DIGITAL
#define TIMEOUT

using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollWheelInterpreter{

	/*	Variables:
	scroll: Input action for scrolling
	up: Action callback for up
	down: Action callback for down
	total: Total scroll level so far
	timer: Timeout timer for remaining scroll level
	timeout: Total duration of timeout
	*/
	private InputAction scroll;
	public System.Action up;
	public System.Action down;
#if DIGITAL
	private float total = 0;
#if TIMEOUT
	private float timer = 0;
	private float timeout;
#endif
#endif


	//	Validation
	public ScrollWheelInterpreter(PlayerInput input, float timeout = 1){
		scroll = input.actions.FindAction("Scroll");
#if (DIGITAL && TIMEOUT)
		this.timeout = timeout;
#endif
	}


	//	Update for scrolling
	public void update(){

#if !DIGITAL
		//	Analog
		if(scroll.ReadValue<float>() > 0.5f) up();
		else if(scroll.ReadValue<float>() < -0.5f) down();

#elif TIMEOUT
		//	Digital
		if(scroll.ReadValue<float>() != 0){

			//	Add scroll value
			total += scroll.ReadValue<float>();

			//	Increment for each scroll level
			while(total >= 1){
				up();
				total -= 1;
			}
			while(total <= -1){
				down();
				total += 1;
			}

			//	Reset `timer`
			timer = timeout;

		}

		//	Increment `timer` and reset `total`
		else if(timer > 0) timer -= Time.unscaledDeltaTime;
		else total = 0;

#else

		//	Add scroll value
		total += scroll.ReadValue<float>();

		//	Increment for each scroll level
		while(total >= 1){
			up();
			total -= 1;
		}
		while(total <= -1){
			down();
			total += 1;
		}

#endif

	}

}
