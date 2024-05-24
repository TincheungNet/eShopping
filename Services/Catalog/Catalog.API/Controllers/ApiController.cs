using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace Catalog.API.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
//[Authorize(Policy = "CanRead")]
public class ApiController: ControllerBase
{
    
}