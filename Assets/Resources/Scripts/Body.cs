using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour{

	GameObject m_Blood;

	void Start(){
		m_Blood = transform.Find("Blood").gameObject;

		m_Blood.transform.LookAt(transform.position+Vector3.up);
		Quaternion tmp = m_Blood.transform.rotation;
		tmp.eulerAngles = new Vector3(tmp.eulerAngles.x + Random.Range(-20, 20), tmp.eulerAngles.y + Random.Range(-20, 20), tmp.eulerAngles.z + Random.Range(-20, 20));
		m_Blood.transform.rotation = tmp;
		m_Blood.particleSystem.Play();
	}

	public void Update(){
		if(!m_Blood.particleSystem.isPlaying){
			Destroy(gameObject);
		}
	}
}
