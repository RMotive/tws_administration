using Foundation.Contracts.Datasources.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWS_Business.Entities;
using TWS_Business.Sets;

namespace TWS_Business.Repositories
{
    internal class TruckRepository
        :BRepository<TWSBusinessSource,TruckRepository,TruckEntity,Truck>{

        public TruckRepository()
            : base(new()) { }
    }

}
