﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;

namespace Joker.Identity.Constants
{
    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
        public static bool ShowLogoutPrompt = true;
        public static bool AutomaticRedirectAfterSignOut = true;
        public static string InvalidCredentialsErrorMessage = "Invalid username or password";
    }
}