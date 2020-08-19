using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waka.Brokers;
using Waka.Models;

namespace Waka.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StateController: ControllerBase
    {
        private readonly IStorageBroker<NigerianStates> storageBroker;

        public StateController(IStorageBroker<NigerianStates> storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        [HttpGet]
        public IEnumerable<NigerianStates> GetNigerianStates() => storageBroker.GetAll();

        [HttpPost]
        public NigerianStates PostState(NigerianStates state) => storageBroker.Post(state);

        [HttpGet ("{id}")]
        public NigerianStates GetNigerianStateById(Guid id)
        {
            var states = storageBroker.GetAll();
            return states.Where(s => s.NigerianStatesId == id).FirstOrDefault();
        }

        [HttpDelete ("{id}")]
        public void Deletestate(Guid id)
        {
            var states = storageBroker.GetAll();
            var delState = states.Where(d => d.NigerianStatesId == id).FirstOrDefault();
            storageBroker.Delete(delState);
        }

        [HttpPut("{id}")]
        public void PutState(Guid id, NigerianStates state)
        {
            var states = storageBroker.GetAll();
            var compareState = storageBroker.GetAll().Where(c => c.NigerianStatesId == id).FirstOrDefault();
            var stateToUpdate = states.Where(d => d.NigerianStatesId == id).FirstOrDefault();

            stateToUpdate.Capital = string.IsNullOrWhiteSpace(state.Capital) ? stateToUpdate.Capital : state.Capital;
            stateToUpdate.Name = string.IsNullOrWhiteSpace(state.Name) ? stateToUpdate.Name : state.Name;
            stateToUpdate.SerialNumber = string.IsNullOrWhiteSpace(state.SerialNumber) ? stateToUpdate.SerialNumber : state.SerialNumber;
            storageBroker.Update(stateToUpdate, compareState);
        }

    }
}
