import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_dropup.dart';

class TWSPagingSelector extends StatefulWidget {
  final int items;
  final int total;
  final int page;
  final int pages;

  final int size;
  final List<int> sizes;

  final void Function(int page, int size) onChange;

  const TWSPagingSelector({
    super.key,
    this.page = 1,
    this.items = 0,
    this.total = 0,
    required this.pages,
    required this.size,
    required this.sizes,
    required this.onChange,
  });

  @override
  State<TWSPagingSelector> createState() => _TWSPagingSelectorState();
}

class _TWSPagingSelectorState extends State<TWSPagingSelector> {
  late CSMColorThemeOptions theme;

  late int page;
  late int size;

  void themeUpdate([TWSAThemeBase? themeBase]) {
    themeBase ??= getTheme();
    theme = themeBase.pageColor;
  }

  @override
  void initState() {
    page = widget.page;
    size = widget.size;
    TWSAThemeBase themeBase = getTheme(
      updateEfect: themeUpdate,
    );

    themeUpdate(themeBase);
    super.initState();
  }



  @override
  Widget build(BuildContext context) {
    return Row(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: <Widget>[
        // --> Items indicator
        CSMResponsiveView(
          onLarge: RichText(
            text: TextSpan(
              text: 'Showing ',
              style: TextStyle(
                color: theme.fore,
                fontWeight: FontWeight.w100,
                fontStyle: FontStyle.italic,
              ),
              children: <TextSpan>[
                TextSpan(
                  text: '(${widget.items})',
                  style: const TextStyle(
                    fontWeight: FontWeight.w900,
                  ),
                ),
                const TextSpan(
                  text: ' records from ',
                ),
                TextSpan(
                  text: '(${widget.total})',
                  style: const TextStyle(
                    fontWeight: FontWeight.w900,
                  ),
                ),
              ],
            ),
          ),
          onSmall: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              RichText(
                text: TextSpan(
                  text: 'Showing',
                  style: const TextStyle(
                    color: Colors.white,
                    fontStyle: FontStyle.italic,
                    fontSize: 10,
                  ),
                  children: <TextSpan>[
                    TextSpan(
                      text: ' (${widget.total})',
                      style: const TextStyle(
                        fontWeight: FontWeight.w900,
                      ),
                    ),
                  ],
                ),
              ),
              RichText(
                text: TextSpan(
                  text: 'from',
                  style: const TextStyle(
                    color: Colors.white,
                    fontStyle: FontStyle.italic,
                    fontSize: 10,
                  ),
                  children: <TextSpan>[
                    TextSpan(
                      text: ' (${widget.items})',
                      style: const TextStyle(
                        fontWeight: FontWeight.w900,
                      ),
                    ),
                  ],
                ),
              )
            ],
          ),
        ),
        Expanded(
          child: CSMSpacingRow(
            mainAlignment: MainAxisAlignment.end,
            spacing: 20,
            children: <Widget>[
              // --> Page range selector
              TWSDropup<int>(
                tooltip: 'Page range selection',
                item: widget.size,
                items: widget.sizes,
                onChange: (int size) {
                  setState(() {
                    this.size = size;
                  });
                  widget.onChange(page, size);
                },
              ),
              // --> Page selector
              TWSDropup<int>(
                tooltip: 'Page selection',
                item: widget.page,
                onChange: (int page) {
                  setState(() {
                    this.page = page;
                  });
                  widget.onChange(page, size);
                },
                items: List<int>.generate(widget.pages, (int i) => i + 1),
              ),
            ],
          ),
        ),
      ],
    );
  }
}
