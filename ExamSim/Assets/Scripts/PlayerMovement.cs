using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
public class PlayerMovement: MonoBehaviour 
{
    // 1
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    // 2

    public GameObject player;
    private float _vInput;
    private float _hInput;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        // 3
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        // 4
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        // 5
       this.transform.Translate(Vector3.forward * _vInput * 
        Time.deltaTime);
        // 6
       this.transform.Rotate(Vector3.up * _hInput * 
        Time.deltaTime);
         
    }

   
    void FixedUpdate()
    {
        // 2
        Vector3 rotation = Vector3.up * _hInput;
        // 3
        Quaternion angleRot = Quaternion.Euler(rotation *
            Time.fixedDeltaTime);
        // 4
        _rb.MovePosition(player.transform.position +
            player.transform.forward * _vInput * Time.fixedDeltaTime);
        // 5
        _rb.MoveRotation(_rb.rotation * angleRot);
    }
 private Rigidbody Get_rb()
    {
        return _rb;
    }

} 