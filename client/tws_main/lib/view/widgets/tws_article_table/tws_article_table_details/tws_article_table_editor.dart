part of '../tws_article_table.dart';

final class TWSArticleTableEditor extends StatelessWidget {
  final Widget form;
  final Function onCancel;
  final FutureOr<void> Function()? onSave;

  const TWSArticleTableEditor({
    super.key,
    this.onSave,
    required this.form,
    required this.onCancel,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        // --> Editor actions
        CSMSpacingRow(
          mainAlignment: MainAxisAlignment.end,
          spacing: 8,
          children: <Widget>[
            _TWSArticleTableDetailsAction(
              icon: Icons.close_rounded,
              hint: 'Cancel editing',
              action: () => onCancel(),
            ),
            if (onSave != null)
              _TWSArticleTableDetailsAction(
                icon: Icons.save_alt_rounded,
                hint: 'Save',
                action: () async {
                  await onSave?.call();
                },
              ),
          ],
        ),

        // --> Custom content
        Expanded(
          child: Padding(
            padding: const EdgeInsets.only(
              top: 12,
            ),
            child: SizedBox(
              child: form,
            ),
          ),
        ),
      ],
    );
  }
}
