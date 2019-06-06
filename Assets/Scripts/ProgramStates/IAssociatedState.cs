using UnityEngine;

/// <summary>
/// // interface for canvases which are associated with a certain program state
/// </summary>
/// <typeparam name="T">Type of arguments to be passed from previous state</typeparam>>
/// <typeparam name="R">Type of arguments to be passed onto next state</typeparam>>
internal interface IAssociatedState<T, R>
{
    /// <summary>
    /// returns canvas associated with program state
    /// </summary>
    /// <returns>The canvas</returns>
    Canvas GetCanvas();

    /// <summary>
    /// Gets the associated state
    /// </summary>
    /// <returns>The associated state</returns>
    ProgramData.State GetAssociatedState();

    /// <summary>
    /// Method to be called when state is switched over to this one
    /// </summary>
    /// <param name="args">arguments passed on from previous state</param>
    void OnEnterState(T args);
    /// <summary>
    /// Method ot be called upon switching over from this state
    /// </summary>
    /// <returns>Parameters to pass onto next state</returns>
    R OnLeaveState();
}
