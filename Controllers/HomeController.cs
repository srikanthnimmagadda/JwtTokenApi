﻿using Microsoft.AspNetCore.Mvc;

namespace JwtTokenApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public HomeController()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to Home Controller");
        }
    }
}