﻿namespace ZamzamUiCompact.Services.Security;

public enum CRED_TYPE
{
    GENERIC = 1,
    DOMAIN_PASSWORD,
    DOMAIN_CERTIFICATE,
    DOMAIN_VISIBLE_PASSWORD,
    GENERIC_CERTIFICATE,
    DOMAIN_EXTENDED,
    MAXIMUM,
    MAXIMUM_EX = (MAXIMUM + 1000)
}


