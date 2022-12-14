using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//Anujitha Fernando (20903448) & Senara Mallikahewa (20903435) - Assignment 1

namespace ServiceProvider.Controllers
{
    public class ServicesController : ApiController
    {
        // HTTPGet method - ADDTwoNumbers
        [HttpGet]
        [Route("add2Number/{num1}/{num2}")]
        public int ADDTwoNumbers(int num1, int num2)
        {
            int tot = 0;
            tot = num1 + num2;

            return tot;
        }


        // HTTPGet method - ADDThreeNumbers
        [HttpGet]
        [Route("add3Number/{num1}/{num2}/{num3}")]
        public int ADDThreeNumbers(int num1, int num2, int num3)
        {
            int tot = 0;
            tot = num1 + num2 + num3;

            return tot;
        }


        // HTTPGet method - MulTwoNumbers
        [HttpGet]
        [Route("multi2Number/{num1}/{num2}")]
        public int MulTwoNumbers(int num1, int num2)
        {
            int tot = 0;
            tot = num1 * num2;

            return tot;
        }


        // HTTPGet method - MulThreeNumbers
        [HttpGet]
        [Route("mult3Number/{num1}/{num2}/{num3}")]
        public int MulThreeNumbers(int num1, int num2, int num3)
        {
            int tot = 0;
            tot = num1 * num2 * num3;

            return tot;
        }

    }
}
