using UnityEngine;

public class test : MonoBehaviour{

	//	Some debug classes, for checking serialize reference
	private interface BaseDebugTest{
		public void push();
	}
	[System.Serializable] private class DefaultDebugTest : BaseDebugTest{
		public void push(){
			Debug.Log("Debug Test (DEFAULT)");
		}
	}
	[System.Serializable] private class DebugTest : BaseDebugTest{
		[SerializeField] private string text;
		public DebugTest(){
			text = "Debug Test";
		}
		public DebugTest(string text){
			this.text = text;
		}
		public void push(){
			Debug.Log(text);
		}
	}


	[SerializeReference, SubclassSelector] private BaseDebugTest debug = new DefaultDebugTest();

	void Update(){
		debug.push();
	}
}
