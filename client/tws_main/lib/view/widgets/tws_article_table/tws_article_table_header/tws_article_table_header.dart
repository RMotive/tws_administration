part of '../tws_article_table.dart';

final class _TWSArticleTableHeader<TArticle> extends StatelessWidget {
  final List<TWSArticleTableFieldOptions<TArticle>> fields;
  final double minFieldWidth;
  final double fieldWidth;

  const _TWSArticleTableHeader({
    required this.fields,
    required this.fieldWidth,
    required this.minFieldWidth,
  });

  @override
  Widget build(BuildContext context) {
    return DecoratedBox(
      decoration: const BoxDecoration(
        border: Border.fromBorderSide(
          BorderSide(
            width: 1,
            color: Colors.blueGrey,
          ),
        ),
      ),
      child: Row(
        children: <Widget>[
          for (TWSArticleTableFieldOptions<TArticle> header in fields)
            ConstrainedBox(
              constraints: BoxConstraints(
                minWidth: minFieldWidth,
              ),
              child: SizedBox(
                width: fieldWidth,
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                    vertical: 12,
                    horizontal: 8,
                  ),
                  child: Text(
                    header.name,
                    style: const TextStyle(
                      color: Colors.white,
                      fontWeight: FontWeight.w900,
                    ),
                  ),
                ),
              ),
            ),
        ],
      ),
    );
  }
}
