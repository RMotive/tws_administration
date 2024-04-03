part of '../article_frame.dart';

class _ArticleActions extends StatelessWidget {
  final ActionRibbonOptions options;
  const _ArticleActions({
    required this.options,
  });

  @override
  Widget build(BuildContext context) {
    final MaintenanceGroupOptions? mtnOptions = options.maintenanceGroupConfig;

    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        return DecoratedBox(
          decoration: const BoxDecoration(
            border: Border.fromBorderSide(
              BorderSide(
                width: 1,
                color: Colors.blueGrey,
              ),
            ),
          ),
          child: ConstrainedBox(
            constraints: BoxConstraints(
              minHeight: constrains.maxHeight,
            ),
            child: Padding(
              padding: const EdgeInsets.symmetric(
                horizontal: 8,
                vertical: 4,
              ),
              child: SpacingRow(
                spacing: 8,
                children: <Widget>[
                  if (mtnOptions != null)
                    _ArticleActionsGroup(
                      name: 'Maintenance',
                      actions: <ArticleFrameActionsOptions>[
                        if (mtnOptions.onCreate != null)
                          // --> Create button action
                          ArticleFrameActionsOptions(
                            tooltip: 'Create',
                            action: mtnOptions.onCreate!,
                            decorator: (double size, Color color) {
                              return Icon(
                                Icons.add_box,
                                color: color,
                                size: size * .7,
                              );
                            },
                          ),
                      ],
                    ),
                ],
              ),
            ),
          ),
        );
      },
    );
  }
}
