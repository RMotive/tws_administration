part of '../tws_article_table.dart';

final class _TWSArticleTableDetails extends StatelessWidget {
  final VoidCallback closeAction;

  const _TWSArticleTableDetails({
    required this.closeAction,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      color: Colors.red,
      child: Column(
        children: <Widget>[
          Row(
            children: <Widget>[
              CSMPointerHandler(
                cursor: SystemMouseCursors.click,
                onClick: closeAction,
                child: const Icon(
                  Icons.close,
                  size: 24,
                ),
              ),
            ],
          )
        ],
      ),
    );
  }
}
