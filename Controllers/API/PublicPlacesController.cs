using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waka.Brokers;
using Waka.Models;
using Waka.Services;
using Waka.ViewModels;

namespace Waka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicPlacesController: ControllerBase
    {
        private readonly IStorageBroker<PublicPlaces> storageBroker;

        public PublicPlacesController(IStorageBroker<PublicPlaces> storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        [HttpGet]
        public IEnumerable<PublicPlaces> GetPlaces() => storageBroker.GetAll();

        [HttpGet("{Id}")]
        public PublicPlaces GetOnePlace(Guid Id) => storageBroker.GetAll().Where(o => o.Id == Id).FirstOrDefault();
       

    }
}
