using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ToDo.DB;

namespace To_Do_api.Controllers
{
    [RoutePrefix("api/v1/Account")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : GenericController
    {

    }
}
