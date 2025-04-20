using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SakilaAPI.Controllers.Diagnostics;

[ApiController]
[Route("api/v1/apihealth")]
public class APIHealthController : ControllerBase
{
    [HttpGet(Name = "APIHealth")]
    public string Hello()
    {
        string hostName = System.Net.Dns.GetHostName();
        StringBuilder sb = new();
        foreach (var adr in System.Net.Dns.GetHostEntry(hostName).AddressList)
            sb.Append($"Adress: {adr.AddressFamily} {adr.ToString()}\n");
        return $"Hello from host: {hostName}\n{sb}";
    }
}