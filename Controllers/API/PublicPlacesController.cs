﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Route("group")]
        public IEnumerable<PublicPlaces> GetGroupPlace(string Cat)
        {
            List<PublicPlaces> publicPlaces = storageBroker.GetAll().ToList();
            List<PublicPlaces> selectedPlaces = new List<PublicPlaces>();
            foreach (var place in publicPlaces)
            {
                if (place.Description.ToLower() == Cat.ToLower())
                {
                    selectedPlaces.Add(place);
                }
            }
            return selectedPlaces;
        }



    }
}
