# libHitron

![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/Krutonium/dotDesktop4dotNet) [![GitHub License](https://img.shields.io/github/license/Krutonium/dotDesktop4dotNet)](https://github.com/Krutonium/dotDesktop4dotNet/blob/master/LICENSE)

A simple library to access and eventually change the settings on a Hitron CGN3 Modem

In theory this should work in general for _most_ versions of the CGN3 Modem. As of time of writing, it is read only, but it can give you the following information:

All of the status information on the index page of your modem,
A your SSID's, Passwords for said, etc.
Everything you have port forwarded - Ports, IP's, Names.

To use, add the dll or project to your own project, and then do this:
```
 var example = new libHitron.libHitron();
 example.Connect("192.168.0.1", "cusadmin", "password"); //Default IP Address, Username, and Password. Returns True if credentials are verified, False if it is unable to verify for any reason.
```
You _MUST_ run Connect before attempting anything else, as it verifies and stores the credentials for all use afterwards.

Each call will re-authenticate against the modem, because it sets authentication cookies with very short time limits, and re-authentication is cheap and easy.
