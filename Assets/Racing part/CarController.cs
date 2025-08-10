using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBrakeForce;
    private bool isBraking;

    // Settings
    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float brakeForce = 3000f;
    [SerializeField] private float maxSteerAngle = 25f;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    // Rigidbody
    private Rigidbody rb;

    // Audio
    [Header("Audio Clips")]
    [SerializeField] private AudioClip idleAudioClip;
    [SerializeField] private AudioClip drivingAudioClip;
    [SerializeField] private AudioClip driftingAudioClip;

    private AudioSource audioSource;
    private enum CarSoundState { Idle, Driving, Drifting }
    private CarSoundState currentSoundState = CarSoundState.Idle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.9f, 0);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        PlaySound(idleAudioClip);
        currentSoundState = CarSoundState.Idle;
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        HandleAudio();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        float appliedTorque = verticalInput * motorForce;
        frontLeftWheelCollider.motorTorque = appliedTorque;
        frontRightWheelCollider.motorTorque = appliedTorque;

        currentBrakeForce = (isBraking || Mathf.Abs(verticalInput) < 0.05f) ? brakeForce : 0f;
        ApplyBraking();
    }

    private void ApplyBraking()
    {
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void HandleAudio()
    {
        if (isBraking && Mathf.Abs(horizontalInput) > 0.1f)
        {
            // Drifting sound
            if (currentSoundState != CarSoundState.Drifting)
            {
                PlaySound(driftingAudioClip);
                currentSoundState = CarSoundState.Drifting;
            }
        }
        else if (Mathf.Abs(verticalInput) > 0.1f)
        {
            // Driving sound
            if (currentSoundState != CarSoundState.Driving)
            {
                PlaySound(drivingAudioClip);
                currentSoundState = CarSoundState.Driving;
            }
        }
        else
        {
            // Idle sound
            if (currentSoundState != CarSoundState.Idle)
            {
                PlaySound(idleAudioClip);
                currentSoundState = CarSoundState.Idle;
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public float KPH
    {
        get
        {
            return rb.velocity.magnitude * 3.6f;
        }
    }
}