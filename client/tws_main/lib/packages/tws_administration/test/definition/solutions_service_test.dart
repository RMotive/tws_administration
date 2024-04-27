import 'dart:convert';

import 'package:csm_foundation_services/csm_foundation_services.dart';
import 'package:test/test.dart';
import 'package:tws_administration/src/resolvers/main_resolver.dart';
import 'package:tws_administration/src/services/bases/solutions_service_base.dart';
import 'package:tws_administration/tws_administration.dart';

void main() {
  late final SolutionsServiceBase service;

  late MigrationView<Solution> mock;
  late MigrationViewOptions options;

  setUp(
    () {
      List<MigrationViewOrderOptions> noOrderigns = <MigrationViewOrderOptions>[];
      options = MigrationViewOptions(null, noOrderigns, 1, 10, false);
      mock = MigrationView<Solution>(<Solution>[], 1, DateTime.now(), 3, 0);

      Client mockClient = MockClient(
        (Request request) async {
          SuccessFrame<MigrationView<Solution>> mockFrame = SuccessFrame<MigrationView<Solution>>('randomGUID', mock);
          JObject jObject = mockFrame.encode();
          String object = jsonEncode(jObject);

          return Response(object, 200);
        },
      );

      service = TWSAdministrationSource(
        false,
        client: mockClient,
      ).solutions;
    },
  );

  test(
    'View',
    () async {
      MainResolver<MigrationView<Solution>> fact = await service.view(options);

      bool passed = false;
      fact.resolve(
        decoder: MigrationViewDecode<Solution>(SolutionDecode()),
        onConnectionFailure: () {},
        onFailure: (FailureFrame failure, int status) {
          assert(false, 'server returned a success $status');
        },
        onException: (Object exception, StackTrace trace) {
          assert(false, 'server returned a success');
        },
        onSuccess: (SuccessFrame<MigrationView<Solution>> success) {
          passed = true;

          MigrationView<Solution> fact = success.estela;
          expect(mock.page, fact.page);
          expect(mock.pages, fact.pages);
          expect(mock.records, fact.records);
          expect(mock.creation, fact.creation);
        },
      );

      expect(passed, true, reason: 'expected the service returned a success');
    },
    timeout: Timeout.factor(5),
  );
}
