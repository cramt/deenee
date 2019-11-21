using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Deenee {

    public class CameraController : MainBehavior {
        public float dragSpeed = 0.1f;
        public float movespeed = 0.1f;
        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);
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


        }

    }
}