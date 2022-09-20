using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers

{
    [ApiController]
    //the controller in square brackets is just a placeholder for what the class name is called, in this case thus "Products"
    [Route("api/[controller]")]
    public class BaseAPiController : ControllerBase
    {
        
    }
}