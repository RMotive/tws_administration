
part of '../whispers/trucks_create_whisper.dart';

final SessionStorage _sessionStorage = SessionStorage.i;
final TrucksServiceBase _trucksService = Sources.administration.trucks;

final class _ManufacturerViewAdapter implements TWSFutureAutocompleteAdapter<Manufacturer>{
  const _ManufacturerViewAdapter();

  @override
  Future<MigrationView<Manufacturer>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    String auth = _sessionStorage.session!.token;
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    final MainResolver<MigrationView<Manufacturer>> resolver = await Sources.administration.manufacturers.view(options, auth);
    final MigrationView<Manufacturer> view = await resolver.act(const MigrationViewDecode<Manufacturer>(ManufacturerDecoder())).catchError(
          (Object x, StackTrace s) {
            const CSMAdvisor('manufacturer-future-autocomplete-field-adapter').exception('Exception catched at Future Autocomplete field consume', Exception(x), s);
            throw x;
          },
        );
    return view;
  }
}

final class _SituationsViewAdapter implements TWSFutureAutocompleteAdapter<Situation>{
  const _SituationsViewAdapter();
  
  @override
  Future<MigrationView<Situation>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    String auth = _sessionStorage.session!.token;
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    final MainResolver<MigrationView<Situation>> resolver = await Sources.administration.situations.view(options, auth);
    final MigrationView<Situation> view = await resolver.act(const MigrationViewDecode<Situation>(SituationDecoder())).catchError(
          (Object x, StackTrace s) {
            const CSMAdvisor('situation-future-autocomplete-field-adapter').exception('Exception catched at Future Autocomplete field consume', Exception(x), s);
            throw x;
          },
        );
    return view;
  }
}