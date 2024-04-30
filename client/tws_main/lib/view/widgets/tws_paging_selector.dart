import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:tws_main/view/widgets/tws_dropup.dart';

class TWSPagingSelector extends StatelessWidget {
  final int items;
  final int total;
  final int page;
  final int pages;

  const TWSPagingSelector({
    super.key,
    this.page = 1,
    this.items = 0,
    this.total = 0,
    required this.pages,
  });

  @override
  Widget build(BuildContext context) {
    return Row(
      children: <Widget>[
        // --> Items indicator
        Text(
          'Showing ($items) records from ($total) total records',
          style: const TextStyle(
            fontSize: 12,
            fontStyle: FontStyle.italic,
          ),
        ),
        Expanded(
          child: CSMSpacingRow(
            mainAlignment: MainAxisAlignment.end,
            spacing: 20,
            children: <Widget>[
              // --> Page range selector
              const TWSDropup(
                tooltip: 'Page range selection',
                item: 25,
                items: <int>[15, 25, 35, 50],
              ),
              // --> Page selector
              TWSDropup(
                tooltip: 'Page selection',
                item: 1,
                items: List<int>.generate(pages, (int i) => i + 1),
              ),
            ],
          ),
        ),
      ],
    );
  }
}
