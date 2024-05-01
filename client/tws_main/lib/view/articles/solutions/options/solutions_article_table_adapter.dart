part of '../solutions_article.dart';

const CSMAdvisor _advisor = CSMAdvisor('solution-article-table');

final class _TableAdapter implements TWSArticleTableDataAdapter<Solution> {
  const _TableAdapter();

  @override
  Future<MigrationView<Solution>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    MainResolver<MigrationView<Solution>> resolver = await administration.solutions.view(options, '');

    late MigrationView<Solution> view;
    resolver.resolve(
      onConnectionFailure: () {
        _advisor.warning('Connection failure');
        throw 'ConnectionException';
      },
      onFailure: (FailureFrame failure, int status) {
        _advisor.warning(
          'Request failure ($status)',
          info: <String, dynamic>{
            'situation': failure.estela.situation,
            'system': failure.estela.system,
            'advise': failure.estela.advise,
          },
        );
        throw failure;
      },
      onSuccess: (SuccessFrame<MigrationView<Solution>> success) {
        _advisor.success('Success request (${success.tracer})');
        view = success.estela;
      },
    );
    return view;
  }

  @override
  Widget factory(Solution entry) {
    throw UnimplementedError();
  }
}
