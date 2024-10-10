
part of '../whispers/trucks_create_whisper.dart';

final SessionStorage _sessionStorage = SessionStorage.i;
final TrucksServiceBase _trucksService = Sources.administration.trucks;

final class _ManufacturerViewAdapter {
  const _ManufacturerViewAdapter();

  @override
  Future<SetViewOut<Manufacturer>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    String auth = _sessionStorage.session!.token;
    final SetViewOptions<Manufacturer> options = SetViewOptions<Manufacturer>(false, page, range, null, orderings, <SetViewFilterNodeInterface<Manufacturer>>[]);
    final MainResolver<SetViewOut<Manufacturer>> resolver = await Sources.administration.manufacturers.view(options, auth);
    final SetViewOut<Manufacturer> view = await resolver.act((JObject json) => SetViewOut<Manufacturer>.des(json, Manufacturer.des)).catchError(
          (Object x, StackTrace s) {
            const CSMAdvisor('manufacturer-future-autocomplete-field-adapter').exception('Exception catched at Future Autocomplete field consume', Exception(x), s);
            throw x;
          },
        );
    return view;
  }
}

final class _SituationsViewAdapter{
  const _SituationsViewAdapter();
  
  @override
  Future<SetViewOut<Situation>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    String auth = _sessionStorage.session!.token;
    final SetViewOptions<Situation> options =  SetViewOptions<Situation>(false, page, range, null, orderings, <SetViewFilterNodeInterface<Situation>>[]);
    final MainResolver<SetViewOut<Situation>> resolver = await Sources.administration.situations.view(options, auth);
    final SetViewOut<Situation> view = await resolver.act((JObject json) => SetViewOut<Situation>.des(json, Situation.des)).catchError(
          (Object x, StackTrace s) {
            const CSMAdvisor('situation-future-autocomplete-field-adapter').exception('Exception catched at Future Autocomplete field consume', Exception(x), s);
            throw x;
          },
        );
    return view;
  }
}