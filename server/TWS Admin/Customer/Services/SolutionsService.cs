using Customer.Services.Interfaces;

using TWS_Security.Depots;
using TWS_Security.Sets;
using CSMFoundation.Source.Models.Out;
using CSMFoundation.Source.Models.In;

namespace Customer.Services;
/// <summary>
/// 
/// </summary>
public class SolutionsService
    : ISolutionsService {
    /// <summary>
    /// 
    /// </summary>
    readonly SolutionsDepot SolutionsDepot;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Solutions"></param>
    public SolutionsService(SolutionsDepot Solutions) {
        this.SolutionsDepot = Solutions;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Options"></param>
    /// <returns></returns>
    public async Task<SetViewOut<Solution>> View(SetViewOptions Options) {
        return await SolutionsDepot.View(Options);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Solutions"></param>
    /// <returns></returns>
    public async Task<SourceTransactionOut<Solution>> Create(Solution[] Solutions) {
        return await this.SolutionsDepot.Create(Solutions);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Solution"></param>
    /// <returns></returns>
    public async Task<RecordUpdateOut<Solution>> Update(Solution Solution) { 
        return await this.SolutionsDepot.Update(Solution);    
    }
}
