﻿namespace Zamzam.Application.Security;
public class AuthResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiredAt { get; set; }


}
