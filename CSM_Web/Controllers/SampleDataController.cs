using CSM_Web.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace CSM_Web.Controllers
{

    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(SampleData.Orders, loadOptions);
        }

    }
}