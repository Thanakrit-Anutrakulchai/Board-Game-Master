using UnityEngine;

// interface for canvases which are associated with a certain program state
internal interface IAssociatedState
{
    Canvas GetCanvas();
    ProgramData.State GetAssociatedState();
}
