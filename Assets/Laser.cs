using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : MonoBehaviour 
{
	private LineRenderer m_Laser;
	public List<GameObject> players;

	private Vector2 m_Direction = new Vector2(1.0f, 0.0f);

	void Start () 
	{
		m_Laser = GetComponent<LineRenderer>();

	}

	GameObject FindClosestPlayer() 
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Player");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) 
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) 
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	// Update is called once per frame
	void Update () 
	{	
		Vector3 t_Dir = new Vector3(m_Direction.x*2, -1.45f, m_Direction.y*2);
		m_Laser.SetPosition(1, new Vector3(m_Direction.x*2, -1.45f, m_Direction.y*2));

		GameObject t_Target = FindClosestPlayer();
		if(t_Target!=null)
		{
			Vector2 t_TargetDirection = (new Vector2(t_Target.transform.position.x, t_Target.transform.position.z)-new Vector2(transform.parent.position.x,transform.parent.position.z)).normalized;
			float z = t_TargetDirection.x * m_Direction.y - t_TargetDirection.y * m_Direction.x;

			if(z<0.0f)
				m_Direction = Quaternion.AngleAxis(Time.deltaTime*30.0f, Vector3.forward) * m_Direction;
			else m_Direction = Quaternion.AngleAxis(-Time.deltaTime*30.0f, Vector3.forward) * m_Direction;

			Vector3 t_LaserPointer = new Vector3(m_Direction.x*2, -1.45f, m_Direction.y*2);

			RaycastHit t_Hit;

			if(Physics.Raycast (transform.position, t_Dir, out t_Hit) && t_Hit.distance<t_Dir.magnitude && t_Hit.collider.tag=="Player")
			{
				t_Hit.collider.gameObject.GetComponent<Controller>().Kill();
			}

			m_Laser.SetPosition(1, t_LaserPointer);
		}


	}
}
