namespace HarSA.Configurations
{
    public class HarConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether to display the full error in production environment.
        /// It's ignored (always enabled) in development environment
        /// </summary>
        public bool DisplayFullErrorStack { get; set; } = false;
    }
}
