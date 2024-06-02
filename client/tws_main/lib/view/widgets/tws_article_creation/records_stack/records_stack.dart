part of '../tws_article_creation.dart';

class _RecordsStack extends StatelessWidget {
  final CSMColorThemeOptions pageTheme;
  final int recordsCount;

  const _RecordsStack({
    required this.pageTheme,
    required this.recordsCount,
  });

  @override
  Widget build(BuildContext context) {    
    return Column(
      children: <Widget>[
        // --> Actions
        CSMSpacingRow(
          mainAlignment: MainAxisAlignment.end,
          spacing: 8,
          includeEnd: true,
          includeStart: true,
          children: <Widget>[
            Expanded(
              child: Text(
                'Records count: $recordsCount',
                style: TextStyle(
                  color: pageTheme.fore,
                ),
              ),
            ),
            // --> Add item action
            Icon(
              Icons.add,
              size: 24,
              color: pageTheme.fore,
            ),
            // --> Remove selection
            Icon(
              Icons.remove,
              size: 24,
              color: Colors.red.shade200,
            ),
          ],
        ),
        // --> Stack Display Content
        Container(),
      ],
    );
  }
}
