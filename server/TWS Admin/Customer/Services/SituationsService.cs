using Customer.Services.Interfaces;
using Foundation.Migrations.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWS_Business.Depots;
using TWS_Business.Sets;

namespace Customer.Services;
public class SituationsService :ISituationsService {

    readonly SituationsDepot Situations;

    public SituationsService(SituationsDepot situations) {
        this.Situations = situations;
    }

    public async Task<MigrationView<Situation>> View(MigrationViewOptions options) {
        return await Situations.View(options);
    }
}
