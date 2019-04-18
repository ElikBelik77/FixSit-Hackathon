namespace FixSitWPF.Controller
{

    public delegate void ActivityEventArgs(IActivity sender);
    /// <summary>
    /// Activity interface for the scheduler.
    /// </summary>
    public interface IActivity
    {
        /// <summary>
        /// Starts the activity.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the activity.
        /// </summary>
        void Stop();

        /// <summary>
        /// Occurs when [on finish].
        /// </summary>
        event ActivityEventArgs OnFinish;

        /// <summary>
        /// Gets the priority of the activity.
        /// </summary>
        /// <returns></returns>
        int GetPriority();

        /// <summary>
        /// Gets the priority of the 
        /// </summary>
        /// <returns></returns>
        string GetIdentifier();
    }
}
