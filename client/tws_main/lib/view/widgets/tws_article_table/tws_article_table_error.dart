part of 'tws_article_table.dart';

final class _TWSArticleTableError extends StatelessWidget {
  const _TWSArticleTableError();

  @override
  Widget build(BuildContext context) {
    return const Padding(
      padding: EdgeInsets.only(
        top: 50,
        left: 20,
        right: 20,
      ),
      child: TWSDisplayFlat(
        width: 300,
        display: 'Critical error reading view',
      ),
    );
  }
}
