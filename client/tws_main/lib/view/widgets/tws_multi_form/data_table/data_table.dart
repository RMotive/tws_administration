part of "../tws_multi_form.dart";

const double _tdMinHeight = 445;

/// [_DataTable] Widget class that shows the table data section.
/// In this section incluide some buttons options to add or delete rows of items
/// & a list of items with its asigned data by the [_ValueDetails] section.
class _DataTable extends StatelessWidget {
  final double listHeight;

  final String title;
  const _DataTable({
    required this.listHeight,
    required this.title,
  });

  @override
  Widget build(BuildContext context) {
    final CSMColorThemeOptions pageStruct = getTheme<TWSAThemeBase>().page;

    return Padding(
      padding: const EdgeInsets.symmetric(
        horizontal: 20,
        vertical: 20,
      ),
      child: Column(
        children: <Widget>[
          // --> Table actions ribbon
          _TableActionsRibbon(
            add: () {},
            remove: () {},
            show: () {},
          ),
          // --> Table header
          Container(
            height: 40,
            color: pageStruct.highlight,
            alignment: Alignment.centerLeft,
            padding: const EdgeInsets.symmetric(
              horizontal: 10,
            ),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Text(
                  title,
                  style: TextStyle(
                    fontSize: 14,
                    color: pageStruct.fore,
                  ),
                ),
                Text(
                  "Total items: ${_tableValues.length}",
                  style: TextStyle(
                    fontSize: 14,
                    color: pageStruct.fore,
                  ),
                ),
              ],
            ),
          ),

          // --> Table body content
          Material(
            color: pageStruct.foreAlt,
            child: Container(
              height: listHeight,
              constraints: const BoxConstraints(
                minHeight: _tdMinHeight,
              ),
              decoration: BoxDecoration(
                border: Border.fromBorderSide(
                  BorderSide(
                    color: pageStruct.highlight,
                    width: 2,
                  ),
                ),
              ),
              child: ListView.builder(
                itemCount: _tableValues.length,
                itemBuilder: (_, int index) {
                  return ListTile(
                    selected: _selectedItem == index,
                    enabled: _selectedItem != index,
                    hoverColor: _pageTheme.main,
                    selectedTileColor: _pageTheme.highlight,
                    onTap: () {
                      _selectItem(index);
                      _globalState.effect();
                    },
                    subtitle: Wrap(children: _generateLabels(index)),
                    title: Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 8.0),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: <Widget>[
                          Text(
                            "#${index + 1} $title",
                            style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold, color: _pageTheme.fore),
                          )
                        ],
                      ),
                    ),
                  );
                },
              ),
            ),
          ),
        ],
      ),
    );
  }
}
