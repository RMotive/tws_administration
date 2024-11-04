part of '../trucks_article.dart';

/// [_TruckTable] component that build the [Truck] view table for Trucks Article.
class _TruckTable extends StatelessWidget {
  final TWSArticleTableAgent agent;
  final _TableAdapter adapter;
  const _TruckTable({
    required this.agent,
    required this.adapter,
  });

  @override
  Widget build(BuildContext context) {
    return TWSArticleTable<Truck>(
      editable: true,
      removable: false,
      adapter: adapter,
      agent: agent,
      fields: <TWSArticleTableFieldOptions<Truck>>[
        TWSArticleTableFieldOptions<Truck>(
          'Economic',
          (Truck item, int index, BuildContext ctx) =>
              item.truckCommonNavigation?.economic ?? '---',
        ),
        TWSArticleTableFieldOptions<Truck>(
          'VIN',
          (Truck item, int index, BuildContext ctx) => item.vin,
        ),
        TWSArticleTableFieldOptions<Truck>(
          'Carrier',
          (Truck item, int index, BuildContext ctx) =>
              item.carrierNavigation?.name ?? '---',
        ),
        TWSArticleTableFieldOptions<Truck>(
          'Manufacturer',
          (Truck item, int index, BuildContext ctx) =>
              item.vehiculeModelNavigation?.manufacturerNavigation?.name ?? '---',
        ),
        TWSArticleTableFieldOptions<Truck>(
          'Motor',
          (Truck item, int index, BuildContext ctx) => item.motor ?? '---',
          true,
        ),
        TWSArticleTableFieldOptions<Truck>(
          'SCT Number',
          (Truck item, int index, BuildContext ctx) =>
              item.sctNavigation?.number ?? '---',
          true,
        ),
        TWSArticleTableFieldOptions<Truck>(
          'USDOT number',
          (Truck item, int index, BuildContext ctx) =>
              item.carrierNavigation?.usdotNavigation?.scac ?? '---',
        ),
        TWSArticleTableFieldOptions<Truck>(
          'Trimestral Maintenance',
          (Truck item, int index, BuildContext ctx) =>
              item.maintenanceNavigation?.trimestral.toString() ?? '---',
          true,
        ),
        TWSArticleTableFieldOptions<Truck>(
          'Anual Maintenance',
          (Truck item, int index, BuildContext ctx) =>
              item.maintenanceNavigation?.anual.toString() ?? '---',
          true,
        ),
        TWSArticleTableFieldOptions<Truck>(
          'Insurance',
          (Truck item, int index, BuildContext ctx) =>
              item.insuranceNavigation?.policy ?? '---',
          true,
        ),
        TWSArticleTableFieldOptions<Truck>(
          'Plates',
          (Truck item, int index, BuildContext ctx) => adapter.getPlates(item),
          true,
        ),
        TWSArticleTableFieldOptions<Truck>(
          'Situation',
          (Truck item, int index, BuildContext ctx) =>
              item.truckCommonNavigation?.situationNavigation?.name ?? '---',
          true,
        ),
        TWSArticleTableFieldOptions<Truck>(
          'Location',
          (Truck item, int index, BuildContext ctx) =>
              item.truckCommonNavigation?.locationNavigation?.name ?? '---',
          true,
        ),
      ],
      page: 1,
      size: 25,
      sizes: const <int>[25, 50, 75, 100],
    );
  }
}
