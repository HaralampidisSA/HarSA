﻿namespace HarSA.Net.Mail
{
    public interface IEmailSenderConfiguration
    {
        string DefaultFromAddress { get; set; }

        string DefaultFromDisplayName { get; set; }
    }
}
