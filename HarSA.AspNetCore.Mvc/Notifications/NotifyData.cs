namespace HarSA.AspNetCore.Mvc.Notifications
{
    public struct NotifyData
    {
        /// <summary>
        /// Message type (success/warning/error)
        /// </summary>
        public NotifyType NotifyType { get; set; }

        /// <summary>
        /// Message text
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get a sets a value indicating whether the message should not be HTML encoded
        /// </summary>
        public bool Encode { get; set; }
    }
}
