# Zaptibot Identity Server

## Overview

The Zaptibot Identity Server provides authentication services, including generating JWT tokens for users. This document provides instructions on how to execute API calls using the `api.http` file.

## Prerequisites

- .NET 8.0 SDK or later
- An IDE that supports `.http` files

## Configuration

Ensure that the `appsettings.json` file is configured with the appropriate JWT token settings:

```json
{
  "JwtTokenSettings": {
    "Key": "ZaptibotSuperExtremeSecretKeyForNow",
    "Issuer": "Zaptibot",
    "Audience": "Zaptibot",
    "AccessTokenExpiration": 60,
    "RefreshTokenExpiration": 60
  }
}
```

## API Documentation and Execution

You can use the `api.http` file to document and execute API calls directly from your IDE.

## Notes

- Ensure the server is running on the correct port (default is `5101`).
- Replace `zaptibot@example.com` and `your_password` with actual credentials.

## License

This project is licensed under the MIT License.