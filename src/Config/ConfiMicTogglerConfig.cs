namespace ConfiMicToggler.Config
{
    /// <summary>
    /// Representation of the appsettings configuration
    /// </summary>
    public class ConfiMicTogglerConfig
    {
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public string Port { get; set; }

        /// <summary>
        /// Gets or sets the target conference tool. Can be "Skype" for use in Skype for Business or "Teams" for MicrosoftTeams
        /// </summary>
        /// <value>
        /// The target conference tool.
        /// </value>
        public string TargetConferenceTool { get; set; }
    }
}
