using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRelativeKeyboardControls : MonoBehaviour {

    public float movementSpeed;
    public float movementSmooth;
    Vector3 refMovement;
    Vector3 movement;
    Transform camT;

    CharacterController controller;

	// Use this for initialization
	void Start () {
        camT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        //Get the inputs from the user
        Vector3 inputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //Get the forward direction of the camera but ignore the Y element
        Vector3 cameraForward = camT.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        //Get the right direction of the camera but ignore the Y element
        Vector3 cameraRight = camT.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        //Use these inputs as being in camera space, and convert them into word space
        Vector3 newMovement = cameraForward * inputs.z * movementSpeed * Time.deltaTime;
        newMovement += cameraRight * inputs.x * movementSpeed * Time.deltaTime;
        movement = Vector3.SmoothDamp(movement, newMovement, ref refMovement, movementSmooth);

        //Move the drone using the character controller pre-made component. It does the same thing as the following line, but takes collisions into account
        //transform.Translate(movement, Space.World);
        controller.Move(movement);

        //Apply a rotation based on the acceleration to make the movement looks more pleasing
        RotateByAcceleration();
	}

    #region RotationPhysics
    public float rotationSmooth;
    public float rotationAmount;
    Vector3 acceleration;
    Vector3 refAcceleration;

    void RotateByAcceleration()
    {
        Vector3 newAcceleration;
        LinearAcceleration(out newAcceleration, transform.position, 3);
        acceleration = Vector3.SmoothDamp(acceleration, newAcceleration, ref refAcceleration, rotationSmooth);
        Vector3 rotation = Vector3.Cross(Vector3.up, acceleration);
        transform.eulerAngles = rotation * rotationAmount;
    }

    Vector3[] positionRegister;
    float[] posTimeRegister;
    private int positionSamplesTaken;

    //This function calculates the acceleration vector in meter/second^2.
    //Input: position. If the output is used for motion simulation, the input transform
    //has to be located at the seat base, not at the vehicle CG. Attach an empty GameObject
    //at the correct location and use that as the input for this function.
    //Gravity is not taken into account but this can be added to the output if needed.
    //A low number of samples can give a jittery result due to rounding errors.
    //If more samples are used, the output is more smooth but has a higher latency.
    public bool LinearAcceleration(out Vector3 vector, Vector3 position, int samples)
    {
        Vector3 averageSpeedChange = Vector3.zero;
        vector = Vector3.zero;
        Vector3 deltaDistance;
        float deltaTime;
        Vector3 speedA;
        Vector3 speedB;

        //Clamp sample amount. In order to calculate acceleration we need at least 2 changes
        //in speed, so we need at least 3 position samples.
        if (samples < 3)
        {

            samples = 3;
        }

        //Initialize
        if (positionRegister == null)
        {

            positionRegister = new Vector3[samples];
            posTimeRegister = new float[samples];
        }

        //Fill the position and time sample array and shift the location in the array to the left
        //each time a new sample is taken. This way index 0 will always hold the oldest sample and the
        //highest index will always hold the newest sample. 
        for (int i = 0; i < positionRegister.Length - 1; i++)
        {

            positionRegister[i] = positionRegister[i + 1];
            posTimeRegister[i] = posTimeRegister[i + 1];
        }
        positionRegister[positionRegister.Length - 1] = position;
        posTimeRegister[posTimeRegister.Length - 1] = Time.time;

        positionSamplesTaken++;

        //The output acceleration can only be calculated if enough samples are taken.
        if (positionSamplesTaken >= samples)
        {

            //Calculate average speed change.
            for (int i = 0; i < positionRegister.Length - 2; i++)
            {

                deltaDistance = positionRegister[i + 1] - positionRegister[i];
                deltaTime = posTimeRegister[i + 1] - posTimeRegister[i];

                //If deltaTime is 0, the output is invalid.
                if (deltaTime == 0)
                {

                    return false;
                }

                speedA = deltaDistance / deltaTime;
                deltaDistance = positionRegister[i + 2] - positionRegister[i + 1];
                deltaTime = posTimeRegister[i + 2] - posTimeRegister[i + 1];

                if (deltaTime == 0)
                {

                    return false;
                }

                speedB = deltaDistance / deltaTime;

                //This is the accumulated speed change at this stage, not the average yet.
                averageSpeedChange += speedB - speedA;
            }

            //Now this is the average speed change.
            averageSpeedChange /= positionRegister.Length - 2;

            //Get the total time difference.
            float deltaTimeTotal = posTimeRegister[posTimeRegister.Length - 1] - posTimeRegister[0];

            //Now calculate the acceleration, which is an average over the amount of samples taken.
            vector = averageSpeedChange / deltaTimeTotal;

            return true;
        }

        else
        {

            return false;
        }
    }
    #endregion
}
