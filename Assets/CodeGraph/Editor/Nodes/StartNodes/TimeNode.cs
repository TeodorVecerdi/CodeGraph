using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeGraph.Editor {
    [Title("Basic", "Time")]
    public class TimeNode : AbstractStartNode {
        public TimeNode() {
            Initialize("Time", DefaultNodePosition);
            var timePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var unscaledTimePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var fixedTimePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var fixedUnscaledTimePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var deltaTimePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var unscaledDeltaTimePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var fixedDeltaTimePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var fixedUnscaledDeltaTimePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            var timeScalePort = base.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            timePort.portName = "time";
            unscaledTimePort.portName = "unscaledTime";
            fixedTimePort.portName = "fixedTime";
            fixedUnscaledTimePort.portName = "fixedUnscaledTime";
            deltaTimePort.portName = "deltaTime";
            unscaledDeltaTimePort.portName = "unscaledDeltaTime";
            fixedDeltaTimePort.portName = "fixedDeltaTime";
            fixedUnscaledDeltaTimePort.portName = "fixedUnscaledDeltaTime";
            timeScalePort.portName = "timeScale";
            AddOutputPort(timePort, () => "Time.time");
            AddOutputPort(unscaledTimePort, () => "Time.unscaledTime");
            AddOutputPort(fixedTimePort, () => "Time.fixedTime");
            AddOutputPort(fixedUnscaledTimePort, () => "Time.fixedUnscaledTime");
            AddOutputPort(deltaTimePort, () => "Time.deltaTime");
            AddOutputPort(unscaledDeltaTimePort, () => "Time.unscaledDeltaTime");
            AddOutputPort(fixedDeltaTimePort, () => "Time.fixedDeltaTime");
            AddOutputPort(fixedUnscaledDeltaTimePort, () => "Time.fixedUnscaledDeltaTime");
            AddOutputPort(timeScalePort, () => "Time.timeScale");
            Refresh();
        }
        public override void SetNodeData(string jsonData) {
            // This node does not not require any data
        }
        public override string GetNodeData() {
            return "";
        }
    }
}