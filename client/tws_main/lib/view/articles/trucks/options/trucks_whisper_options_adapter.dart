
part of '../whispers/truck_create_whisper.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

Future<MigrationView<TSet>> consumeOptions<TSet extends CSMEncodeInterface>(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    final MainResolver<MigrationView<dynamic>> resolver;
    final MigrationView<dynamic> view;
    String auth = _sessionStorage.session!.token;

    switch(TSet){
      case const (Manufacturer):
        resolver = await Sources.administration.manufacturers.view(options, auth);
        view = await resolver.act(const MigrationViewDecode<Manufacturer>(ManufacturerDecoder())).catchError(
          (Object x, StackTrace s) {
            const CSMAdvisor('manufacturer-future-autocomplete-field-adapter').exception('Exception catched at Future Autocomplete field consume', Exception(x), s);
            throw x;
          },
        );
      break;
      default:
       resolver = await Sources.administration.situations.view(options, auth);
        view = await resolver.act(const MigrationViewDecode<Situation>(SituationDecoder())).catchError(
          (Object x, StackTrace s) {
            const CSMAdvisor('situation-future-autocomplete-field-adapter').exception('Exception catched at Future Autocomplete field consume', Exception(x), s);
            throw x;
          },
        );
      break;
    }
    
    return view as MigrationView<TSet>;
  }