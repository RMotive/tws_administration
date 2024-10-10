part of 'tws_article_table.dart';

final class _TWSArticleTableError extends StatelessWidget {
  final Size viewSize;
  const _TWSArticleTableError({required this.viewSize});

  @override
  Widget build(BuildContext context) {
    const double width = 300;
    return Padding(
      padding: EdgeInsets.symmetric(
        horizontal: (viewSize.width / 2) - (width / 2),
      ),
      child: const TWSDisplayFlat(
        width: width,
        display: 'Critical error reading view',
      ),
    );
  }
}
