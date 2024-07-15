using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using TWS_Customer.Services.Interfaces;

using TWS_Security.Depots;
using TWS_Security.Sets;

namespace TWS_Customer.Services;
/// <summary>
/// 
/// </summary>
public class SolutionsService
    : ISolutionsService {
    /// <summary>
    /// 
    /// </summary>
    private readonly SolutionsDepot SolutionsDepot;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Solutions"></param>
    public SolutionsService(SolutionsDepot Solutions) {
        SolutionsDepot = Solutions;
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
        return await SolutionsDepot.Create(Solutions);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Solution"></param>
    /// <returns></returns>
    public async Task<RecordUpdateOut<Solution>> Update(Solution Solution) {
        return await SolutionsDepot.Update(Solution);
    }
}
