using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWS_Business.Sets;

namespace TWS_Business
{
    public partial class TWSBusinessSource
    : BMigrationSource<TWSBusinessSource>
    {
        public TWSBusinessSource()
            : base() { }

        protected override IMigrationSet[] EvaluateFactory()
        {
            return [
                new Plate(),
                new Manufacturer(),
                new Maintenance(),
                new Insurance(),
                new Situation(),
                new Truck(),
                new Sct()
            ];
        }
    }
}
