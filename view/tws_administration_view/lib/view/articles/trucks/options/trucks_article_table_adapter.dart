part of '../trucks_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Truck> {
  const _TableAdapter();

  Object getFactory(Type set){
    if(set is Truck) return Truck.a();
    return TruckExternal.a();
  } 

  String getPlates(Truck item){
    String plates = '---';
    if (item.plates.isNotEmpty) {
      plates = '';
      for (int cont = 0; cont < item.plates.length; cont++) {
        plates += item.plates[cont].identifier;
        if (item.plates.length > cont) plates += '\n';
      }
    }
    return plates;
  }

  @override
  Future<SetViewOut<Truck>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    final SetViewOptions<Truck> options = SetViewOptions<Truck>(false, range, page, null, orderings, <SetViewFilterNodeInterface<Truck>>[]);
    String auth = _sessionStorage.session!.token;
    MainResolver<SetViewOut<Truck>> resolver = await Sources.administration.trucks.view(options, auth);

    SetViewOut<Truck> view = await resolver.act((JObject json) => SetViewOut<Truck>.des(json, Truck.des)).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('truck-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
  
  @override
  TWSArticleTableEditor? composeEditor(Truck set, Function closeReinvoke, BuildContext context) {
    return TWSArticleTableEditor(
      form: const SizedBox.expand(), 
      onCancel: () {}
    );
  }

  @override
  Widget composeViewer(Truck set, BuildContext context) {
    return SizedBox.expand(
      child: CSMSpacingColumn(
        spacing: 12,
        crossAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          TWSPropertyViewer(
            label: 'Economic',
            value: set.truckCommonNavigation?.economic ?? '---',
          ),
          TWSPropertyViewer(
            label: 'VIN',
            value: set.vin,
          ),
          TWSPropertyViewer(
            label: 'Carrier',
            value: set.carrierNavigation?.name ?? '---',
          ),
          TWSPropertyViewer(
            label: 'Manufacturer',
            value: set.vehiculeModelNavigation?.manufacturerNavigation?.name ?? '---',
          ),
          TWSPropertyViewer(
            label: 'Motor',
            value: set.motor ?? '---',
          ),
          TWSPropertyViewer(
            label: 'SCT number',
            value: set.sctNavigation?.number ?? '---',
          ),
          TWSPropertyViewer(
            label: 'USDOT number',
            value: set.carrierNavigation?.usdotNavigation?.scac ?? '---',
          ),
          TWSPropertyViewer(
            label: 'Trimestral Maintenance',
            value: set.maintenanceNavigation?.trimestral.dateOnlyString ?? '---',
          ),
          TWSPropertyViewer(
            label: 'Anual Maintenance',
            value: set.maintenanceNavigation?.anual.dateOnlyString ?? '---',
          ),
          TWSPropertyViewer(
            label: 'Situation',
            value: set.truckCommonNavigation?.situationNavigation?.name ?? '---',
          ),
          TWSPropertyViewer(
            label: 'Location',
            value: set.truckCommonNavigation?.locationNavigation?.name ?? '---',
          ),
          TWSPropertyViewer(
            label: 'Plates',
            value: getPlates(set),
          ),
        ],
      ),
    );
  }

  @override
  void onRemoveRequest(Truck set, BuildContext context) { }
}
