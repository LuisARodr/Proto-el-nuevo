using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;

public class TestControls : MonoBehaviour {
    
    int connectedControllers;
    Vector2[,] joystick;
    Vector2[] pad;
    float[,] trigger;
    int control;
    int joy;

    private void Start() {
        connectedControllers = Input.GetJoystickNames().Length;
        joystick = new Vector2[connectedControllers, 2];
        trigger = new float[connectedControllers, 2];
        pad = new Vector2[connectedControllers];
        print("Controles conectados: " + connectedControllers);
    }

    void Update () {
        for (control = 1; control <= connectedControllers; control++) {
            //botones
            if (Controllers.GetButton(control, "A", 2)) {
                print("Control " + control + " - Boton A");
            }
            if (Controllers.GetButton(control, "B", 2)) {
                print("Control " + control + " - Boton B");
            }
            if (Controllers.GetButton(control, "X", 2)) {
                print("Control " + control + " - Boton X");
            }
            if (Controllers.GetButton(control, "Y", 2)) {
                print("Control " + control + " - Boton Y");
            }
            if (Controllers.GetButton(control, "Back", 2)) {
                print("Control " + control + " - Boton Back");
            }
            if (Controllers.GetButton(control, "Start", 2)) {
                print("Control " + control + " - Boton Start");
            }
            if (Controllers.GetButton(control, "LB", 2)) {
                print("Control " + control + " - Boton LB");
            }
            if (Controllers.GetButton(control, "RB", 2)) {
                print("Control " + control + " - Boton RB");
            }
            //pad
            pad[control - 1] = Controllers.GetPad(control);
            if (pad[control - 1] != Vector2.zero) {
                print("Control " + control + " - " + pad[control - 1]);
            }
            if (Controllers.PadReturnedCenter(control)) {
                print("Control " + control + " - Pad centrado");
            }
            for (joy = 1; joy < 3; joy++) {
                //joysticks
                joystick[control - 1, joy - 1] = Controllers.GetJoystick(control, joy);
                if (joystick[control - 1, joy - 1] != Vector2.zero) {
                    print("Control " + control + " - Joystick " + joy + " - " + joystick[control - 1, joy - 1]);
                }
                if (Controllers.JoystickReturnedCenter(control, joy)) {
                    print("Control " + control + " - Joystick " + joy + " - centrado");
                }
                if (Controllers.GetJoystickClick(control, joy, 2)) {
                    print("Control " + control + " - Joystick " + joy + " - click");
                }
                //triggers
                trigger[control - 1, joy - 1] = Controllers.GetTrigger(control, joy);
                if (trigger[control - 1, joy - 1] != 0f) {
                    print("Control " + control + " - Trigger " + (joy == 1 ? "L" : "R") + " - " + trigger[control - 1, joy - 1]);
                }
                if (Controllers.TriggerUp(control, joy)) {
                    print("Control " + control + " - Trigger " + (joy == 1 ? "L" : "R") + " - suelto");
                }
            }
        }
    }
}
