import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_button_flat.dart';

final class TWSConfirmationDialog extends StatelessWidget {
  final String title;
  final Text? statement;
  final String accept;
  final VoidCallback? onClose;
  final VoidCallback? onAccept;

  const TWSConfirmationDialog({
    super.key,
    this.onClose,
    this.onAccept,
    this.statement,
    this.title = 'Confirmation',
    this.accept = 'Accept',
  });

  void _close(BuildContext ctx) {
    Navigator.of(ctx).pop();
    onClose?.call();
  }

  @override
  Widget build(BuildContext context) {
    final TWSAThemeBase theme = getTheme<TWSAThemeBase>();

    final CSMColorThemeOptions pageTheme = theme.page;
    final CSMColorThemeOptions dangerTheme = theme.primaryCriticalControl;

    return Padding(
      padding: const EdgeInsets.symmetric(
        vertical: 12,
        horizontal: 20,
      ),
      child: Center(
        child: ConstrainedBox(
          constraints: const BoxConstraints(
            maxWidth: 600,
            maxHeight: 450,
          ),
          child: ColoredBox(
            color: pageTheme.main,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.start,
              children: <Widget>[
                // --> Dialog header
                ColoredBox(
                  color: pageTheme.highlight,
                  child: Padding(
                    padding: const EdgeInsets.all(16.0),
                    child: Row(
                      children: <Widget>[
                        Text(
                          title,
                          style: TextStyle(
                            color: pageTheme.fore,
                            fontWeight: FontWeight.w800,
                          ),
                        ),
                        Expanded(
                          child: Row(
                            mainAxisAlignment: MainAxisAlignment.end,
                            children: <Widget>[
                              IconButton(
                                onPressed: () => _close(context),
                                icon: Icon(
                                  Icons.close,
                                  color: dangerTheme.highlight,
                                ),
                              ),
                            ],
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
                // --> Dialog bod
                Expanded(
                  child: Padding(
                    padding: const EdgeInsets.all(20),
                    child: DefaultTextStyle(
                      style: TextStyle(
                        color: pageTheme.fore,
                      ),
                      child: statement ??
                          const Text(
                            'Are you sure you want to continue?',
                          ),
                    ),
                  ),
                ),
                // --> Dialog footer
                ColoredBox(
                  color: pageTheme.highlight,
                  child: Padding(
                    padding: const EdgeInsets.symmetric(
                      vertical: 12,
                      horizontal: 20,
                    ),
                    child: CSMSpacingRow(
                      spacing: 12,
                      mainAlignment: MainAxisAlignment.end,
                      children: <Widget>[
                        TWSButtonFlat(
                          label: accept,
                          onTap: () => onAccept?.call(),
                        ),
                        TWSButtonFlat(
                          label: 'Cancel',
                          themeOptions: dangerTheme,
                          onTap: () => _close(context),
                        )
                      ],
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
