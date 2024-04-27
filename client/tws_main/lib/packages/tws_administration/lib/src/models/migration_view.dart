import 'package:csm_foundation_services/csm_foundation_services.dart';

final class MigrationView<TSet extends CSMEncodeInterface> implements CSMEncodeInterface {
  final List<TSet> sets;
  final int pages;
  final int page;
  final DateTime creation;
  final int records;

  const MigrationView(this.sets, this.page, this.creation, this.pages, this.records);
  factory MigrationView.des(
    JObject json, {
    CSMDecodeInterface<TSet>? setDecode,
  }) {
    List<JObject> rawSetsArray = json.getDefault('sets', <dynamic>[]).cast();
    List<TSet> setsObjects = rawSetsArray
        .map<TSet>((JObject e) => deserealize(
              json,
              decode: setDecode,
            ))
        .toList();

    int pages = json.get('pages');
    int page = json.get('page');
    int records = json.get('records');

    DateTime creation = json.get('creation');

    return MigrationView<TSet>(setsObjects, page, creation, pages, records);
  }

  @override
  JObject encode() {
    return <String, dynamic>{
      'sets': sets.map((TSet e) => e.encode()).toList(),
      'pages': pages,
      'page': page,
      'creation': creation.toString(),
      'records': records,
    };
  }
}

final class MigrationViewDecode<TSet extends CSMEncodeInterface> implements CSMDecodeInterface<MigrationView<TSet>> {
  final CSMDecodeInterface<TSet> setDecoder;

  const MigrationViewDecode(this.setDecoder);

  @override
  MigrationView<TSet> decode(JObject json) {
    return MigrationView<TSet>.des(json, setDecode: setDecoder);
  }
}
