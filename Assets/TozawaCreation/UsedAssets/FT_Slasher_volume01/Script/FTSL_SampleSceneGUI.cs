using UnityEngine;
using System.Collections;

public class FTSL_SampleSceneGUI : MonoBehaviour {

	public UnityEngine.UI.Text prefabName;
	public GameObject[] particlePrefab;
	public int particleNum = 0;
	public GameObject zombie;

	GameObject effectPrefab;

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0) ){ 
			Ray ray;
			RaycastHit hit;
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 1000.0f)){				
				effectPrefab = (GameObject)Instantiate(particlePrefab[particleNum],
				new Vector3(0.0f, 1.2f, -1.4f), Quaternion.Euler(0,0,0));
			}
			zombie.GetComponent<Animation>().Play("attack");
			zombie.GetComponent<Animation>().PlayQueued("idle");
		}
		
		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			Destroy(effectPrefab);
			particleNum -= 1;
			if( particleNum < 0) {
				particleNum = particlePrefab.Length-1;
			}	
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)){
			Destroy(effectPrefab);
			particleNum += 1;
			if(particleNum >(particlePrefab.Length - 1)) {
				particleNum = 0;
			}
		}
		
		prefabName.text= particlePrefab[particleNum].name;
		
	}
}
