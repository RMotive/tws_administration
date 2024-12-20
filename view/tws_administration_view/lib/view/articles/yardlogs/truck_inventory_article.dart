import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/constants/twsa_common_displays.dart';
import 'package:tws_administration_view/core/extension/datetime.dart';
import 'package:tws_administration_view/core/router/twsa_routes.dart';
import 'package:tws_administration_view/data/services/sources.dart';
import 'package:tws_administration_view/data/storages/session_storage.dart';
import 'package:tws_administration_view/view/frames/article/action_ribbon_options.dart';
import 'package:tws_administration_view/view/pages/yardlog/yardlog_frame.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_adapter.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_agent.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_field_options.dart';
import 'package:tws_administration_view/view/widgets/tws_autocomplete_field/tws_autocomplete_adapter.dart';
import 'package:tws_administration_view/view/widgets/tws_autocomplete_field/tws_autocomplete_field.dart';
import 'package:tws_administration_view/view/widgets/tws_datepicker_field.dart';
import 'package:tws_administration_view/view/widgets/tws_input_text.dart';
import 'package:tws_administration_view/view/widgets/tws_property_viewer.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

part 'options/truck_inventory_table_adapter.dart';
part 'options/inventory_page_state.dart';

final TWSArticleTableAgent tableAgent = TWSArticleTableAgent();
final _InventoryPageState _pageState = _InventoryPageState(tableAgent);

class TruckInventoryArticle extends CSMPageBase {
  final CSMRouteOptions currentRoute;
  const TruckInventoryArticle({
    super.key,
    required this.currentRoute
  });
  


  @override
  Widget compose(BuildContext ctx, Size window) {
    return YardlogFrame(
      currentRoute: TWSARoutes.yardlogsTruckInventoryArticle,
      actionsOptions: ActionRibbonOptions(
        refresher: tableAgent.refresh,
      ),
      article: CSMDynamicWidget<_InventoryPageState>(
        state: _pageState,
        designer: (BuildContext ctx, _InventoryPageState state) {
          _TableAdapter adapter = _TableAdapter(state);
          return Column(
          children: <Widget>[
            SizedBox(
              width: double.maxFinite,
              child: LayoutBuilder(
                builder: (BuildContext context, BoxConstraints boxConstraints) {
                  final double endRowSize = boxConstraints.maxWidth - 440;
                  final bool centerControls = boxConstraints.maxWidth < (448 + 628);
        
                  return Padding(
                    padding: const EdgeInsets.all(8),
                    child: Wrap(
                      alignment: centerControls ? WrapAlignment.center : WrapAlignment.start,
                      spacing: 12,
                      runSpacing: 12,
                      children: <Widget>[
                        TWSDatepicker(
                          label: 'Rango inicial',
                          enablePrefix: false,
                          firstDate: _InventoryPageState.initDate,
                          initialDate: _InventoryPageState.initDate,
                          lastDate: DateTime.now(),
                          onChanged: (String textDate) {
                            DateTime date = DateTime.parse(textDate);
        
                            state.filterDate(date, state.dateFilter.$2);
                          },
                        ),
                        TWSDatepicker(
                          label: 'Rango final',
                          firstDate: _InventoryPageState.initDate,
                          lastDate: DateTime.now(),
                          validator: (String? text) {
                            if (text == null) return 'How did you achieve this x.x';
        
                            DateTime toDate = DateTime.parse(text);
                            if (toDate.isBefore(state.dateFilter.$1)) return 'Debe ser mayor al inicial';
        
                            return null;
                          },
                          onChanged: (String text) {
                            DateTime? date;
                            if (text.isNotEmpty) {
                              date = DateTime.parse(text);
                            }
        
                            state.filterDate(state.dateFilter.$1, date);
                          },
                        ),
                        ConstrainedBox(
                          constraints: const BoxConstraints(
                            minWidth: 612,
                          ),
                          child: SizedBox(
                            width: endRowSize,
                            child: Align(
                              alignment: centerControls ? Alignment.center : Alignment.centerRight,
                              child: Wrap(
                                alignment: centerControls ? WrapAlignment.center : WrapAlignment.start,
                                verticalDirection: VerticalDirection.up,
                                spacing: 12,
                                runSpacing: 12,
                                children: <Widget>[
                                  TWSAutoCompleteField<Section>(
                                    label: 'Section',
                                    isOptional: true,
                                    adapter: const _SectionViewAdapter(),
                                    width: 250,
                                    initialValue: state.sectionFilter,
                                    displayValue: (Section? record) {
                                      if (record == null) return '---';
                                      return '${record.name}: ${record.locationNavigation?.name ?? '---'}';
                                    },
                                    onChanged: (Section? selection) {
                                      state.filterSection(selection);
                                    },
                                  ),
                                  TWSInputText(
                                    label: 'Search',
                                    deBounce: 600.miliseconds,
                                    width: 350,
                                    onChanged: state.filterSearch,
                                  ),
                                ],
                              ),
                            ),
                          ),
                        ),
                      ],
                    ),
                  );
                },
              ),
            ),
            Expanded(
              child: TWSArticleTable<TruckInventory>(
                editable: false,
                removable: false,
                adapter: adapter,
                agent: tableAgent,
                fields: <TWSArticleTableFieldOptions<TruckInventory>>[
                  TWSArticleTableFieldOptions<TruckInventory>(
                    'Entry',
                    (TruckInventory item, int index, BuildContext ctx) => item.entryDate.dateOnlyString,
                  ),
                  TWSArticleTableFieldOptions<TruckInventory>(
                    'Economic',
                    (TruckInventory item, int index, BuildContext ctx) {
                      return  item.truckNavigation != null? item.truckNavigation!.truckCommonNavigation!.economic
                      : item.truckExternalNavigation != null? item.truckExternalNavigation!.truckCommonNavigation!.economic
                      : '---';
                    },
                  ),
                  TWSArticleTableFieldOptions<TruckInventory>(
                    'MX Plate',
                    (TruckInventory item, int index, BuildContext ctx) {
                      return adapter.getPlates(item, true);
                    },
                  ),
                  TWSArticleTableFieldOptions<TruckInventory>(
                    'USA Plate',
                    (TruckInventory item, int index, BuildContext ctx) {
                      return  adapter.getPlates(item, false);
                    },
                  ),
                  TWSArticleTableFieldOptions<TruckInventory>(
                    'Carrier',
                    (TruckInventory item, int index, BuildContext ctx) {
                      return  item.truckNavigation != null? item.truckNavigation!.carrierNavigation!.name 
                      : item.truckExternalNavigation != null? item.truckExternalNavigation!.carrier
                      : '---';
                    },
                  ),
                  TWSArticleTableFieldOptions<TruckInventory>(
                    'Section',
                    (TruckInventory item, int index, BuildContext ctx) => "${item.sectionNavigation?.name} - ${item.sectionNavigation?.locationNavigation?.name}",
                  ),
                  
                  TWSArticleTableFieldOptions<TruckInventory>(
                    'Ownership',
                    (TruckInventory item, int index, BuildContext ctx) => item.truck != null? "Own" : "External",
                  ),
              
                ],
                page: 1,
                size: 25,
                sizes: const <int>[25, 50, 75, 100],
              ),
            ),
          ],
        );
        },
      ),
    );
  }
}
