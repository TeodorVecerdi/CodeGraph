using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeGraph.UI {
    public class Draggable : MouseManipulator {
        private Action<Vector2> handler;
        private bool active;
        private bool outputDeltaMovement;
        
        public Draggable(Action<Vector2> handler, bool outputDeltaMovement = false)
        {
            this.handler = handler;
            active = false;
            this.outputDeltaMovement = outputDeltaMovement;
            var item = new ManipulatorActivationFilter {button = MouseButton.LeftMouse};
            activators.Add(item);
        }

        protected override void RegisterCallbacksOnTarget() {
            target.RegisterCallback(new EventCallback<MouseDownEvent>(OnMouseDown));
            target.RegisterCallback(new EventCallback<MouseMoveEvent>(OnMouseMove));
            target.RegisterCallback(new EventCallback<MouseUpEvent>(OnMouseUp));
        }

        protected override void UnregisterCallbacksFromTarget() {
            target.UnregisterCallback(new EventCallback<MouseDownEvent>(OnMouseDown));
            target.UnregisterCallback(new EventCallback<MouseMoveEvent>(OnMouseMove));
            target.UnregisterCallback(new EventCallback<MouseUpEvent>(OnMouseUp));
        }

        void OnMouseDown(MouseDownEvent mouseEvent) {
            target.CaptureMouse();
            active = true;
            mouseEvent.StopPropagation();
        }

        void OnMouseMove(MouseMoveEvent mouseEvent) {
            if (!active)
                return;
            handler(outputDeltaMovement ? mouseEvent.mouseDelta : mouseEvent.localMousePosition);
        }

        void OnMouseUp(MouseUpEvent mouseEvent) {
            active = false;
            if (target.HasMouseCapture()) {
                target.ReleaseMouse();
            }
            mouseEvent.StopPropagation();
        }
    }
}