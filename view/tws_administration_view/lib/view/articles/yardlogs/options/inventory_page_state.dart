part of '../truck_inventory_article.dart';

///
final class _InventoryPageState extends CSMStateBase {
  static final DateTime initDate = DateTime(2024, 09, 01);

  final TWSArticleTableAgent tableAgent;

  ///
  List<SetViewFilterNodeInterface<TruckInventory>> filters = <SetViewFilterNodeInterface<TruckInventory>>[];

  ///
  Section? sectionFilter;

  ///
  String searchFilter = '';

  ///
  (DateTime from, DateTime? to) dateFilter = (initDate, null);

  ///
  _InventoryPageState(this.tableAgent) {
    SetViewDateFilter<TruckInventory> dateFilterOption = SetViewDateFilter<TruckInventory>(0, dateFilter.$1, dateFilter.$2);
    filters.add(dateFilterOption);
  }

  filterSection(Section? section) {
    if (sectionFilter == section) return;
    sectionFilter = section;

    _composeFilters();
  }

  filterDate(DateTime from, DateTime? to) {
    dateFilter = (from, to);

    _composeFilters();
  }

  filterSearch(String search) {
    searchFilter = search;

    _composeFilters();
  }

  _composeFilters() {
    filters = <SetViewFilterNodeInterface<TruckInventory>>[];

    SetViewDateFilter<TruckInventory> dateFilterOption = SetViewDateFilter<TruckInventory>(0, dateFilter.$1, dateFilter.$2);
    filters.add(dateFilterOption);

    if (sectionFilter != null) {
      SetViewFilterNodeInterface<TruckInventory> nodeFilter = SetViewPropertyFilter<TruckInventory>(1, SetViewFilterEvaluations.equal, YardLog.kSection, sectionFilter!.id);
      filters.add(nodeFilter);
    }

    if (searchFilter.isNotEmpty) {
      SetViewPropertyFilter<TruckInventory> econoFilter = SetViewPropertyFilter<TruckInventory>(0, SetViewFilterEvaluations.contians, 'TruckNavigation.TruckCommonNavigation.Economic', searchFilter);
      SetViewPropertyFilter<TruckInventory> econoFilterExternal = SetViewPropertyFilter<TruckInventory>(0, SetViewFilterEvaluations.contians, 'TruckExternalNavigation.TruckCommonNavigation.Economic', searchFilter);
      List<SetViewFilterInterface<TruckInventory>> searchFilterFilters = <SetViewFilterInterface<TruckInventory>>[
        econoFilter,
        econoFilterExternal,
      ];

      SetViewFilterLinearEvaluation<TruckInventory> searchFilterOption = SetViewFilterLinearEvaluation<TruckInventory>(2, SetViewFilterEvaluationOperators.or, searchFilterFilters);
      filters.add(searchFilterOption);
    }

    effect();
    tableAgent.refresh();
  }
}
