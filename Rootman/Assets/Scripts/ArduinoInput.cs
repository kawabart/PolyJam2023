using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.IO.Ports;
using System.Globalization;
using UnityEngine.SceneManagement;

public class ArduinoInput : MonoBehaviour
{
    [SerializeReference] public String port = "COM3";
    private PlayerInputs playerInputs;

    [SerializeReference]  float minValue = 0.05f;
    [SerializeReference]  float maxSpeed = 100f;

    float rotX = 0;
    float rotY = 0;
    float rotZ = 0;
    Vector3 startPos = new Vector3(0, 0, 0);

    float buttonDelay = 0;

    private bool _isSerialOpened = false;
    private SerialPort _serialPort;

    private void Start()
    {
        playerInputs = GetComponent<PlayerInputs>();
    }

    void Awake()
    {
        string[] ports = SerialPort.GetPortNames();
        if(ports.Length > 0)
        {
            _serialPort = new SerialPort(ports[0], 9600);
        } else
        {
            _serialPort = new SerialPort(port, 9600);
        }

        try
        {
            _serialPort.Open();
            _isSerialOpened = true;
        }
        catch (IOException e)
        {
            Debug.Log("Failed to connect to Arduino");
            _isSerialOpened = false;
        }
    }

    void Update()
    {
        if (!_isSerialOpened || _serialPort.BytesToRead <= 0) return;
        var values = _serialPort.ReadLine().Split(';');
        if (values.Length != 5) return;

        var b1 = int.Parse(values[0]) == 1;
        Debug.Log(b1);
        var b2 = int.Parse(values[1]) == 1;
        Debug.Log(b2);
        var x = float.Parse(values[2], CultureInfo.InvariantCulture.NumberFormat);
        var y = float.Parse(values[3], CultureInfo.InvariantCulture.NumberFormat);
        var z = float.Parse(values[4], CultureInfo.InvariantCulture.NumberFormat);

        if (b2)
        {
            buttonDelay += Time.deltaTime;
        } else
        {
            buttonDelay = 0;
        }

        if(buttonDelay >= 2)
        {
            startPos = new Vector3(0, 0, 0);
            playerInputs.look = new Vector2(0, 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Math.Abs(y) > minValue)
        {
            startPos.y += y;
        }
        /*
        else
        {
            if (startPos.y < 0)
            {
                startPos.y += minValue;
                if (startPos.y > 0)
                {
                    startPos.y = 0;
                }
            }
            else
            {
                startPos.y -= minValue;
                if (startPos.y < 0)
                {
                    startPos.y = 0;
                }
            }
        } */
        if (Math.Abs(z) > minValue)
        {
            startPos.z += z;
        }
        /*
        else
        {
            if (startPos.z < 0)
            {
                startPos.z += minValue;
                if (startPos.z> 0)
                {
                    startPos.z = 0;
                }
            }
            else
            {
                startPos.z -= minValue;
                if (startPos.z < 0)
                {
                    startPos.z = 0;
                }
            }
        } */
        /*
        rotY = startPos.y;
        if (Math.Abs(rotY) > maxSpeed)
        {
            rotY = rotY > 0 ? maxSpeed : -maxSpeed;
        } */
        rotZ = startPos.z;

        if (Math.Abs(rotZ) < minValue)
        {
            rotZ = 0;
        }
        if (Math.Abs(rotZ) > maxSpeed)
        {
            rotZ = rotZ > 0 ? maxSpeed : -maxSpeed;
        }
        rotY = startPos.y;

        if (Math.Abs(rotY) < minValue)
        {
            rotY = 0;
        }
        if (Math.Abs(rotY) > maxSpeed)
        {
            rotY = rotY > 0 ? maxSpeed : -maxSpeed;
        }

        playerInputs.look = new Vector2(- rotZ / 10, -rotY / 10);

        playerInputs.rootUsed = b1;
        playerInputs.shooting = b2;
        Debug.Log("X:"+rotX+"; Y:"+rotY+"; Z:"+rotZ);
}

private void OnDestroy()
{
try
{
    _serialPort.Close();
} catch (IOException e)
{
    _isSerialOpened = false;
}
}
}
/*
public struct PlayerInput
{
public bool ButtonClicked;
public KeyValuePair<int, int> JoystickPosition;
}*/