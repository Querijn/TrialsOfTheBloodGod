using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{

	public static Controller instance;

	private Vector3 m_StartPos, m_CameraStartPos;
	public float m_JumpForce = 300.0f;
	public float m_MoveForce = 40.0f;
	public float m_MaxSpeed = 10.0f;
	[Range(0,1)]public float m_ScreenBoundaryX = 0.2f;
	[Range(0,1)]public float m_ScreenBoundaryY = 0.4f;
	[Range(0,20)]public float m_CameraMinDistance = 4.0f;
	[Range(20,80)]public float m_CameraMaxDistance = 22.0f;
	public float m_CameraSpeedX = 5.2f;
	public float m_CameraSpeedY = 2.2f;
	public float m_CameraSpeedZoom = 0.6f;
	public GameObject[] m_Walls;
	public GameObject m_Camera;
	[Range(1,4)]public int m_PlayerNumber = 1;
	public float m_KillPlane = -4.0f;
	public int m_Deaths = 0;

	private GameObject body;
	private float timer = 0f;
	private bool stopdoubledeath = false;
	
	void Awake(){
		instance = this;
	}

	void Start () 
	{
		SetStartPos();

		body = Resources.Load("Prefabs/Body") as GameObject;
	}

	void FixedUpdate()
	{
		if(!Game.instance.israce){
			UpdateMovement(); // Upfdate the movement of the player
		}
		UpdateCameraBlocking(); // Update the camera making stuff transparant if it does not hit the player
		UpdateCameraPosition(); // Update camera position 
		UpdateAttachment(); // Check what box I am attached to

		CheckOutOfBounds ();
	}

	public void Kill()
	{
		stopdoubledeath = true;
		Instantiate(body, transform.position, Quaternion.identity);
		Respawn ();
	}

	void UpdateAttachment()
	{
		RaycastHit t_Hit;
		if(Physics.Raycast(transform.position, -Vector3.up, out t_Hit) && t_Hit.collider.gameObject.GetComponent<MoveBlock>()!=null && t_Hit.distance<0.5)
		{
			transform.parent = t_Hit.collider.transform;
		}
		else transform.parent = null;
	}

	void OnTriggerEnter(Collider a_Collider)
	{
		if(a_Collider.tag == "Death" && !stopdoubledeath)
		{
			Kill ();
		}
	}

	void Update(){
		if(stopdoubledeath){
			timer += Time.deltaTime;
			if(timer > 0.1f){
				stopdoubledeath = false;
				timer = 0f;
			}
		}
	}

	public void Respawn()
	{
		transform.position = new Vector3(m_StartPos.x, m_StartPos.y, m_StartPos.z);
		m_Camera.transform.position = m_CameraStartPos;
		rigidbody.velocity  = Vector3.zero;
		rigidbody.angularVelocity  = Vector3.zero;
		m_CameraVelocity = Vector3.zero;
		m_Deaths++;
	}

	void CheckOutOfBounds()
	{
		if (transform.position.y < m_KillPlane) 
		{
			Respawn ();
			return;
		}
	}

	public GameObject IsUpon()
	{
		RaycastHit t_Hit;
		if(Physics.Raycast(transform.position, -Vector3.up, out t_Hit) && t_Hit.collider.gameObject.GetComponent<MoveBlock>()!=null && t_Hit.distance<0.5)
			return t_Hit.collider.gameObject;
		else return null;
	}

	private Vector3 m_CameraVelocity;
	void UpdateCameraPosition()
	{
		Vector3 t_Position = m_Camera.camera.WorldToViewportPoint(transform.position); // what why is this a vec3
		
		m_CameraVelocity *= 0.98f;

		if(t_Position.x<m_ScreenBoundaryX)
			m_CameraVelocity.x += (m_ScreenBoundaryX-t_Position.x)*m_CameraSpeedX*Time.deltaTime;
		else if(t_Position.x>(1.0f-m_ScreenBoundaryX))
			m_CameraVelocity.x -= (t_Position.x-(1.0f-m_ScreenBoundaryX))*m_CameraSpeedX*Time.deltaTime;
		if(t_Position.y<m_ScreenBoundaryY)
			m_CameraVelocity.y -= (m_ScreenBoundaryY-t_Position.y)*m_CameraSpeedY*Time.deltaTime;
		else if(t_Position.y>(1.0f-m_ScreenBoundaryY))
			m_CameraVelocity.y += (t_Position.y-(1.0f-m_ScreenBoundaryY))*m_CameraSpeedY*Time.deltaTime;

		float t_CamDistance = (transform.position-m_Camera.transform.position).magnitude;
		//print(t_CamDistance);
		if(t_CamDistance>m_CameraMaxDistance)
			m_CameraVelocity.z -= (m_CameraMaxDistance-t_CamDistance)*m_CameraSpeedZoom*Time.deltaTime;
		else if(t_CamDistance<m_CameraMinDistance)
			m_CameraVelocity.z += (t_CamDistance-m_CameraMinDistance)*m_CameraSpeedZoom*Time.deltaTime;

		// World (kind of interesting but wrong)
		//m_Camera.transform.position += new Vector3(m_CameraVelocity.x, m_CameraVelocity.y, 0.0f);

		// Local (this'll do)
		m_Camera.transform.position -= m_Camera.transform.right * m_CameraVelocity.x;
		m_Camera.transform.position += m_Camera.transform.up * m_CameraVelocity.y;
		m_Camera.transform.position += m_Camera.transform.forward * m_CameraVelocity.z;
	}

	bool IsGrounded()
	{
		RaycastHit t_Hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out t_Hit)) 
		{
			if (t_Hit.collider == null) 
			{
				return false;
			} 
			else if (t_Hit.distance > transform.localScale.y) 
			{
				return false;
			}
			else return true;
		}
		return false;
	}


	void UpdateMovement() 
	{
		Vector2 t_Input = new Vector2(Input.GetAxis("L_YAxis_"+m_PlayerNumber), Input.GetAxis("L_XAxis_"+m_PlayerNumber)); // Get axis input
		if(t_Input.magnitude>0.1f)
		{
			rigidbody.AddForce(Quaternion.Euler (0,m_Camera.transform.eulerAngles.y+90,0)*new Vector3(t_Input.x*m_MoveForce, 0.0f, t_Input.y*m_MoveForce)); 
		}
		else rigidbody.AddForce(-rigidbody.velocity.x,0.0f, -rigidbody.velocity.z, ForceMode.Impulse);

		Vector3 t_Movement = rigidbody.velocity; t_Movement.y = 0.0f; 
		if (Input.GetButtonDown ("A_"+m_PlayerNumber) && IsGrounded()) 
			rigidbody.velocity = (transform.up*10.0f+t_Movement).normalized * m_JumpForce;

		t_Movement = rigidbody.velocity; t_Movement.y = 0.0f; 
		if(t_Movement.magnitude > m_MaxSpeed)
		{
			t_Movement = t_Movement.normalized * m_MaxSpeed;
			rigidbody.velocity = new Vector3(t_Movement.x, rigidbody.velocity.y, t_Movement.z);
		}

		transform.LookAt(rigidbody.position + t_Movement); // Look in that direction
		
	}

	void UpdateCameraBlocking()
	{
		if (m_Walls == null || m_Walls.Length==0) // If walls arent placed yet,
			m_Walls = GameObject.FindGameObjectsWithTag("Wall"); // Load them

		foreach (GameObject t_Wall in m_Walls) // Make all walls visible
			if(t_Wall.renderer!=null) 
				t_Wall.renderer.material.SetFloat("_Transparancy", 1.0f);
			
		Vector3 t_Camera = m_Camera.transform.position; // If we cant see our player because of a wall
		RaycastHit t_Hit;
		if (Physics.Raycast(t_Camera, (transform.position - t_Camera).normalized, out t_Hit) &&
		    t_Hit.collider.gameObject.tag!="Character")
		{
			if(t_Hit.collider.gameObject.renderer!=null) 
				t_Hit.collider.gameObject.renderer.material.SetFloat("_Transparancy", 0.3f); // make it transparant
		}
	}

	public void SetStartPos(Transform trans = null){
		if(trans == null){
			m_StartPos = transform.position;
			m_CameraStartPos = m_Camera.transform.position;
		}
		else{
			m_StartPos = trans.position;
			m_CameraStartPos = m_Camera.transform.position;	
		}
	}
}
