part of '../trailers_article.dart';

/// [_TrailerTable] component that build the [Truck] view table for Trucks Article.
class _TrailerTable extends StatelessWidget {
  final TWSArticleTableAgent agent;
  final _TableAdapter adapter;
  const _TrailerTable({
    required this.agent,
    required this.adapter,
  });

  @override
  Widget build(BuildContext context) {
    return TWSArticleTable<Trailer>(
      editable: true,
      removable: false,
      adapter: adapter,
      agent: agent,
      fields: <TWSArticleTableFieldOptions<Trailer>>[
        TWSArticleTableFieldOptions<Trailer>(
          'Economic',
          (Trailer item, int index, BuildContext ctx) =>
              item.trailerCommonNavigation?.economic ?? '---',
        ),
        TWSArticleTableFieldOptions<Trailer>(
          'Carrier',
          (Trailer item, int index, BuildContext ctx) =>
              item.carrierNavigation?.name ?? '---',
        ),
        TWSArticleTableFieldOptions<Trailer>(
          'Type',
          (Trailer item, int index, BuildContext ctx) =>
              "${item.trailerCommonNavigation?.trailerTypeNavigation?.trailerClassNavigation?.name} - ${item.trailerCommonNavigation?.trailerTypeNavigation?.size}",
        ),
        TWSArticleTableFieldOptions<Trailer>(
          'Manufacturer',
          (Trailer item, int index, BuildContext ctx) =>
              item.vehiculeModelNavigation?.manufacturerNavigation?.name ?? '---',
        ),
        TWSArticleTableFieldOptions<Trailer>(
          'SCT Number',
          (Trailer item, int index, BuildContext ctx) =>
              item.sctNavigation?.number ?? '---',
          true,
        ),
        TWSArticleTableFieldOptions<Trailer>(
          'USDOT number',
          (Trailer item, int index, BuildContext ctx) =>
              item.carrierNavigation?.usdotNavigation?.scac ?? '---',
        ),
        TWSArticleTableFieldOptions<Trailer>(
          'Trimestral Maintenance',
          (Trailer item, int index, BuildContext ctx) =>
              item.maintenanceNavigation?.trimestral.dateOnlyString ?? '---',
          true,
        ),
        TWSArticleTableFieldOptions<Trailer>(
          'Anual Maintenance',
          (Trailer item, int index, BuildContext ctx) =>
              item.maintenanceNavigation?.anual.dateOnlyString ?? '---',
          true,
        ),
        TWSArticleTableFieldOptions<Trailer>(
          'Plates',
          (Trailer item, int index, BuildContext ctx) => adapter.getPlates(item),
          true,
        ),
        TWSArticleTableFieldOptions<Trailer>(
          'Situation',
          (Trailer item, int index, BuildContext ctx) =>
              item.trailerCommonNavigation?.situationNavigation?.name ?? '---',
          true,
        ),
      ],
      page: 1,
      size: 25,
      sizes: const <int>[25, 50, 75, 100],
    );
  }
}
