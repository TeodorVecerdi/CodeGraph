using UnityEngine;
public class TargetFollow : MonoBehaviour {
public Transform Target;//BEGIN_NODE_GUID/aeadd7ff-d9c4-431f-9696-78470f4ac544/END_NODE_GUID
public Vector3 Offset;//BEGIN_NODE_GUID/e0c18633-70c0-4f3a-8c4c-ebf35b28f76b/END_NODE_GUID
private void LateUpdate() {
if(!Target.Equals(((object)null) /* WARNING: You probably want connect this node to something. Node GUID: db9377f2-89ab-4e8c-8544-71ad126a8a30 */)) {//BEGIN_NODE_GUID/50995a65-60d9-4bdd-a236-933448eabd56/END_NODE_GUID
transform.position=Target.position+Offset;//BEGIN_NODE_GUID/3033761e-a248-4838-9e3c-2e5bae159c5e/END_NODE_GUID
}
}

}