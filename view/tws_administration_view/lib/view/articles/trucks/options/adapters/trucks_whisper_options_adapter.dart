
part of '../../whispers/trucks_create_whisper.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _VehiculeModelViewAdapter implements TWSAutocompleteAdapter{
  const _VehiculeModelViewAdapter();

  @override
  Future<List<SetViewOut<VehiculeModel>>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    String auth = _sessionStorage.session!.token;
    final SetViewOptions<VehiculeModel> options = SetViewOptions<VehiculeModel>(false,10, page, null, orderings, <SetViewFilterNodeInterface<VehiculeModel>>[]);
    final MainResolver<SetViewOut<VehiculeModel>> resolver = await Sources.foundationSource.vehiculesModels.view(options, auth);
    final SetViewOut<VehiculeModel> view = await resolver.act((JObject json) => SetViewOut<VehiculeModel>.des(json, VehiculeModel.des)).catchError(
          (Object x, StackTrace s) {
            const CSMAdvisor('VehiculeModel-future-autocomplete-field-adapter').exception('Exception catched at Future Autocomplete field consume', Exception(x), s);
            throw x;
          },
        );
    return <SetViewOut<VehiculeModel>>[view];
  }
}

final class _SituationsViewAdapter implements TWSAutocompleteAdapter{
  const _SituationsViewAdapter();
  
  @override
  Future<List<SetViewOut<Situation>>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    String auth = _sessionStorage.session!.token;
    final SetViewOptions<Situation> options =  SetViewOptions<Situation>(false, range, page, null, orderings, <SetViewFilterNodeInterface<Situation>>[]);
    final MainResolver<SetViewOut<Situation>> resolver = await Sources.foundationSource.situations.view(options, auth);
    final SetViewOut<Situation> view = await resolver.act((JObject json) => SetViewOut<Situation>.des(json, Situation.des)).catchError(
          (Object x, StackTrace s) {
            const CSMAdvisor('situation-future-autocomplete-field-adapter').exception('Exception catched at Future Autocomplete field consume', Exception(x), s);
            throw x;
          },
        );
    return <SetViewOut<Situation>>[view];
  }
}

final class _CarriersViewAdapter implements TWSAutocompleteAdapter {
  const _CarriersViewAdapter();
  
  @override
  Future<List<SetViewOut<Carrier>>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    String auth = _sessionStorage.session!.token;
    final SetViewOptions<Carrier> options =  SetViewOptions<Carrier>(false, 10, page, null, orderings, <SetViewFilterNodeInterface<Carrier>>[]);
    final MainResolver<SetViewOut<Carrier>> resolver = await Sources.foundationSource.carriers.view(options, auth);
    final SetViewOut<Carrier> view = await resolver.act((JObject json) => SetViewOut<Carrier>.des(json, Carrier.des)).catchError(
          (Object x, StackTrace s) {
            const CSMAdvisor('Carrier-future-autocomplete-field-adapter').exception('Exception catched at Future Autocomplete field consume', Exception(x), s);
            throw x;
          },
        );
    return <SetViewOut<Carrier>>[view];
  }
}

final class _ManufacturersViewAdapter implements TWSAutocompleteAdapter {
  const _ManufacturersViewAdapter();
  
  @override
  Future<List<SetViewOut<Manufacturer>>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    String auth = _sessionStorage.session!.token;
    final SetViewOptions<Manufacturer> options =  SetViewOptions<Manufacturer>(false, 10, page, null, orderings, <SetViewFilterNodeInterface<Manufacturer>>[]);
    final MainResolver<SetViewOut<Manufacturer>> resolver = await Sources.foundationSource.manufacturers.view(options, auth);
    final SetViewOut<Manufacturer> view = await resolver.act((JObject json) => SetViewOut<Manufacturer>.des(json, Manufacturer.des)).catchError(
          (Object x, StackTrace s) {
            const CSMAdvisor('Manufacturer-future-autocomplete-field-adapter').exception('Exception catched at Future Autocomplete field consume', Exception(x), s);
            throw x;
          },
        );
    return <SetViewOut<Manufacturer>>[view];
  }
}