﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
	private ISender? _sender;
	protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>()!;

}
