﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweegle.Infrastructure.Configuration
{
    public interface IConfigurationReader
    {
        string GetScreenName();

        string GetOAuthTokenSecret();

        string GetOAuthToken();

        string GetOAuthConsumerSecret();

        string GetOAuthConsumerKey();

        string GetConnectionString();
    }
}
