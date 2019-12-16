using PowerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Input = UnityEngine.Input;

namespace Deenee {

    public class CameraBehavior : MainBehavior {
        public float dragSpeed = 0.1f;
        public float movespeed = 0.1f;
        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);
            
        }
        private Vector3 handleOpenMenuClickPosStart;
        private OptionsMenu currentMenu = null;
        private void HandleOpenMenu() {
            if(Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Escape)) {
                if(currentMenu != null) {
                    InvokeFunction(() => {
                        currentMenu.Remove();
                    });
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                handleOpenMenuClickPosStart = Input.mousePosition;
            }
            if (Input.GetKeyUp(KeyCode.Mouse1)) {
                if (handleOpenMenuClickPosStart == Input.mousePosition) {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit)) {
                        var menu = hit.transform.GetComponent<IMenuOpenable>()?.Menu;
                        if (menu != null) {
                            currentMenu = new OptionsMenu(menu, GameObject.Find("Canvas"));
                        }
                    }
                }
            }
        }
        private void HandleRotation() {
            if (Input.GetKey(KeyCode.Mouse1)) {
                var pos1 = Input.mousePosition;
                InvokeFunction(() => {
                    var pos2 = Input.mousePosition;
                    var diff = pos1 - pos2;
                    transform.eulerAngles += new Vector3(-diff.y, diff.x, 0) * dragSpeed;
                });
            }
        }
        private void HandleMovement() {
            void move(Vector3 direction) {
                transform.Translate(transform.rotation * direction * movespeed, Space.World);
            }
            if (Input.GetKey(KeyCode.W)) {
                move(Vector3.forward);
            }
            if (Input.GetKey(KeyCode.A)) {
                move(Vector3.left);
            }
            if (Input.GetKey(KeyCode.S)) {
                move(Vector3.back);
            }
            if (Input.GetKey(KeyCode.D)) {
                move(Vector3.right);
            }
            if (Input.GetKey(KeyCode.LeftShift)) {
                move(Vector3.down);
            }
            if (Input.GetKey(KeyCode.Space)) {
                move(Vector3.up);
            }
        }
        public override void OnUpdate(OnUpdateProperties onUpdateProperties) {
            base.OnUpdate(onUpdateProperties);
            HandleRotation();
            HandleMovement();
            HandleOpenMenu();

        }

    }
}