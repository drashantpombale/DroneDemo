using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody drone;
    private bool isOn = false, islfying = false;
    public Transform cam, wing1,wing2,wing3,wing4;
    private float speed;
    private float rotationsPerMinute;
    public Slider speedBar;
    private float maxSpeed = 50f;

    void Start()
    {
        rotationsPerMinute = 1000f;
        speed = 10f;
        drone = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!isOn)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                StartCoroutine(StartEngine());
            }
        }

        else
        {
            speedBar.value = drone.velocity.magnitude / maxSpeed;

            wing1.transform.Rotate(0, 0, 6 * rotationsPerMinute * Time.deltaTime);
            wing2.transform.Rotate(0, 0, 6 * rotationsPerMinute * Time.deltaTime);
            wing3.transform.Rotate(0, 0, 6 * rotationsPerMinute * Time.deltaTime);
            wing4.transform.Rotate(0, 0, 6 * rotationsPerMinute * Time.deltaTime);
            drone.AddRelativeForce(Vector3.up * 7.5f);
            if (Input.GetKey(KeyCode.Space))
            {
                drone.AddRelativeForce(Vector3.up * 15);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                drone.AddRelativeForce(Vector3.down * 10);
            }
            else drone.velocity = new Vector3(drone.velocity.x, 0, drone.velocity.z);
            if (islfying)
            {
            
                //Yaw Controls
                float yaw = Input.GetAxis("Horizontal");
                if (Mathf.Abs(yaw) > 0) {
                    transform.Rotate(new Vector3(0, yaw, 0), 100f * Time.deltaTime);

                }

                //Pitch Controls
                float pitch = Input.GetAxis("Vertical");
                if (pitch > 0)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(35, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), Time.deltaTime);
                    transform.position += Mathf.Sign(pitch) * transform.forward * Time.deltaTime * speed;
                }

                else if (pitch < 0)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-35, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), Time.deltaTime);
                }

                else if(pitch==0){
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), 5*Time.deltaTime);
                    drone.velocity = new Vector3(0, drone.velocity.y, 0);
                }
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                StartCoroutine(TurnOffEngine());
            }
        }
        if (drone.velocity.magnitude > maxSpeed)
        {
            drone.velocity = Vector3.ClampMagnitude(drone.velocity, maxSpeed);
        }
    }

    private IEnumerator TurnOffEngine()
    {
        Debug.Log("Engine Stopping");
        yield return new WaitForSeconds(1f);
        isOn = false;
        Debug.Log("Engine Stopped");
    }

    private IEnumerator StartEngine()
    {
        Debug.Log("Engine starting");
        yield return new WaitForSeconds(2f);
        isOn = true;
        Debug.Log("Engine started");
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9) {
            islfying = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            islfying = false;
        }
    }
}
